using System.Collections;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MyMoleSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] m_spawnPoints;
    [SerializeField] private MyMole m_MolePrefab;

    private IObjectResolver m_Resolver;
    private MoleGameConfig m_GameConfig;

    [Inject]
    public void Constuct(IObjectResolver resolver, MoleGameConfig config)
    {
        m_Resolver = resolver;
        m_GameConfig = config;
    }
    private void Start()
    {
        if (m_MolePrefab == null || m_Resolver == null) return;

        for (int i = 0; i < m_GameConfig.MoleCount; i++)
            Spawn();
    }
    private void Spawn()
    {
        if(m_spawnPoints == null || m_spawnPoints.Length == 0) return;

        int index = Random.Range(0, m_spawnPoints.Length);
        Transform spawnPoint = m_spawnPoints[index];

        MyMole mole = m_Resolver.Instantiate(
            m_MolePrefab,
            spawnPoint.position,
            spawnPoint.rotation
            );

        mole.Collected += OnMoleCollected;
    }
    private void OnMoleCollected(MyMole mole)
    {
        mole.Collected -= OnMoleCollected;
        StartCoroutine(RespawnAfterDelay());
    }

    //다 만들고 유니태스크로 수정하기
    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(m_GameConfig.RespawnDelay);
        Spawn();
    }

}
