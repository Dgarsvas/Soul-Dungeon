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
    private float healthMultiplier = 1f;
    [SerializeField]
    private float reloadRateMultiplier = 1f;
    [SerializeField]
    private float damageMultiplier = 1f;
    [SerializeField]
    private float speedMultiplier = 1f;
    [SerializeField]
    private SoulShiftActivationType type;

    [Header("Display")]
    public string title;
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

    public float HealthMutiplier => healthMultiplier;

    public float ReloadRateMultiplier => reloadRateMultiplier;

    public float SpeedMultiplier => speedMultiplier;

    public float DamageMutiplier => damageMultiplier;


    public abstract float GetSoulShiftProgress();
    public abstract void DoSoulShiftEffect();
}
