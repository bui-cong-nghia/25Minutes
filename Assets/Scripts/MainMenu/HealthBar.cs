﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{

	[SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;
    [SerializeField] private TMP_Text healthText;

	//public void SetMaxHealth(float health)
	//{
	//	slider.maxValue = health;

 //       fill.color = gradient.Evaluate(1f);
	//}

    public void SetHealth(float health, float maxHealth)
	{
		slider.maxValue = maxHealth;
		slider.value = health;
        healthText.text = health.ToString() + " / " + maxHealth.ToString();
        fill.color = gradient.Evaluate(slider.normalizedValue);
	}

}
