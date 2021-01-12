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

    private bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnFire()
    {
        if (canShoot)
        {
            canShoot = false;
            StartCoroutine("CoolDown");
            

            Vector3 rotation = transform.parent.localScale.x == 1 ? Vector3.zero : Vector3.forward * 180;
            GameObject projectile = (GameObject)Instantiate(ProjectileTypes[itemType], transform.position * transform.parent.localScale.x, Quaternion.Euler(rotation));

            projectile.GetComponent<Rigidbody2D>().velocity = transform.parent.localScale.x * Vector2.right * 100f;

            /*
                projectile.GetComponent<Rigidbody2D>().isKinematic = false;

                projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.parent.localScale.x, 1) * wpn.projectileSpeed;
            */
        }
        Debug.Log("Firing");
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(0.2f);
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
