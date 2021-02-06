using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
	private Quaternion aimRotation;
	float mousePosX = 0;
	float mousePosY = 0;

    void OnAimMouse(InputValue value) // Mouse
	{
		Vector2 direction = value.Get<Vector2>() * Time.deltaTime;

		if (Vector2.zero == direction) return;
		mousePosX = Mathf.Clamp(mousePosX + direction.x, -1f, 1f); // Constraint voor de virtual analog stick
		mousePosY = Mathf.Clamp(mousePosY + direction.y, -1f, 1f);
		Vector2 newDirection = new Vector2(mousePosX, mousePosY);
		newDirection.Normalize();
		direction = newDirection;

		Vector3 playerPos = transform.position;
		Vector3 aimPos = playerPos + 5 * (Vector3) direction;

		Vector3 difference = aimPos - playerPos;
		float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

		this.aimRotation = Quaternion.Euler(0, 0, rotZ);
	}

	void OnAim(InputValue value) // Controller werkt beter zonder de clamp
	{
		Vector2 direction = value.Get<Vector2>() * Time.deltaTime;

		if (Vector2.zero == direction) return;

		Vector3 playerPos = transform.position;
		Vector3 aimPos = playerPos + 5 * (Vector3)direction;

		Vector3 difference = aimPos - playerPos;
		float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

		this.aimRotation = Quaternion.Euler(0, 0, rotZ);
	}

	public Quaternion GetRotation()
	{
		return this.aimRotation;
	}
}
