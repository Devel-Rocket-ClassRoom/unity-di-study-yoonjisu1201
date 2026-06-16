using System;
using UnityEngine;

public class MoleGameConfig
{
    [SerializeField]
    private int m_MoleValue = 1;

    [SerializeField]
    private float m_GameDuration = 30f;

    [SerializeField]
    private int m_MoleCount = 3;

    [SerializeField]
    private float m_RespawnDelay = 1f;

    //올라오거나 내려가는 데 걸리는 시간
    [SerializeField]
    private float m_MoleMoveDuration = 0.3f;

    //올라와서 멈추는 시간
    [SerializeField]
    private float m_MoleVisibleDuration = 1.7f;

    [SerializeField]
    private float m_MoleUpYOffset = 1.2f;

    [SerializeField]
    private float m_MoleHiddenYOffset = -1.0f;

    [SerializeField]
    private float m_StartSpawnInterval = 0.8f;

    public int MoleValue => m_MoleValue;
    public float GameDuration => m_GameDuration;
    public int MoleCount => m_MoleCount;
    public float RespawnDelay => m_RespawnDelay;
    public float MoleMoveDuration => m_MoleMoveDuration;
    public float MoleVisibleDuration => m_MoleVisibleDuration;
    public float MoleUpYOffset => m_MoleUpYOffset;
    public float MoleHiddenYOffset => m_MoleHiddenYOffset;

    public float StartSpawnInterval => m_StartSpawnInterval;
}
