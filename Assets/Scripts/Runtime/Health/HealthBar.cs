using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    private HealthController healthRef;

    public void Initialize(HealthController health)
    {
        healthRef = health;
        slider.maxValue = healthRef.startingHealth;
        slider.value = healthRef.Health;
        healthRef.OnDamageTaken.AddListener(DamageTaken);
    }

    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(healthRef.transform.position);
    }

    private void OnDestroy()
    {
        healthRef.OnDamageTaken.RemoveListener(DamageTaken);
    }

    private void DamageTaken(float damageAmount, Vector3 dir)
    {
        slider.value = healthRef.Health;
    }
}
