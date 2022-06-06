using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoulShiftAfterLevel", menuName = "SoulShift/Create Soul Variant After Level", order = 100)]

public class SoulShiftAfterLevelScriptableObject : SoulShiftTypeScriptableObject
{
    [Header("Other")]
    [SerializeField]
    private int levelsNeeded;

    public override void DoSoulShiftEffect()
    {
    }

    public override float GetSoulShiftProgress()
    {
        return ((float)(GameState.LevelsPassed - GameState.SoulShiftAmountUsed * levelsNeeded)) / levelsNeeded;
    }
}
