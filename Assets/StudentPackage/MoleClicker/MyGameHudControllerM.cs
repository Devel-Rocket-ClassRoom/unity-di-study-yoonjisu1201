using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class MyGameHudControllerM : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_ScoreText;
    [SerializeField] private TextMeshProUGUI m_TimerText;
    [SerializeField] private TextMeshProUGUI m_hScoreText;

    [SerializeField] private Button m_RestartButton;

    private IScoreServiceM m_Score;
    private ISaveServiceM m_Save;

    [Inject]
    public void Construct(IScoreServiceM score, ISaveServiceM save)
    {
        m_Score = score;
        m_Save = save;
    }
    private void Start()
    {
        if (m_Score == null) return;
        m_Score.ScoreChanged += OnScoreChanged;
        OnScoreChanged(m_Score.CurrentScore);
    }

    private void OnDestroy()
    {
        if (m_Score != null)
            m_Score.ScoreChanged -= OnScoreChanged;
    }
    private void OnScoreChanged(int score)
    {
        if (m_ScoreText != null)
        {
            m_ScoreText.text = $"점수: {score}";
        }
    }
}
