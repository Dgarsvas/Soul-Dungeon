using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SqrDistance 
{
    [SerializeField]
    private float actualDistance;

    public float DistanceSqr
    {
        get
        {
            if (sqrDistance == float.MaxValue)
            {
                sqrDistance = actualDistance * actualDistance;
            }

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

    private float sqrDistance = float.MaxValue;
}
