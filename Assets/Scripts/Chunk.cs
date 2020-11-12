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
		float minObjectX = transform.position.x - GetBlockLength() / 2f + GetBlockHeight() * 3f;
		float maxObjectX = transform.position.x + GetBlockLength() / 2f - GetBlockHeight() * 2.4f;
		float step = 1.65f;
		int spikesTogether = 0;
		float startDelay = 0;
		float upDelay = 0;
		float downDelay = 0;
		while (maxObjectX >= minObjectX)
		{
			int randInt = Random.Range(0, 2);
			if (randInt == 0 && spikesTogether < 5) // 50%
			{
				spikesTogether++;
				minObjectX += step;
			}
			else
			{
				spikesTogether = 0;
				minObjectX = Random.Range(minObjectX + step, maxObjectX);
			}
			minObjectX += step;
			spikePos.x = minObjectX;
			GameObject spikeObj = Instantiate(spikePrefab, spikePos, Quaternion.identity, transform);
			Spike spike = spikeObj.GetComponent<Spike>();
			if (spikesTogether > 0)
			{
				if (startDelay == 0)
				{
					startDelay = Random.Range(1f, 3f);
					upDelay = Random.Range(2f, 5f);
					downDelay = Random.Range(1f, 2f);
				}
				else
					startDelay += 0.19f;
				spike.Generate(startDelay, upDelay, downDelay);
			}
			else
			{
				startDelay = 0;
				spike.GenerateRandom();
			}
		}
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
