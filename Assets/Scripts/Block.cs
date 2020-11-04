using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	[SerializeField] int height;
	[Space]
	[SerializeField] int minLength;
	[SerializeField] int maxLength;
	
	int length;
	[HideInInspector] public float convertedLength;
	[HideInInspector] public float convertedHeight;

	public void Generate()
	{
		length = Random.Range(minLength, maxLength);
		convertedLength = Utils.ConvertBlockScaleToPosition(length);
		convertedHeight = Utils.ConvertBlockScaleToPosition(height);

		transform.localScale = new Vector2(length, height);
	}
}
