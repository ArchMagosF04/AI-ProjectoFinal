using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newRoulette", menuName = "Data/Roulette/OnLeaderDeath", order = 0)]
public class LeaderDeathRoulette : ScriptableObject
{
    [Header("Base Outcome Chances")]
    [SerializeField, Range(0f, 1f)] private float inspireBuff = 0.2f;
    public float InspireBuff => inspireBuff;
    [SerializeField, Range(0f, 1f)] private float keepFighting = 0.6f;
    public float KeepFighting => keepFighting;
    [SerializeField, Range(0f, 1f)] private float fleeInTerror = 0.2f;
    public float FleeInTerror => fleeInTerror;

    [Header("Health Thresholds")]
    [SerializeField, Range(0f, 1f)] private float upperHealthThreshold = 0.8f;
    public float UpperHealthThreshold => upperHealthThreshold;
    [SerializeField, Range(0f, 1f)] private float lowerHealthThreshold = 0.3f;
    public float LowerHealthThreshold => lowerHealthThreshold;

    [Header("On High Modifier")]
    [SerializeField, Range(0f, 3f)] private float inspireHighMod = 2.5f;
    public float InspireHighMod => inspireHighMod;
    [SerializeField, Range(0f, 3f)] private float fightHighMod = 0.66f;
    public float FightHighMod => fightHighMod;
    [SerializeField, Range(0f, 3f)] private float fleeHighMod = 0.5f;
    public float FleeHighMod => fleeHighMod;

    [Header("On Low Modifier")]
    [SerializeField, Range(0f, 3f)] private float inspireLowMod = 0.5f;
    public float InspireLowMod => inspireLowMod;
    [SerializeField, Range(0f, 3f)] private float fightLowMod = 0.66f;
    public float FightLowMod => fightLowMod;
    [SerializeField, Range(0f, 3f)] private float fleeLowMod = 2.5f;
    public float FleeLowMod => fleeLowMod;
}
