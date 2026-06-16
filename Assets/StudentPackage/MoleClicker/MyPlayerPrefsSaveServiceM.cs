using System;
using UnityEngine;

public class MyPlayerPrefsSaveServiceM : ISaveServiceM
{
    private const string ScoreKey = "MoleClicker.Score";

    public event Action<int> Saved;

    public int LoadScore()
    {
        return PlayerPrefs.GetInt(ScoreKey, 0);
    }

    public void SaveScore(int score)
    {
        PlayerPrefs.SetInt(ScoreKey, score);
        PlayerPrefs.Save();
        Saved?.Invoke(score);
    }
}
