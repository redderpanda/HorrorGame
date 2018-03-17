using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarecrowMovement : MonoBehaviour {
    public Transform player;
    public PlayerStats playerStats;
    private Vector3 directionOfPlayer;
    public Camera cam;
    private Plane[] planes;
    public BoxCollider self;
    public float speed;
    //public AudioSource hack;

	// Use this for initialization
	void Start () {
        self = GetComponent<BoxCollider>();
        speed = 7.5f;
	}
	
	// Update is called once per frame
	void Update () {
        planes = GeometryUtility.CalculateFrustumPlanes(cam);
        float distance = Vector3.Distance(player.position, transform.position);
        if (GeometryUtility.TestPlanesAABB(planes, self.bounds)) // can see the scarecrow
        {
            //Debug.Log("has been detected");
            if (distance < 20f) //in a certain range
            {
                playerStats.sanity-= Time.deltaTime;
            }
        }
        else {
            if (distance < 75f)
            {
                FacePlayer();
                TowardPlayer();
            }
            //Debug.Log("not detected");
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

    public void TowardPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

}
