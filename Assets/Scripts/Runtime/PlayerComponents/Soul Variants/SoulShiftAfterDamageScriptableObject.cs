using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoulShiftAfterDamage", menuName = "SoulShift/Create Soul Variant After Damage", order = 100)]

public class SoulShiftAfterDamageScriptableObject : SoulShiftTypeScriptableObject
{
    [Header("Other")]
    [SerializeField]
    private int damageNeeded;

    public override void DoSoulShiftEffect()
    {
    }

    public override float GetSoulShiftProgress()
    {
        Debug.Log($"damage needed {(GameState.DamageDealt - GameState.SoulShiftAmountUsed * damageNeeded)} / {damageNeeded}");
        return ((float)(GameState.DamageDealt - GameState.SoulShiftAmountUsed * damageNeeded)) / damageNeeded;
    }
}
