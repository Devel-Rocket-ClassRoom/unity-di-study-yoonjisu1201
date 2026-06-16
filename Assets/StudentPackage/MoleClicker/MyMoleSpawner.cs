using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MyMoleSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] m_spawnPoints;
    [SerializeField] private MyMole m_MolePrefab;

    private IObjectResolver m_Resolver;
    private MoleGameConfig m_GameConfig;
    private MoleGameDirector m_Director;

    private readonly List<MyMole> m_AliveMoles = new List<MyMole>();
    private readonly List<int> m_AvailableSpawnIndexes = new List<int>();

    [Inject]
    public void Constuct(IObjectResolver resolver, MoleGameConfig config, MoleGameDirector director)
    {
        m_Resolver = resolver;
        m_GameConfig = config;
        m_Director = director;

        if (m_Director != null)
        {
            m_Director.RoundStarted += OnRoundStarted;
            m_Director.RoundEnded += OnRoundEnded;
        }
    }


    private void OnDestroy()
    {
        if (m_Director != null)
        {
            m_Director.RoundStarted -= OnRoundStarted;
            m_Director.RoundEnded -= OnRoundEnded;
        }
    }
    private void OnRoundStarted()
    {
        StopAllCoroutines();
        ClearAllMoles();
        ResetAvailableSpawnIndexes();

        if (m_MolePrefab == null || m_Resolver == null)
            return;

        StartCoroutine(StartSpawnRoutine());
    }

    private void OnRoundEnded()
    {
        StopAllCoroutines();
    }
    private void ResetAvailableSpawnIndexes()
    {
        m_AvailableSpawnIndexes.Clear();

        if (m_spawnPoints == null) return;

        for (int i = 0; i < m_spawnPoints.Length; i++)
        {
            m_AvailableSpawnIndexes.Add(i);
        }
    }
    private IEnumerator StartSpawnRoutine()
    {
        for (int i = 0; i < m_GameConfig.MoleCount; i++)
        {
            if (m_Director != null && !m_Director.IsPlaying)
                yield break;

            Spawn();

            yield return new WaitForSeconds(m_GameConfig.StartSpawnInterval);
        }
    }
    private void Spawn()
    {
        if (m_Director != null && !m_Director.IsPlaying) return;

        if(m_spawnPoints == null || m_spawnPoints.Length == 0) return;

        if (m_AvailableSpawnIndexes.Count == 0) return;

        int randomListIndex = Random.Range(0, m_AvailableSpawnIndexes.Count);
        int spawnIndex = m_AvailableSpawnIndexes[randomListIndex];

        m_AvailableSpawnIndexes.RemoveAt(randomListIndex);

        Transform spawnPoint = m_spawnPoints[spawnIndex];

        MyMole mole = m_Resolver.Instantiate(
            m_MolePrefab,
            spawnPoint.position,
            spawnPoint.rotation
            );

        mole.SetSpawnIndex(spawnIndex);

        m_AliveMoles.Add(mole);
        mole.Finished += OnMoleFinished;
    }
    private void OnMoleFinished(MyMole mole)
    {
        mole.Finished -= OnMoleFinished;
        m_AliveMoles.Remove(mole);

        if (!m_AvailableSpawnIndexes.Contains(mole.SpawnIndex))
            m_AvailableSpawnIndexes.Add(mole.SpawnIndex);

        StartCoroutine(RespawnAfterDelay());
    }

    //다 만들고 유니태스크로 수정하기
    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(m_GameConfig.RespawnDelay);

        if (m_Director != null && !m_Director.IsPlaying)
            yield break;

        Spawn();
    }
    private void ClearAllMoles()
    {
        for (int i = m_AliveMoles.Count - 1; i >= 0; i--)
        {
            if (m_AliveMoles[i] != null)
            {
                m_AliveMoles[i].Finished -= OnMoleFinished;
                Destroy(m_AliveMoles[i].gameObject);
            }
        }

        m_AliveMoles.Clear();
    }

}
