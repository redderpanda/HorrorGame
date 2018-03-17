using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {
    public PauseController pause_controller;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(Input.GetKey(KeyCode.E))
            {
                //Debug.Log("You Escaped");
                //Time.timeScale = 0;
                pause_controller.paused = true;
                pause_controller.pauseText.text = "You Escaped";
                pause_controller.pauseText.gameObject.SetActive(true);
                pause_controller.quitButton.gameObject.SetActive(true);
                pause_controller.restartButton.gameObject.SetActive(true);
                

            }
        }
    }
}
