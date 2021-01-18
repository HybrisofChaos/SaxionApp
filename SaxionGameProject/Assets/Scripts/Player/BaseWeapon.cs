using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    private enum Class: int
    {
        Warrior = 0,
        Mage = 1,
        Ranger = 2
    }

    private enum itemTypes: int
    {
        Neutral = 0,
        Fire = 1,
        Water = 2,
        Electric = 3
    }

    public GameObject[] ProjectileTypes = new GameObject[4];
    
    
    public int playerClass;
    public int itemType;

    private bool canShoot = true;
    private IEnumerator coolDown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnFire() // On Fire Normal
    {
        if (canShoot)
        {



            Vector3 rotation = transform.localScale.x == 1 ? Vector3.zero : Vector3.forward * 180;
            GameObject projectile = (GameObject)Instantiate(ProjectileTypes[itemType], transform.position * transform.localScale.x, Quaternion.Euler(rotation));
            weaponStats stats = projectile.GetComponent<weaponStats>();
            projectile.GetComponent<Rigidbody2D>().velocity = transform.localScale.x * Vector2.right * stats.projectileSpeed;

            /*
                projectile.GetComponent<Rigidbody2D>().isKinematic = false;

                projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.parent.localScale.x, 1) * wpn.projectileSpeed;
            */

            canShoot = false;
            coolDown = CoolDown(stats.coolDown);
            StartCoroutine(coolDown);
            Destroy(projectile, 3f);
        }
  /*      Debug.Log("Firing");
        Debug.Log(canShoot); */
    }

    public void OnSpecialFire() // On Fire Special
    {
        // nil
    }


    IEnumerator CoolDown(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
