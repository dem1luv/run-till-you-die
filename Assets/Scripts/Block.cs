using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	public int height;
	[Space]
	public int minLength;
	public int maxLength;
	
	[HideInInspector] public int length;

	public void Generate()
	{
		length = Random.Range(minLength, maxLength);

		transform.localScale = new Vector2(length, height);
	}
}
