using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
	void Start()
	{
		StartCoroutine("Move");
	}

	IEnumerator Move()
	{
		yield return new WaitForSeconds(1f);
		while (true)
		{
			transform.Translate(new Vector2(0, -1.44f));

			yield return new WaitForSeconds(4f);

			transform.Translate(new Vector2(0, 1.44f));

			yield return new WaitForSeconds(4f);
		}
	}
}
