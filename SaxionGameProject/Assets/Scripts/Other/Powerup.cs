using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

	[SerializeField]
	private float duration;
	[SerializeField]
	private float respawnTime = 300; // 5 minutes

	private GameObject[] particleSystems;

	void Start()
	{
		particleSystems = new GameObject[this.transform.childCount];
		for (int i = 0; i < particleSystems.Length; i++)
		{
			particleSystems[i] = this.transform.GetChild(i).gameObject;
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		PlayerController controller = collider.gameObject.GetComponent<PlayerController>();
		if (!controller) return;

		controller.ApplyPowerup(duration);

		StartCoroutine("StartRespawn", respawnTime);
	}

	private IEnumerator StartRespawn(float time)
	{
		TogglePowerup();
		yield return new WaitForSeconds(time);
		TogglePowerup();
	}

	private void ToggleParticleSystems()
	{
		foreach (GameObject o in this.particleSystems)
		{
			o.SetActive(!o.activeSelf);
		}
	}

	private void ToggleCollider()
	{
		Collider2D collider = this.GetComponent<Collider2D>();
		collider.enabled = !collider.enabled;
	}

	private void TogglePowerup()
	{
		ToggleParticleSystems();
		ToggleCollider();
	}
}
