using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState
{
    private static int amountOfKills;
    private static int soulShiftAmountUsed;

    public static int AmountOfKills
    {
        get
        {
            return amountOfKills;
        }
    }

    public static int SoulShiftAmountUsed
    {
        get
        {
            return soulShiftAmountUsed;
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    public static void ResetStatistics()
    {
        amountOfKills = 0;
        soulShiftAmountUsed = 0;
    }

    public static void EnemyKilled()
    {
        amountOfKills++;
    }

    public static void SoulShiftUsed()
    {
        soulShiftAmountUsed++;
    }
}
