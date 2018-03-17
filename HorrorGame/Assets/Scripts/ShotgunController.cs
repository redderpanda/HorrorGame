using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunController : MonoBehaviour {
    public PlayerStats playerStats;
    public Animation anim;
    public GameObject smokeParticles;
    public Transform muzzlePosition;
    public int range;   //range of shotgun
    public GameObject monster;
    public AudioSource gunShot;
    public AudioSource monsterInjured;

    public bool canShootAgain = true;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animation>();
        range = 15;
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        StartCoroutine(Shoot());
		
	}

    public IEnumerator Shoot()
    {
        if (playerStats.hasShotgun)
        {
            if (canShootAgain)
            {
                if (Input.GetButton("Fire1"))
                {
                    if (playerStats.ammo > 0)
                    {
                        canShootAgain = false;
                        anim.Play();
                        gunShot.Play();
                        Debug.Log("firing!");
                        playerStats.ammo--;
                        if (HitMonster())
                        {
                            Debug.Log("hit monster");
                            monsterInjured.Play();
                            monster.GetComponent<CrawlerMovement>().wasShot = true;
                            //monster.GetComponent<CrawlerMovement>().speed = 0;
                        }
                        Instantiate(smokeParticles, muzzlePosition.position, muzzlePosition.rotation);
                        yield return new WaitForSeconds(1f);
                        canShootAgain = true;
                    }
                }
            }
        }
    }

    public bool HitMonster() // function to check where shotgun fires using raycasting
    {
        RaycastHit hit1;
        RaycastHit hit2;
        RaycastHit hit3;
        RaycastHit hit4;
        RaycastHit hit5;
        Debug.DrawRay(muzzlePosition.position, muzzlePosition.transform.forward * range, Color.green, 10f);
        Debug.DrawRay(muzzlePosition.position, (muzzlePosition.transform.forward + new Vector3(0, 0, 0.5f)) * range, Color.green, 10f);
        Debug.DrawRay(muzzlePosition.position, (muzzlePosition.transform.forward + new Vector3(0, 0.1f, 0)) * range, Color.green, 10f);
        Debug.DrawRay(muzzlePosition.position, (muzzlePosition.transform.forward + new Vector3(-0.1f, 0, 0)) * range, Color.green, 10f);
        Debug.DrawRay(muzzlePosition.position, (muzzlePosition.transform.forward + new Vector3(0.5f, 0.5f, 0.1f)) * range, Color.green, 10f);
        //for debugging and testing where lines of fire are
        if (Physics.Raycast(muzzlePosition.position, muzzlePosition.transform.forward, out hit1, range))
        {
            if (hit1.collider.gameObject.tag == "Monster")
            {
                return true;
            }
        }
        if (Physics.Raycast(muzzlePosition.position, muzzlePosition.transform.forward + new Vector3(0,0,0.5f), out hit2, range))
        {
            if (hit2.collider.gameObject.tag == "Monster")
            {
                return true;
            }
        }
        if (Physics.Raycast(muzzlePosition.position, muzzlePosition.transform.forward + new Vector3(0, 0.1f, 0), out hit3, range))
        {
            if (hit3.collider.gameObject.tag == "Monster")
            {
                return true;
            }
        }
        if (Physics.Raycast(muzzlePosition.position, muzzlePosition.transform.forward + new Vector3(-0.1f, 0, 0), out hit4, range))
        {
            if (hit4.collider.gameObject.tag == "Monster")
            {
                return true;
            }
        }
        if (Physics.Raycast(muzzlePosition.position, muzzlePosition.transform.forward + new Vector3(0.5f, 0.5f, 0.1f), out hit5, range))
        {
            if (hit5.collider.gameObject.tag == "Monster")
            {
                return true;
            }
        }

        return false;

    }

}
