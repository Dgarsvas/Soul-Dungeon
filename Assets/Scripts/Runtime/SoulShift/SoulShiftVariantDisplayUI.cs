using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoulShiftVariantDisplayUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI healthMultiplier;
    [SerializeField] private TextMeshProUGUI speedMultiplier;
    [SerializeField] private TextMeshProUGUI damageMultiplier;
    [SerializeField] private TextMeshProUGUI reloadRateMultiplier;

    internal void PopulateFields(SoulShiftTypeScriptableObject variant)
    {
        icon.sprite = variant.icon;
        title.text = variant.title;
        healthMultiplier.text = variant.HealthMutiplier.ToString();
        speedMultiplier.text = variant.SpeedMultiplier.ToString();
        damageMultiplier.text = variant.DamageMutiplier.ToString();
        reloadRateMultiplier.text = variant.ReloadRateMultiplier.ToString();
    }
}
