using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTrigger : MonoBehaviour
{
	public Vector2 gravityDirection;
	public Vector2 playerDirection;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			Physics2D.gravity = gravityDirection * 9.81f;
			collision.GetComponent<Player>().UpdateDirection(playerDirection);			
		}
	}
}
