using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    Damageable playerDamageable;
    public Slider healthSlider;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.Log("No player found in the scene, make sure player GameObject has tag 'Player' assigned");
        }
        playerDamageable = player.GetComponent<Damageable>();
    }

    // Start is called before the first frame update
    void Start()
    {
        healthSlider.value = CalculateSliderPercentage(playerDamageable.Health, playerDamageable.MaxHealth);
    }

    private void OnEnable()
    {
        playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }
    private void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    private void OnPlayerHealthChanged(int currentHealth, int maxHealth)
    {
        healthSlider.value = CalculateSliderPercentage(currentHealth, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
