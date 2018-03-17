using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerMovement : MonoBehaviour {
    public float speed; //good sprint speed is 6.0
    public float gravity;
    public Transform player;
    private Vector3 directionOfPlayer;
    Animator anim;
    AudioSource laugh;
    public bool laughing;
    public bool wasShot;
    public bool idle;
    public float multiplier;

    private void Awake()
    {
        speed = 4.5f;
        gravity = 20f;
        anim = GetComponent<Animator>();
        laugh = GetComponent<AudioSource>();
        wasShot = false;
        idle = false;
        multiplier = 1f;
    }
    // Update is called once per frame
    void Update () {
        FacePlayer();
        FollowPlayer();
        StartCoroutine(GotShot());
    }

    public IEnumerator GotShot()
    {
        anim.SetBool("WasShot", wasShot);
        if (wasShot)
        {
            if (!idle)
            {
                idle = true;
                yield return new WaitForSeconds(3f);
                wasShot = false;
                idle = false;
            }
        }
    }
    public void FacePlayer()
    {
        directionOfPlayer = player.transform.position - transform.position;
        float angle = Vector3.Angle(directionOfPlayer, transform.forward);
        angle -= 90f;
        Quaternion rot = Quaternion.LookRotation(directionOfPlayer);
        rot *= Quaternion.Euler(0, 90, 0);
        this.transform.rotation = Quaternion.Slerp(transform.rotation, rot, 0.1f);

    }
    public void FollowPlayer()
    { 
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        float distance = Vector3.Distance(player.position, transform.position);
        anim.SetFloat("Distance", distance);
        if (distance < 30) // sprint range
        {
            speed = 6.5f;
            anim.SetFloat("Speed", speed);
        }
        else if(distance < 5) //pounce range
        {
            speed = 10.0f;
            anim.SetFloat("Speed", speed);
        }
        else // walking range
        {
            speed = 4.5f;
            anim.SetFloat("Speed", speed);
        }
        if(wasShot)
        {
            multiplier = 0.1f;
        }
        else
        {
            multiplier = 1f;
        }
        speed = speed * multiplier;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            laugh.Play();
        }
    }
}



