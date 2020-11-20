using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : MonoBehaviour
{
	Rigidbody2D rb;
	Chunk parentChunk;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	void Start()
	{
		parentChunk = transform.parent.parent.parent.GetComponent<Chunk>();

		if (parentChunk.gravityDirection.x == 0)
		{
			rb.constraints = RigidbodyConstraints2D.FreezePositionX;
			rb.freezeRotation = true;
		}
		else
		{
			rb.constraints = RigidbodyConstraints2D.FreezePositionY;
			rb.freezeRotation = true;
		}
	}
	public void Fall()
	{
		rb.bodyType = RigidbodyType2D.Dynamic;
	}
}
