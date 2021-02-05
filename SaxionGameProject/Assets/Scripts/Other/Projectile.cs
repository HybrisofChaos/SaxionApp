using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public enum Mode
    { MELEE, STRAIGHT, FOLLOW, THROW }

    [SerializeField]
    private int damage = 20;

    [SerializeField]
    private float speed = 15f;

    [SerializeField]
    private GameObject onDestroyParticles;

    private GameObject owner;
    private Rigidbody2D rb2d;

    public Mode projectileMode;

    void OnTriggerEnter2D(Collider2D collider)
	{
        if (collider.gameObject == owner.gameObject) return;

        GameObject go = collider.gameObject;
        IKillable killable = go.GetComponent<IKillable>();

		if (null != killable)
		{
            killable.ApplyDamage(damage, owner);
		}


        Destroy(this.gameObject);
	}

    void Start()
	{
        rb2d = this.GetComponent<Rigidbody2D>();
        rb2d.AddForce(transform.right * speed, ForceMode2D.Impulse);
	}

    void OnDestroy()
	{
		if (onDestroyParticles)
		{
            Instantiate(onDestroyParticles, transform.position, Quaternion.identity);
		}
	}

    public void SetOwner(GameObject owner)
	{
        this.owner = owner;
	}

    public GameObject GetOwner()
	{
        return owner;
	}
}
