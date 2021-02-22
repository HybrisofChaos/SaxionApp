using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DestructableObject : MonoBehaviour, IKillable
{

	[SerializeField]
	private int health = 100;

	[SerializeField]
	private GameObject destroyedObject; // Object to replace this one with on death

	[SerializeField]
	private GameObject destroyParticles; // Particles to be created on death

	public void ApplyDamage(int damage, GameObject source)
	{
		health -= damage;

		if (health < 0)
		{
			Die();
		}
	}

	public void Die()
	{
		if (destroyParticles)
		{
			Instantiate(destroyParticles, transform.position, Quaternion.identity);
		}

		if (destroyedObject)
		{
			Instantiate(destroyedObject, transform.position, Quaternion.identity);
		}

		Destroy(this.gameObject);
	}

	public void RegisterToUIEvents()
	{
		// NOOP
	}
}
