using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed;
    public float sprintSpeed;
    public float sensitivity;
    public float jumpSpeed;
    public bool notAquiredShotgun;
    public bool wasWalking;
    public bool hasJumped;

    CharacterController player;
    public GameObject eyes;
    public GameObject shotgun;
    public GameObject shotgunPrefab;
    public PlayerStats playerStats;
    public AudioSource walk;
    public AudioSource run;
    public AudioSource pickup;

    public bool running;
    public bool walking;

    float forwardBack;
    float leftRight;
    float upDown;
    float vertVelocity;


    float rotationX;
    float rotationY;

    // Use this for initialization
    void Start() {

        speed = 4.5f;
        sprintSpeed = 13f; // 12 is decent number
        sensitivity = 5f;
        jumpSpeed = 5f;
        player = GetComponent<CharacterController>();
        notAquiredShotgun = true;
        walk.Play();
        run.Play();
        running = true;
        walking = true;

    }

    // Update is called once per frame
    void Update() {
        if (Time.timeScale == 1)
        {
            Movement();
            if(Input.GetButtonDown("Jump"))
            {
                hasJumped = true;
            }
            ApplyGravity();
        }


    }
    public void Movement()
    {
        if (Input.GetAxis("Vertical") == 0 && (Input.GetAxis("Horizontal") == 0)) // not moving
        {
            if (walking)
            {
                walk.Pause();
                walking = false;
            }
            if (running)
            {
                run.Pause();
                running = false;
            }

        }
        else if (Input.GetKey(KeyCode.LeftShift)) //running
        {
            if (playerStats.stamina > 0)
            {
                forwardBack = Input.GetAxis("Vertical") * sprintSpeed;
                leftRight = Input.GetAxis("Horizontal") * sprintSpeed;
                if (walking)
                {
                    walking = false;
                    walk.Pause();
                }
                running = true;
                run.UnPause();
                playerStats.stamina -= 0.1f;
            }
            else
            {
                forwardBack = Input.GetAxis("Vertical") * speed;
                leftRight = Input.GetAxis("Horizontal") * speed;
                if (running)
                {
                    running = false;
                    run.Pause();
                }
                walking = true;
                walk.UnPause();
                playerStats.stamina += 0.05f;
            }
        }
        else // walking
        {
            forwardBack = Input.GetAxis("Vertical") * speed;
            leftRight = Input.GetAxis("Horizontal") * speed;
            if (running)
            {
                running = false;
                run.Pause();
            }
            walking = true;
            walk.UnPause();
            playerStats.stamina += 0.15f;

        }


        rotationX = Input.GetAxis("Mouse X") * sensitivity;
        rotationY -= Input.GetAxis("Mouse Y") * sensitivity;
        rotationY = Mathf.Clamp(rotationY, -60f, 60f);
        Vector3 movement;

        movement = new Vector3(leftRight, vertVelocity, forwardBack);
        transform.Rotate(0, rotationX, 0);
        eyes.transform.localRotation = Quaternion.Euler(rotationY, 0, 0);
        

        movement = transform.rotation * movement;
        player.Move(movement * Time.deltaTime);
        if (Input.GetKey(KeyCode.C))
        {
            transform.localScale = new Vector3(1.5f, 1.0f, 1.5f);

        }
        else
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }

    }

    private void ApplyGravity()
    {
        if(player.isGrounded == true)
        {
            if(hasJumped == false)
            {
                vertVelocity = -20;
            }
            else
            {
                vertVelocity = jumpSpeed;
            }
        }
        else
        {
            vertVelocity += -20 * Time.deltaTime;
            vertVelocity = Mathf.Clamp(vertVelocity, -100f, jumpSpeed);
            hasJumped = false;
        }
    }



    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.tag == "Shotgun") // found shotgun
        {
            if (notAquiredShotgun)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    pickup.Play();
                    Debug.Log("shotgun acquired");
                    playerStats.hasShotgun = true;
                    playerStats.ammo += 6;  //can changed to random number maybe?
                    shotgun = Instantiate(shotgunPrefab, eyes.transform.position, eyes.transform.rotation) as GameObject;
                    shotgun.transform.parent = eyes.transform;
                    shotgun.transform.Translate(new Vector3(0.37f, -0.37f, 0.33f));
                    shotgun.transform.Rotate(new Vector3(0, 90, 0));
                    notAquiredShotgun = false;
                    Destroy(other.gameObject);


                }
            }

        }
        else if (other.gameObject.tag == "Shells") // found shotgun ammo
        {
            if (Input.GetKey(KeyCode.E))
            {
                pickup.Play();
                Debug.Log("shells acquired");
                playerStats.ammo += 4; //change to random number later
                Destroy(other.gameObject);
            }
        }
        else if (other.gameObject.tag == "Batteries") // found batteries
        {
            if (Input.GetKey(KeyCode.E))
            {
                pickup.Play();
                Debug.Log("batteries acquired");
                playerStats.flashlightBattery = 150;
                Destroy(other.gameObject);
            }
        }
        else if (other.gameObject.tag == "Teddy") // found teddybear
        {
            if (Input.GetKey(KeyCode.E))
            {
                pickup.Play();
                Debug.Log("teddy acquired");
                playerStats.sanity = 100;
                Destroy(other.gameObject);
            }
        }

    }


}
