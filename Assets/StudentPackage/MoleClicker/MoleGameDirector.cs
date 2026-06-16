using System;
using UnityEngine;
using VContainer.Unity;

//게임 전체 규칙 담당 (게임 시작, 시간 감소, 게임 종료, 최고 점수 저장)
public class MoleGameDirector : IStartable, ITickable
{
    private readonly IScoreServiceM m_Score;
    private readonly ISaveServiceM m_Save;
    private readonly MoleGameConfig m_Config;

    private float m_RemainingTime;
    private bool m_IsPlaying;
    private int m_HighScore;

    public float RemainingTime => m_RemainingTime;
    public int HighScore => m_HighScore;
    public bool IsPlaying => m_IsPlaying;

    public event Action<float> TimeChanged;
    public event Action<int> HighScoreChanged;
    public event Action RoundStarted;
    public event Action RoundEnded;

    public MoleGameDirector(IScoreServiceM score, ISaveServiceM save, MoleGameConfig config)
    {
        m_Score = score;
        m_Save = save;
        m_Config = config;
    }

    public void Start()
    {
        m_HighScore = m_Save.LoadScore();
        RestartGame();
    }

    public void Tick()
    {
        if (!IsPlaying) return;

        m_RemainingTime -= Time.deltaTime;

        if (m_RemainingTime <= 0f)
        {
            m_RemainingTime = 0f;
            TimeChanged?.Invoke(m_RemainingTime);
            EndGame();
            return;
        }
        TimeChanged?.Invoke(m_RemainingTime);
    }

    public void RestartGame()
    {
        m_IsPlaying = true;
        m_RemainingTime = m_Config.GameDuration;

        m_Score.Restore(0);

        TimeChanged?.Invoke(m_RemainingTime);
        HighScoreChanged?.Invoke(m_HighScore);
        RoundStarted?.Invoke();
    }
    private void EndGame()
    {
        if (!m_IsPlaying) return;

        m_IsPlaying = false;

        if (m_Score.CurrentScore > m_HighScore)
        {
            m_HighScore = m_Score.CurrentScore;
            m_Save.SaveScore(m_HighScore);
            HighScoreChanged?.Invoke(m_HighScore);
        }
        RoundEnded?.Invoke();
    }
}
