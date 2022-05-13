using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoulShiftAfterKill", menuName = "SoulShift/Create SoulShift", order = 100)]
public class SoulShiftAfterKillScriptableObject : SoulShiftTypeScriptableObject
{
    [Header("Other")]
    [SerializeField]
    private int amountOfKillsNeeded;

    public override float GetSoulShiftProgress()
    {
        return ((float)(CurrentStatistics.AmountOfKills - CurrentStatistics.SoulShiftAmountUsed * amountOfKillsNeeded)) / amountOfKillsNeeded;
    }

    public override void DoSoulShiftEffect()
    {
        //Does nothing
    }
}
