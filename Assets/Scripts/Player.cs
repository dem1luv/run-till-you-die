using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public static Player current;

	[SerializeField] float movingSpeed = 4f;
	[SerializeField] Animator cameraAnimator;
	[SerializeField] SpriteRenderer playerSprite;

	public Vector2 playerDir = Vector2.right;

	Rigidbody2D rb;
	public bool isGrounded = true;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		current = this;
	}

	void Update()
	{
		if (Input.GetMouseButton(0))
		{
			if (isGrounded)
			{
				Vector2 direction = playerDir * movingSpeed * Time.deltaTime;
				transform.Translate(direction);

				float passedMeters;
				if (direction.x == 0)
					passedMeters = direction.y;
				else
					passedMeters = direction.x;
				passedMeters = Mathf.Abs(passedMeters);
				passedMeters /= 4f;
				GameManager.IncreaseMeters(passedMeters);
			}
			playerSprite.transform.Rotate(new Vector3(0, 0, -movingSpeed * Time.deltaTime * 100)); //idk how does it work but it does, so i won't touch it
		}
	}

	public void UpdateDirection(Vector2 newDir)
	{
		playerDir = newDir;
		if (newDir.x > 0)
			cameraAnimator.SetInteger("state", 0);
		else if (newDir.y < 0)
			cameraAnimator.SetInteger("state", 1);
		else if (newDir.x < 0)
			cameraAnimator.SetInteger("state", 2);
		else
			cameraAnimator.SetInteger("state", 3);
		StartCoroutine("TempBanPlayerMoving");
	}

	IEnumerator TempBanPlayerMoving()
	{
		isGrounded = false;
		rb.velocity = Physics2D.gravity / 9.81f * 3;
		yield return new WaitForSeconds(0.3481927f);
		rb.velocity = Vector2.zero;
		isGrounded = true;
	}
}
