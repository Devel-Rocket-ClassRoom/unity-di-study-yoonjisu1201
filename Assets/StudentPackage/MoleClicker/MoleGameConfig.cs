using System;
using UnityEngine;

public class MoleGameConfig
{
    [SerializeField]
    private int m_MoleValue = 1;

    [SerializeField]
    private float m_AutoSaveInterval = 10f;

    [SerializeField]
    private int m_MoleCount = 3;

    [SerializeField]
    private float m_RespawnDelay = 1f;

    public int MoleValue => m_MoleValue;
    public float AutoSaveInterval => m_AutoSaveInterval;
    public int MoleCount => m_MoleCount;
    public float RespawnDelay => m_RespawnDelay;
}
