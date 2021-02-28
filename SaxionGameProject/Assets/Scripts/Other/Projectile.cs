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

    private TrailRenderer trail;
    private SpriteRenderer sprite;

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

    void Awake()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        rb2d.AddForce(transform.right * speed, ForceMode2D.Impulse);

        this.sprite = this.gameObject.GetComponent<SpriteRenderer>();
        this.trail = this.gameObject.GetComponentInChildren<TrailRenderer>();
        if (trail)
        {
            trail.sortingOrder = 999;
        }
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
        Color c = owner.GetComponentInParent<Player>().color;
        SetColor(c);
    }

    private void SetColor(Color color)
    {
        this.sprite.color = color;

        if (trail)
        {
            float alpha = trail.startColor.a;
            color.a = alpha;
            trail.startColor = color;
        }
    }

    public GameObject GetOwner()
    {
        return owner;
    }
}
