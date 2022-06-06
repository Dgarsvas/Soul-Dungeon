using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoulShiftAbsolute", menuName = "SoulShift/Create Soul Variant Absolute Amount", order = 100)]

public class SoulShiftAbsoluteAmountScriptableObject : SoulShiftTypeScriptableObject
{
    [Header("Other")]
    [SerializeField]
    private int totalAmount;

    public override void DoSoulShiftEffect()
    {
    }

    public override float GetSoulShiftProgress()
    {
        return totalAmount - GameState.SoulShiftAmountUsed;
    }
}
