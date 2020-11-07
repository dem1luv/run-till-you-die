﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] float movingSpeed = 4f;
	[SerializeField] Animator cameraAnimator;
	[SerializeField] SpriteRenderer playerSprite;

	Vector2 playerDirection = Vector2.right;

	void Update()
	{
		if (Input.GetMouseButton(0))
		{
			Vector2 direction = playerDirection * movingSpeed * Time.deltaTime;
			transform.Translate(direction);
			playerSprite.transform.Rotate(new Vector3(0, 0, -movingSpeed * Time.deltaTime * 100)); //idk how does it work but it does, so i won't touch it
		}
	}

	public void UpdateDirection(Vector2 newDirection)
	{
		playerDirection = newDirection;
		if (newDirection.x > 0)
			cameraAnimator.SetInteger("state", 0);
		else if (newDirection.y < 0)
			cameraAnimator.SetInteger("state", 1);
		else if (newDirection.x < 0)
			cameraAnimator.SetInteger("state", 2);
		else
			cameraAnimator.SetInteger("state", 3);
	}
}
