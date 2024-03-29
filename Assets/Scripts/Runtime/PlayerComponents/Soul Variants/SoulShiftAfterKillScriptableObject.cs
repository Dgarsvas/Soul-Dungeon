using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoulShiftAfterKill", menuName = "SoulShift/Create Soul Variant After kill", order = 100)]
public class SoulShiftAfterKillScriptableObject : SoulShiftTypeScriptableObject
{
    [Header("Other")]
    [SerializeField]
    private int amountOfKillsNeeded;

    public override float GetSoulShiftProgress()
    {
        return ((float)(GameState.AmountOfKills - GameState.SoulShiftAmountUsed * amountOfKillsNeeded)) / amountOfKillsNeeded;
    }

    public override void DoSoulShiftEffect()
    {
        //Does nothing
    }
}
