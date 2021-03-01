using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [SerializeField]
    private float shootRate = 0.5f;
    private Animator anim;

    public enum ItemTypes : int
    {
        NEUTRAL = 0,
        FIRE = 1,
        WATER = 2,
        ELECTRIC = 3
    }

    public GameObject[] projectileTypes = new GameObject[4];

    public ItemTypes itemType;

    private bool canShoot = true;

    public void Fire(Quaternion rotation) // On Fire Normal
    {
        if (canShoot)
        {
            //			print(transform.Find("aim_arrow_anchor").Find("aim_arrow"));
            //			Projectile projectile = Instantiate(projectileTypes[(int)itemType], transform.position + transform.right * 0.5f + new Vector3(0, 1f, 0), rotation).GetComponent<Projectile>();
            Projectile projectile = Instantiate(projectileTypes[(int)itemType], transform.Find("aim_arrow_anchor").Find("aim_arrow").position, rotation).GetComponent<Projectile>();
            projectile.SetOwner(this.gameObject);
            StartCoroutine("CoolDown", shootRate);
            anim.SetFloat("shootRate", 1 / shootRate);
            anim.SetTrigger("Attack");
        }
    }

    public void SpecialFire() // On Fire Special
    {
        // noop
    }


    IEnumerator CoolDown(float waitTime)
    {
        canShoot = false;
        yield return new WaitForSeconds(waitTime);
        canShoot = true;
    }

    void Start()
    {
        this.anim = this.GetComponent<Animator>();
    }

}
