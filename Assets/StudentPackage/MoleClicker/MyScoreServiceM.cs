using System;
using UnityEngine;

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
    

