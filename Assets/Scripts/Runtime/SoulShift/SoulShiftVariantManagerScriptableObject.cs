using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoulShiftVariantManager", menuName = "SoulShift/SoulShift Variant Manager", order = 100)]

public class SoulShiftVariantManagerScriptableObject : ScriptableObject
{
    [SerializeField]
    private List<SoulShiftTypeScriptableObject> soulshifts;

    public SoulShiftTypeScriptableObject this[int index]
    {
        get
        {
            return soulshifts[index];
        }
    }

    public int Count
    {
        get
        {
            return soulshifts.Count;
        }
    }
}
