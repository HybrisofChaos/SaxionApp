using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
	private Quaternion aimRotation;

    void OnAim(InputValue value)
	{
		Vector2 direction = value.Get<Vector2>() * Time.deltaTime;

		if (Vector2.zero == direction) return;

		Vector3 playerPos = transform.position;
		Vector3 aimPos = playerPos + 5 * (Vector3) direction;

		Vector3 difference = aimPos - playerPos;
		float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

		this.aimRotation = Quaternion.Euler(0, 0, rotZ);
	}

	public Quaternion GetRotation()
	{
		return this.aimRotation;
	}
}
