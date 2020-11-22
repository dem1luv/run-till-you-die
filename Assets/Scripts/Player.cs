using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private static Player current;
	public static Player Current
	{
		get { return current; }
	}

	[SerializeField] AudioSource audioHit;
	public AudioSource audioCockdillac;
	[SerializeField] float movingSpeed = 4f;
	[SerializeField] Animator cameraAnimator;
	[SerializeField] GameObject playerSpriteObj;
	
	SpriteRenderer playerSprite;

	public Vector2 playerDir = Vector2.right;

	Rigidbody2D rb;
	Collider2D collider;

	public bool IsGrounded { get; private set; } = true;
	public bool playMusic = false;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		collider = GetComponent<Collider2D>();
		playerSprite = playerSpriteObj.GetComponent<SpriteRenderer>();
		current = this;

		if (PlayerPrefs.HasKey("audioCockdillac.time"))
			audioCockdillac.time = PlayerPrefs.GetFloat("audioCockdillac.time");
	}

	void Update()
	{
		if (Input.GetMouseButton(0) && !GameManager.Current.IsLosed)
		{
			playMusic = true;
			if (IsGrounded)
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
				GameManager.Current.IncreaseMeters(passedMeters);
			}
			playerSprite.transform.Rotate(new Vector3(0, 0, -movingSpeed * Time.deltaTime * 100)); //idk how does it work but it does, so i won't touch it
		}
		else
			playMusic = false;
		if (playMusic && !audioCockdillac.isPlaying)
			audioCockdillac.Play();
		else if (!playMusic)
			audioCockdillac.Pause();
	}
	public void Die()
	{
		if (GameManager.Current.CurrentMeterCount != 0)
			audioHit.Play();
		Destroy(rb);
		Destroy(collider);
		playerSpriteObj.AddComponent<Rigidbody2D>();
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
		IsGrounded = false;
		rb.velocity = Physics2D.gravity / 9.81f * 3;
		yield return new WaitForSeconds(0.3481927f);
		rb.velocity = Vector2.zero;
		IsGrounded = true;
	}
}
