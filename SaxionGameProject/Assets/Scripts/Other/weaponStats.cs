using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponStats : MonoBehaviour
{
    public enum Modes
    { melee, Straight, Follow, Throw }

    public float projectileSpeed;
    public float coolDown;
    public bool pierce;
    public float speed;
    public float lifeTime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;


    public Modes projectileMode;

 /*   void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.attachedRigidbody);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
        Debug.Log(collision.gameObject.name);
    } */

    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            /*         if (hitInfo.collider.CompareTag("Enemy"))
                     {
                         hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
                     } */
            Debug.Log("HIT");
            if (!pierce)
            {
              DestroyProjectile();
            }
        }


        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

}
