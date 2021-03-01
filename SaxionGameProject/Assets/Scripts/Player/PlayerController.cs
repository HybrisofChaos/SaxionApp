using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IKillable
{
    [SerializeField]
    private int maxHealth = 1000;

    [SerializeField]
    private GameObject aimArrow;

    private int health;

	[SerializeField]
	protected GameObject powerupEffect;
    public delegate void DeathAction(GameObject deadPlayer, GameObject killer);
    public static event DeathAction OnDeath;

    private List<GameObject> damageSources = new List<GameObject>();

    private BaseWeapon weapon;
    private PlayerAim playerAim;


    void Start()
    {
        this.health = this.maxHealth;

        this.weapon = this.GetComponent<BaseWeapon>();
        playerAim = this.GetComponent<PlayerAim>();
    }

	protected void OnSkill(){
	}

	//To be overridden by child
	public void ApplyPowerup(float duration)
	{
		if (powerupEffect)
		{
			GameObject effect = Instantiate(powerupEffect, this.transform);
			TimedDestroy destroyTimer = effect.GetComponent<TimedDestroy>();
			if (destroyTimer)
			{
				destroyTimer.SetTime(duration);
				destroyTimer.StartCountdown();
			}
		}
	}
    void Update()
    {
        //TODO: See if lerping this helps with mouse input
        aimArrow.transform.rotation = playerAim.GetRotation();
        aimArrow.transform.localScale = transform.localScale;
    }

    public void ApplyDamage(int damage, GameObject source)
    {
        health -= damage;
        damageSources.Add(source);

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        OnDeath(this.gameObject, damageSources[damageSources.Count - 1]);
    }

    public void RegisterToUIEvents()
    {
        //NOOP for now
    }

    protected void OnFire()
    {
        weapon.Fire(aimArrow.transform.rotation);
    }

    protected void OnFireSpecial()
    {
        weapon.SpecialFire();
    }
}
