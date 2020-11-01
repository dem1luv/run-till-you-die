using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSpike : MonoBehaviour
{
	public void Fall()
	{
		GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
	}
}
