using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
	[SerializeField] float startDelay = 1f;
	[SerializeField] float upDelay = 4f;
	[SerializeField] float downDelay = 4f;
	
	void Start()
	{
		StartCoroutine("Move");
	}

	IEnumerator Move()
	{
		yield return new WaitForSeconds(startDelay);
		while (true)
		{
			transform.Translate(new Vector2(0, -1.44f));

			yield return new WaitForSeconds(upDelay);

			transform.Translate(new Vector2(0, 1.44f));

			yield return new WaitForSeconds(downDelay);
		}
	}
}
