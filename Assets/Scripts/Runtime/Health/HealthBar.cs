using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    private HealthController healthRef;
    private bool isShown;
    private float showTimer;
    private const float SHOW_DURATION = 2f;

    public void Initialize(HealthController health)
    {
        healthRef = health;
        slider.maxValue = healthRef.startingHealth;
        slider.value = healthRef.Health;
        healthRef.OnDamageTaken.AddListener(DamageTaken);
    }

    private void Update()
    {
        if (showTimer <= 0f)
        {
            isShown = false;
            gameObject.SetActive(false);
        }
        if (isShown)
        {
            transform.position = Camera.main.WorldToScreenPoint(healthRef.transform.position);
            showTimer -= Time.deltaTime;
        }
    }

    private void OnDestroy()
    {
        healthRef.OnDamageTaken.RemoveListener(DamageTaken);
    }

    private void DamageTaken(float damageAmount, Vector3 dir)
    {
        gameObject.SetActive(true);
        isShown = true;
        showTimer = SHOW_DURATION;
        slider.value = healthRef.Health;
    }
}