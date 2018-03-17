using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerAttack : MonoBehaviour {
    public GameObject player;
    public AudioSource hack;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            hack.Play();
            player.GetComponent<PlayerStats>().health--;
            player.GetComponent<PlayerStats>().injured.Play();
        }
    }
}
