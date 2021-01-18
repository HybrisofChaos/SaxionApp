using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IKillable
{
	[SerializeField]
	private int maxHealth = 1000;
	private int health;

	public delegate void DeathAction(GameObject deadPlayer, GameObject killer);
	public static event DeathAction OnDeath;

	private List<GameObject> damageSources = new List<GameObject>();

	void Start(){
		this.health = this.maxHealth;
	}

	public void ApplyDamage(int damage, GameObject source)
	{
		health -= damage;

		if(health <= 0){
			Die();
		}
	}

	public void Die(){
		OnDeath(this.gameObject, damageSources[damageSources.Count - 1]);
	}

	public void RegisterToUIEvents(){
		//NOOP for now
	}

	protected void OnAttack(){
	}

	protected void OnSkill(){
	}
}
