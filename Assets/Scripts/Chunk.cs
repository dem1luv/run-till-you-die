using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
	public Vector2 gravityDirection;
	[Space]
	public GameObject blockObject;
	[Space]
	[SerializeField] GameObject spikePrefab;


	public void Generate()
	{
		blockObject.GetComponent<Block>().Generate();
		Vector2 spikePos = transform.position;
		spikePos.y += GetBlockHeight() / 2f + 1.42f / 2f;
		spikePos.x = Random.Range(transform.position.x - GetBlockLength() / 2f + GetBlockHeight() * 2.4f, transform.position.x + GetBlockLength() / 2f - GetBlockHeight() * 2.4f);
		Instantiate(spikePrefab, spikePos, Quaternion.identity, transform);
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
