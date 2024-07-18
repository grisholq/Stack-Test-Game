using System;
using UnityEngine;

[Serializable]
public struct ResourceProduction
{
    [HideInInspector] public float NextProductionTime;
    public float SecondsPerResourceProduction;
    public Transform ProductionPoint;

    public bool Produced(float time) => time >= NextProductionTime;
    public void SetNextProductionTime(float currentTime) => NextProductionTime = currentTime + SecondsPerResourceProduction;
}