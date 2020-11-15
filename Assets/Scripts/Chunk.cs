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
		
		// set default spike positions (without xpos for now)
		Vector2 spikePos = transform.position;
		spikePos.y += GetBlockHeight() / 2f + 1.34f / 2f;
		
		float minObjectX = transform.position.x - GetBlockLength() / 2f + GetBlockHeight() * 3f;
		float maxObjectX = transform.position.x + GetBlockLength() / 2f - GetBlockHeight() * 2.4f;

		float maxObjectGap = 10f;

		float spikeWidth = 1.65f;
		float thornWidth = 1.65f * 0.25f;
		
		// marker variables
		int groupedSpikesCount = 0;
		float startDelay = 0;
		float upDelay = 0;
		float downDelay = 0;
		float lastSpikePosX = 0; // for calcuting thorns position

		// generation cycle
		while (true)
		{
			minObjectX += spikeWidth;

			// chance 50% that a current spike will be grouped
			if (Random.Range(0, 2) == 0 && groupedSpikesCount < 5)
			{
				// left intend if it's the first grouped spike
				if (groupedSpikesCount == 0)
					minObjectX += spikeWidth * 2;

				// increase the grouped spikes count marker
				groupedSpikesCount++;
			}
			// else, the current spike is a single
			else
			{
				// if it was some groupedSpikes, it can spawn a thorn platform with 12.5% (because there should be at least 2 grouped spikes)
				if (Random.Range(0, 2) == 0 & groupedSpikesCount > 1)
				{
					// create variable for thorn platform position
					float thornPlatformPosX = lastSpikePosX;
					thornPlatformPosX -= (groupedSpikesCount - 1) / 2f * spikeWidth;
					float thornPlatformPosY = Random.Range(4f, 6f);
					Vector2 thornPlatformPos = new Vector2(thornPlatformPosX, thornPlatformPosY);

					GameObject thornPlatformObj = Instantiate(thornPlatformPref, Vector2.zero, Quaternion.identity, transform);
					// set thorn platform local scale
					thornPlatformObj.transform.localPosition = thornPlatformPos;

					// create and set thorn platform local scale
					Vector2 thornPlatformLS = new Vector2();
					thornPlatformLS.x = Utils.ConvertPositionToBlockScale(spikeWidth * groupedSpikesCount + spikeWidth * 2);
					thornPlatformLS.y = thornPlatformObj.transform.localScale.y;
					thornPlatformObj.transform.localScale = thornPlatformLS;

					// set xpos for the first thorn
					float thornPosX = thornPlatformPos.x;
					thornPosX -= Utils.ConvertBlockScaleToPosition(thornPlatformLS.x) / 2f;
					thornPosX += thornWidth / 2f;
					// set ypos for all thorns
					float thornPosY = thornPlatformPos.y - 0.543f;

					for(int i = 0; i < groupedSpikesCount * 4 + 8; i++) // 1 spike = 4 thorn and 8 additionals (by 4 for left and right sides)
					{
						GameObject thornObj = Instantiate(thornPref, Vector2.zero, Quaternion.identity, transform);
						thornObj.transform.localPosition = new Vector2(thornPosX, thornPosY);

						// increase thorn xpos
						thornPosX += thornWidth;
					}
				}
				groupedSpikesCount = 0;
				minObjectX = Random.Range(minObjectX + spikeWidth * 2, minObjectX + maxObjectGap); // "+ spikeWidth * 2" for right ident
			}

			// if future spikes go beyond, cycle will be terminated
			if (minObjectX >= maxObjectX)
				break;

			// set xpos for spike position
			spikePos.x = minObjectX;

			GameObject spikeObj = Instantiate(spikePref, spikePos, Quaternion.identity, transform);
			
			lastSpikePosX = spikeObj.transform.localPosition.x;
			
			// generate spike
			Spike spike = spikeObj.GetComponent<Spike>();
			// spikes will be synchronous if they are grouped
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
			// else, set random values
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
