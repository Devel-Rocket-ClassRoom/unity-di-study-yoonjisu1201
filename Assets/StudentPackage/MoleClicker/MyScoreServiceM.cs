using System;
using UnityEngine;

//현재 점수 담당 (점수 증가, 초기화)
public class MyScoreServiceM : IScoreServiceM
{
    public int CurrentScore { get; private set; }

    public event Action<int> ScoreChanged;

    public void AddScore(int score)
    {
        if (score == 0) return;

        CurrentScore += score;
        ScoreChanged?.Invoke(CurrentScore);
    }

    public void Restore(int value)
    {
        CurrentScore = value;
        ScoreChanged?.Invoke(CurrentScore);
    }
}
    

