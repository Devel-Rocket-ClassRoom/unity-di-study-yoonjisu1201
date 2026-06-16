using System;
using System.Collections;
using UnityEngine;
using VContainer;

//두더지 한 마리 담당 (클릭되면 점수 추가, 자신 파괴, 스포너 전달)
public class MyMole : MonoBehaviour
{
    [SerializeField] private AudioClip m_CollectClip;

    private IScoreServiceM m_Score;
    //오디오서비스 인터페이스
    private MoleGameConfig m_Config;

    private bool m_Collected;
    private bool m_Finished;

    private Vector3 m_UpPosition;
    private Vector3 m_DownPosition;

    public int SpawnIndex { get; private set; }

    public event Action<MyMole> Finished;

    [Inject]
    public void Construct(IScoreServiceM score, MoleGameConfig config)
    {
        m_Score = score;
        m_Config = config;
    }
    private void Start()
    {
        Vector3 basePosition = transform.position;

        m_UpPosition = basePosition + new Vector3(0f, m_Config.MoleUpYOffset, 0f);
        m_DownPosition = m_UpPosition + new Vector3(0f, m_Config.MoleHiddenYOffset, 0f);

        transform.position = m_DownPosition;

        StartCoroutine(MoleLifeRoutine());
    }

    private IEnumerator MoleLifeRoutine()
    {
        yield return MoveTo(m_UpPosition, m_Config.MoleMoveDuration);

        yield return new WaitForSeconds(m_Config.MoleVisibleDuration);

        yield return MoveTo(m_DownPosition, m_Config.MoleMoveDuration);

        Finish();
    }
    private IEnumerator MoveTo(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            yield return null;
        }
        transform.position = targetPosition;
    }
    public void SetSpawnIndex(int index)
    {
        SpawnIndex = index;
    }
    public void Collect()
    {
        if (m_Collected || m_Score == null)
            return;

        m_Collected = true;
        m_Score.AddScore(m_Config.MoleValue);
        Finished?.Invoke(this);
        Destroy(gameObject);
    }
    private void Finish()
    {
        if (m_Finished) return;

        m_Finished = true;

        Finished?.Invoke(this);

        Destroy(gameObject);
    }
}
