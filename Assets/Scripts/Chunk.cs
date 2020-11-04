using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
	public Vector2 gravityDirection;
	[Space]
	public GameObject blockObject;


	public void Generate()
	{
		blockObject.GetComponent<Block>().Generate();
	}

	public float GetBlockLength()
	{
		return blockObject.GetComponent<Block>().convertedLength;
	}
	
	public float GetBlockHeight()
	{
		return blockObject.GetComponent<Block>().convertedHeight;
	}
}
