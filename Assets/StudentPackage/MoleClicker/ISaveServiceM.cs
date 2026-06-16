using System;
using UnityEngine;

public interface ISaveServiceM
{
    event Action<int> Saved;

    int LoadScore();
    void SaveScore(int score);
}
