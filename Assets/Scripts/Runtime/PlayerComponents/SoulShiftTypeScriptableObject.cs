using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoulShiftActivationType
{
    Kills,
    LevelsPassed,
    DamageDealt,
    Time
}

public abstract class SoulShiftTypeScriptableObject : ScriptableObject
{
    [Header("Base properties")]
    [SerializeField]
    private float healthMutiplier = 1f;
    [SerializeField]
    private float attackSpeedMultiplier = 1f;
    [SerializeField]
    private float movementSpeedMultiplier = 1f;
    [SerializeField]
    private SoulShiftActivationType type;

    [Header("Display")]
    public Sprite icon;
    [Multiline]
    public string description;


    public SoulShiftActivationType Type
    {
        get
        {
            return type;
        }
    }

    public abstract float GetSoulShiftProgress();
    public abstract void DoSoulShiftEffect();
}
