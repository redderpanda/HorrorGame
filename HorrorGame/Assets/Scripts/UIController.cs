using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public PlayerStats playerStats;
    public Slider sanitySlider;
    public Slider healthSlider;
    public Slider batterySlider;
    public Slider staminaSlider;
    public Text ammoText;
    public Image reticle;

	// Use this for initialization
	void Start () {
        sanitySlider.maxValue = 100f;
        healthSlider.maxValue = 8f;
        batterySlider.maxValue = 150f;
        staminaSlider.maxValue = 50f;

		
	}
	
	// Update is called once per frame
	void Update () {
        if (playerStats.stamina > staminaSlider.maxValue)
        {
            playerStats.stamina = staminaSlider.maxValue;
        }
        sanitySlider.value = playerStats.sanity;
        healthSlider.value = playerStats.health;
        batterySlider.value = playerStats.flashlightBattery;
        staminaSlider.value = playerStats.stamina;
        if (playerStats.hasShotgun)
        {
            ammoText.enabled = true;
            ammoText.text = playerStats.ammo.ToString();
            reticle.gameObject.SetActive(true);
        }
        

		
	}

}
