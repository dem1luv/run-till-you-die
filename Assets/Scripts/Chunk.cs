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
	[SerializeField] GameObject smallSpikePlatformPrefab;
	[SerializeField] GameObject smallSpikePrefab;


	public void Generate()
	{
		blockObject.GetComponent<Block>().Generate();
		Vector2 spikePos = transform.position;
		spikePos.y += GetBlockHeight() / 2f + 1.42f / 2f;
		float minObjectX = transform.position.x - GetBlockLength() / 2f + GetBlockHeight() * 3f;
		float maxObjectX = transform.position.x + GetBlockLength() / 2f - GetBlockHeight() * 2.4f;
		float step = 1.65f;
		float smallStep = 1.65f * 0.25f;
		int spikesTogether = 0;
		float startDelay = 0;
		float upDelay = 0;
		float downDelay = 0;
		int randInt;
		float lastSpikeX = 0;
		while (true)
		{
			minObjectX += step;
			randInt = Random.Range(0, 2);
			if (randInt == 0 && spikesTogether < 5) // 50%
			{
				if (spikesTogether == 0)
					minObjectX += step;
				spikesTogether++;
			}
			else
			{
				randInt = Random.Range(0, 2);
				if (randInt == 0 & spikesTogether > 1) // 50% (25%)
				{
					float smallSpikePlatformX = lastSpikeX;
					smallSpikePlatformX -= (spikesTogether - 1) / 2f * step;
					float smallSpikePlatformY = Random.Range(4f, 6f);
					Vector2 smallSpikePlatformPos = new Vector2(smallSpikePlatformX, smallSpikePlatformY);
					GameObject smallSpikePlatform = Instantiate(smallSpikePlatformPrefab, Vector2.zero, Quaternion.identity, transform);
					smallSpikePlatform.transform.localPosition = smallSpikePlatformPos;
					Vector2 smallSpikePlatformLocalScale = new Vector2();
					smallSpikePlatformLocalScale.x = Utils.ConvertPositionToBlockScale(step * spikesTogether + step * 2);
					smallSpikePlatformLocalScale.y = smallSpikePlatform.transform.localScale.y;
					smallSpikePlatform.transform.localScale = smallSpikePlatformLocalScale;
					float x = smallSpikePlatformPos.x;
					x -= Utils.ConvertBlockScaleToPosition(smallSpikePlatformLocalScale.x) / 2f;
					x += smallStep / 2f;
					float y = smallSpikePlatformPos.y - 0.554f;
					for(int i = 0; i < spikesTogether * 4 + 8; i++)
					{
						GameObject smallSpikeObj = Instantiate(smallSpikePrefab, Vector2.zero, Quaternion.identity, transform);
						smallSpikeObj.transform.localPosition = new Vector2(x, y);
						x += smallStep;
					}
				}
				spikesTogether = 0;
				minObjectX = Random.Range(minObjectX + step, maxObjectX);
			}
			if (minObjectX >= maxObjectX)
				break;
			spikePos.x = minObjectX;
			GameObject spikeObj = Instantiate(spikePrefab, spikePos, Quaternion.identity, transform);
			lastSpikeX = spikeObj.transform.localPosition.x;
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
