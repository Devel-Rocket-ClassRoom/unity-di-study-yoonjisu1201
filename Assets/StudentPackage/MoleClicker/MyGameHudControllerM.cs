using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

//화면 표시 담당 
public class MyGameHudControllerM : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_ScoreText;
    [SerializeField] private TextMeshProUGUI m_TimerText;
    [SerializeField] private TextMeshProUGUI m_hScoreText;

    [SerializeField] private Button m_RestartButton;

    private IScoreServiceM m_Score;
    private MoleGameDirector m_Director;

    [Inject]
    public void Construct(IScoreServiceM score, MoleGameDirector director)
    {
        m_Score = score;
        m_Director = director;
    }
    private void Start()
    {
        if (m_Score != null)
        {
            m_Score.ScoreChanged += OnScoreChanged;
            OnScoreChanged(m_Score.CurrentScore);
        }

        if (m_Director != null)
        {
            m_Director.TimeChanged += OnTimeChanged;
            m_Director.HighScoreChanged += OnHighScoreChanged;

            OnTimeChanged(m_Director.RemainingTime);
            OnHighScoreChanged(m_Director.HighScore);
        }

        if (m_RestartButton != null)
        {
            m_RestartButton.onClick.AddListener(OnRestartClicked);
        }
    }

    private void OnDestroy()
    {
        if (m_Score != null)
            m_Score.ScoreChanged -= OnScoreChanged;

        if (m_Director != null)
        {
            m_Director.TimeChanged -= OnTimeChanged;
            m_Director.HighScoreChanged -= OnHighScoreChanged;
        }

        if (m_RestartButton != null)
            m_RestartButton.onClick.RemoveListener(OnRestartClicked);
    }
    private void OnScoreChanged(int score)
    {
        if (m_ScoreText != null)
            m_ScoreText.text = $"점수: {score}";
    }
    private void OnTimeChanged(float time)
    {
        if (m_TimerText != null)
            m_TimerText.text = $"시간: {Mathf.CeilToInt(time)}"; //소수점 올림
    }
    private void OnHighScoreChanged(int highScore)
    {
        if (m_hScoreText != null)
            m_hScoreText.text = $"최고 점수: {highScore}";
    }
    private void OnRestartClicked()
    {
        if (m_Director == null) return;

        m_Director.RestartGame();
    }
}
