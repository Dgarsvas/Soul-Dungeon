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
        slider.maxValue = healthRef.currentMaxHealth;
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

    public void SafeDestroy()
    {
        healthRef.OnDamageTaken.RemoveListener(DamageTaken);
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }

    private void DamageTaken(float damageAmount, Vector3 dir)
    {
        gameObject.SetActive(true);
        isShown = true;
        showTimer = SHOW_DURATION;
        slider.value = healthRef.Health;
    }
}
