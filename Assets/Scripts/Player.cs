using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] float movingSpeed = 1f;

	Vector2 playerDirection = Vector2.right;

	void Update()
	{
		if (Input.GetMouseButton(0))
		{
			Vector2 direction = playerDirection * movingSpeed * Time.deltaTime;
			transform.Translate(direction);
		}
	}

	public void ChangeDirection(Vector2 direction)
	{
		playerDirection = direction;
	}
}
