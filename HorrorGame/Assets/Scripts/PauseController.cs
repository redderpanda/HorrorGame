using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour {

    public bool paused;
    public Button pauseButton;
    public Button quitButton;
    public Button restartButton;
    public Text pauseText;
    public AudioSource aud;

    // Use this for initialization
    void Start () {
        paused = false;
        aud = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        PauseScene();
    }


    public void PauseScene()
    {
        
        if (Input.GetKey(KeyCode.Escape))
        {
            paused = true;
            pauseButton.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
            pauseText.gameObject.SetActive(true);
            aud.GetComponent<AudioSource>().Pause();

        }
        if (paused)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            
        }
        else if (!paused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
    }

    public void Unpause()
    {
        paused = false;
        pauseButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        pauseText.gameObject.SetActive(false);
        aud.GetComponent<AudioSource>().UnPause();
    }
}
