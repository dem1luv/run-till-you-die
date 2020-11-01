using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] float movingSpeed = 1f;
	[SerializeField] GameObject mainCamera;

	Vector2 playerDirection = Vector2.right;
	Animator cameraAnimator;

	private void Start()
	{
		cameraAnimator = mainCamera.GetComponent<Animator>();
	}

	void Update()
	{
		if (Input.GetMouseButton(0))
		{
			Vector2 direction = playerDirection * movingSpeed * Time.deltaTime;
			transform.Translate(direction);
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
