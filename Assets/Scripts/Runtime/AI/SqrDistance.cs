using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SqrDistance 
{
    [SerializeField]
    private float actualDistance;
    [SerializeField]
    private float sqrDistance = float.MaxValue;

    public float DistanceSqr
    {
        get
        {
            return sqrDistance;
        }
    }

    public float Distance
    {
        get
        {
            return actualDistance;
        }
    }
}
