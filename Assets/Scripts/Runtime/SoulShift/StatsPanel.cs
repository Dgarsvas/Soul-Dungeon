using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsPanel : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI damageText;
    [SerializeField]
    private TextMeshProUGUI healthText;
    [SerializeField]
    private TextMeshProUGUI speedText;

    public void SetStats((float damage, float health, float speed) statsTuple)
    {
        damageText.text = statsTuple.damage.ToString();
        healthText.text = statsTuple.health.ToString();
        speedText.text = statsTuple.speed.ToString();
    }
}
