using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthBar : MonoBehaviour
{
    private Slider _healthSlider;
    private int _maxHealth;
    private Text _healthText;
    private void Awake()
    {
        Player.onPlayerHealthChange += SetBootstrap;
        Player.onPlayerHealthChange += SetHealthBar;
        _healthSlider = GetComponent<Slider>();
        _healthText = GetComponentInChildren<Text>();
    }

    private void SetBootstrap(float maxHealth)
    {
        _healthSlider.maxValue = maxHealth;
        _healthSlider.value = maxHealth;
        _maxHealth = (int)maxHealth;
        Player.onPlayerHealthChange -= SetBootstrap;
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
