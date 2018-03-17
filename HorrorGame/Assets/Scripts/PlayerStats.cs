using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    public float sanity;
    public int health;

    public Light flashlight;
    bool isFlashlightOn;
    public float flashlightBattery;
    public float stamina;

    public bool hasShotgun;
    public bool isFalling;
    public int ammo;
    public PauseController pause_controller;
    public PlayerMovement playerMovement;

    public AudioSource injured;
    public AudioSource fall;


	// Use this for initialization
	void Start () {
        sanity = 100f;
        health = 8;
        isFlashlightOn = true;
        flashlightBattery = 150f;
        hasShotgun = false;
        ammo = 0;
        isFalling = false;
        stamina = 50f;
        playerMovement = GetComponent<PlayerMovement>();
		
	}
	
	// Update is called once per frame
	void Update () {
        GoingInsane();
        Flashlight();
        StartCoroutine(BatteryDrain());
        Death();
		
	}

    public void GoingInsane() // decrement health for when there is no light
    {
        if(!isFlashlightOn)
        {
            sanity-= Time.deltaTime;
            //sanity--; // for faster testing
        }
    }

    public void Flashlight()  // toggle on/off the flashlight 
    {
        if (Input.GetKey(KeyCode.F))
        {
            if (isFlashlightOn)
            {
                flashlight.enabled = false;
                isFlashlightOn = false;
            }
            else
            {
                if (flashlightBattery > 0)
                {
                    flashlight.enabled = true;
                    isFlashlightOn = true;
                }
            }
        }
    }

    public IEnumerator BatteryDrain() // drain the battery of the flashlight if on
    {
        if (isFlashlightOn)
        {
            flashlightBattery -= Time.deltaTime;
            //flashlightBattery--; // for faster testing
            yield return new WaitForSeconds(1f);
        }
        if (flashlightBattery <= 0)
        {
            isFlashlightOn = false;
            flashlight.enabled = false;
            flashlightBattery = 0;
        }
    }
    
    public void Death() // handler for death conditions
    {
        if(health <= 0 || sanity <= 0)
        {
            pause_controller.paused = true;
            pause_controller.quitButton.gameObject.SetActive(true);
            pause_controller.pauseText.text = "You Died";
            pause_controller.pauseText.gameObject.SetActive(true);
            pause_controller.restartButton.gameObject.SetActive(true);
            if(playerMovement.running)
            {
                playerMovement.run.Pause();
            }
            else
            {
                playerMovement.walk.Pause();
            }

        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Fall Zone") // died
        {
            if (!isFalling)
            {
                Falling();
            }
        }
    }
    public void Falling()
    {
        isFalling = true;
        Debug.Log("you fell to your death");
        if (playerMovement.running)
        {
            playerMovement.run.Pause();
        }
        else if (playerMovement.walking)
        {
            playerMovement.walk.Pause();
        }
        fall.Play();
        pause_controller.paused = true;
        pause_controller.quitButton.gameObject.SetActive(true);
        pause_controller.pauseText.text = "You Died";
        pause_controller.pauseText.gameObject.SetActive(true);
        pause_controller.restartButton.gameObject.SetActive(true);

    }
}
