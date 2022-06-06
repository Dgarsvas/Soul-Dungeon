using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState
{
    private static int amountOfKills;
    private static float damageDealt;
    private static int soulShiftAmountUsed;
    private static int levelsPassed;

    private static Dictionary<string, object> dataDictionary;

    public const string CHOSEN_SOUL_VARIANT_KEY = "Soul_Picked";

    public delegate void DamageDealtEvent(float damageDealt);
    public static event DamageDealtEvent OnDamageDealt;

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

    public static float DamageDealt
    {
        get
        {
            return damageDealt;
        }
    }

    public static float LevelsPassed
    {
        get
        {
            return levelsPassed;
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    public static void ResetStatistics()
    {
        amountOfKills = 0;
        soulShiftAmountUsed = 0;
        damageDealt = 0f;
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

    public static void DealDamage(float curDamageDealt)
    {
        damageDealt += Mathf.Max(0f, curDamageDealt);
        OnDamageDealt?.Invoke(Mathf.Max(0f, curDamageDealt));
    }


    public static void SoulShiftUsed()
    {
        soulShiftAmountUsed++;
    }

    public static void LevelPassed()
    {
        levelsPassed++;
    }
}