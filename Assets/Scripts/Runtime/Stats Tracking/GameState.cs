using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState
{
    private static int amountOfKills;
    private static int soulShiftAmountUsed;

    private static Dictionary<string, object> dataDictionary;

    public const string CHOSEN_SOUL_VARIANT_KEY = "Soul_Picked";

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
        dataDictionary = new Dictionary<string, object>();
    }

    public static void SetData(object createdData, string key)
    {
        if (dataDictionary.ContainsKey(key))
        {
            dataDictionary[key] = createdData;
            return;
        }
        else
        {
            dataDictionary.Add(key, createdData);
        }
    }

    public static object GetData(string key, object defaultValue = null)
    {
        if (dataDictionary.TryGetValue(key, out object createdData))
        {
            return createdData;
        }
        else if (defaultValue != null)
        {
            dataDictionary.Add(key, defaultValue);
            return defaultValue;
        }
        else
        {
            return null;
        }
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