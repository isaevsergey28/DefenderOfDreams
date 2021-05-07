using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    private Slider _healthSlider;
    private int _maxHealth;
    private Text _healthText;
    private void Awake()
    {
        Player.onPlayerHealthChange += SetHealthBar;
    }

    private void Start()
    {
        _healthSlider = GetComponent<Slider>();
        _healthText = GetComponentInChildren<Text>();
        _maxHealth = (int)_healthSlider.maxValue;
    }
    private void SetHealthBar(float health)
    {
        _healthSlider.value = health;
        SetHealthText((int)_healthSlider.value);
    }

    private void SetHealthText(int currentHealth)
    {
        _healthText.text = currentHealth + "/" + _maxHealth;
    }
}
