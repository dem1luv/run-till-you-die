using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
	public Vector2 gravityDirection;
	[Space]
	public GameObject blockObject;
	[Space]
	[SerializeField] GameObject spikePref;
	[SerializeField] GameObject thornPlatformPref;
	[SerializeField] GameObject thornPref;


	public void Generate()
	{
		blockObject.GetComponent<Block>().Generate();
		
		Vector2 spikePos = transform.position;
		spikePos.y += GetBlockHeight() / 2f + 1.42f / 2f;
		
		float minObjectX = transform.position.x - GetBlockLength() / 2f + GetBlockHeight() * 3f;
		float maxObjectX = transform.position.x + GetBlockLength() / 2f - GetBlockHeight() * 2.4f;

		float spikeWidth = 1.65f;
		float thornWidth = 1.65f * 0.25f;
		
		// marker variables
		int groupedSpikesCount = 0;
		float startDelay = 0;
		float upDelay = 0;
		float downDelay = 0;
		float lastSpikeX = 0;

		// generation cycle
		while (true)
		{
			minObjectX += spikeWidth;

			// chance 50% that a current spike will be grouped
			if (Random.Range(0, 2) == 0 && groupedSpikesCount < 5)
			{
				// front intend if it's the first grouped spike
				if (groupedSpikesCount == 0)
					minObjectX += spikeWidth;

				// increase the grouped spikes count marker
				groupedSpikesCount++;
			}
			// else, the current spike is a single
			else
			{
				// if it was some groupedSpikes, it can spawn a thorn platform with 12.5% (because there should be at least 2 grouped spikes)
				if (Random.Range(0, 2) == 0 & groupedSpikesCount > 1)
				{
					float thornPlatformPosX = lastSpikeX;
					thornPlatformPosX -= (groupedSpikesCount - 1) / 2f * spikeWidth;
					float thornPlatformPosY = Random.Range(4f, 6f);
					Vector2 thornPlatformPos = new Vector2(thornPlatformPosX, thornPlatformPosY);

					GameObject thornPlatformObj = Instantiate(thornPlatformPref, Vector2.zero, Quaternion.identity, transform);
					thornPlatformObj.transform.localPosition = thornPlatformPos;

					Vector2 thornPlatformLS = new Vector2();
					thornPlatformLS.x = Utils.ConvertPositionToBlockScale(spikeWidth * groupedSpikesCount + spikeWidth * 2);
					thornPlatformLS.y = thornPlatformObj.transform.localScale.y;
					
					thornPlatformObj.transform.localScale = thornPlatformLS;

					float x = thornPlatformPos.x;
					x -= Utils.ConvertBlockScaleToPosition(thornPlatformLS.x) / 2f;
					x += thornWidth / 2f;
					float y = thornPlatformPos.y - 0.554f;

					for(int i = 0; i < groupedSpikesCount * 4 + 8; i++)
					{
						GameObject thornObj = Instantiate(thornPref, Vector2.zero, Quaternion.identity, transform);
						thornObj.transform.localPosition = new Vector2(x, y);

						x += thornWidth;
					}
				}
				groupedSpikesCount = 0;

				minObjectX = Random.Range(minObjectX + spikeWidth, maxObjectX);
			}

			if (minObjectX >= maxObjectX)
				break;

			spikePos.x = minObjectX;

			GameObject spikeObj = Instantiate(spikePref, spikePos, Quaternion.identity, transform);
			
			lastSpikeX = spikeObj.transform.localPosition.x;
			
			Spike spike = spikeObj.GetComponent<Spike>();
			if (groupedSpikesCount > 0)
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
