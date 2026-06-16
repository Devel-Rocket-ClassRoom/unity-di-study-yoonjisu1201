using System;
using UnityEngine;

public interface IScoreServiceM
{
    int CurrentScore { get; }

    event Action<int> ScoreChanged;

    void AddScore(int score);

    void Restore(int value);
}
