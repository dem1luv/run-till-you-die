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
		switch (Random.Range(0, 3))
		{
			case 0:
				gravityDirection = Vector2.up;
				//transform.Rotate(new Vector3(0, 0, 180f));
				break;
			case 1:
				gravityDirection = Vector2.right;
				//transform.Rotate(new Vector3(0, 0, 90f));
				break;
			case 2:
				gravityDirection = Vector2.down;
				break;
			case 3:
				gravityDirection = Vector2.left;
				//transform.Rotate(new Vector3(0, 0, -90f));
				break;
		}
		blockObject.GetComponent<Block>().Generate();
	}

	public int GetBlockLength()
	{
		return blockObject.GetComponent<Block>().length;
	}
	
	public int GetBlockHeight()
	{
		return blockObject.GetComponent<Block>().height;
	}
}
