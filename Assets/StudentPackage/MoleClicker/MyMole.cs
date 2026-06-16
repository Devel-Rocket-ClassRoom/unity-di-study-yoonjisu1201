using DIStudy.CoinClicker.Student;
using System;
using UnityEngine;
using VContainer;

public class MyMole : MonoBehaviour
{
    [SerializeField] private AudioClip m_CollectClip;

    private IScoreServiceM m_Score;
    //오디오서비스 인터페이스
    private MoleGameConfig m_Config;
    private bool m_Collected;

    public event Action<MyMole> Collected;

    [Inject]
    public void Construct(IScoreServiceM score, MoleGameConfig config)
    {
        m_Score = score;
        m_Config = config;
    }

    public void Collect()
    {
        if (m_Collected || m_Score == null)
            return;

        m_Collected = true;
        m_Score.AddScore(m_Config.MoleValue);
        Collected?.Invoke(this);
        Destroy(gameObject);
    }
}
