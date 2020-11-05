using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneratorManager : MonoBehaviour
{
    [SerializeField] GameObject chunkPrefab;
	[SerializeField] GameObject gravityTriggerPrefab;

	Vector2 instancePosition = Vector2.zero;
	Vector2 lastGravityDirection = Vector2.right;
	float lastBlockLength = 0;
	float blockHeight;
	int randomInt = -1;

	private void Start()
	{
		StartCoroutine("GenerateChunks");
	}

	IEnumerator GenerateChunks()
	{
		for (int i = 0; i < 10; i++)
		{
			GameObject chunkObject = Instantiate(chunkPrefab, instancePosition, Quaternion.identity);
			Chunk chunk = chunkObject.GetComponent<Chunk>();

			chunk.Generate();

			blockHeight = chunk.GetBlockHeight();

			randomInt = randomInt == -1 ? 1 : Random.Range(0, 2);

			Vector2 gravityTriggerPos = Vector2.zero;
			GameObject gravityTrigger = Instantiate(gravityTriggerPrefab, chunk.transform);
			GravityTrigger gt = gravityTrigger.GetComponent<GravityTrigger>();

			gravityTriggerPos.x -= chunk.GetBlockLength() / 2f;

			if (randomInt == 0)
			{
				gravityTriggerPos.x += Utils.ConvertBlockScaleToPosition(gravityTriggerPrefab.transform.localScale.x) / 2f;
				gravityTriggerPos.x += blockHeight;
				gravityTriggerPos.y += blockHeight * 1.5f;
			}
			else
			{
				gravityTriggerPos.x += blockHeight / 1.5f;
				gravityTriggerPos.y += Utils.ConvertBlockScaleToPosition(gravityTriggerPrefab.transform.localScale.x) / 2f;
				gravityTriggerPos.y += blockHeight / 2f;
				gravityTrigger.transform.Rotate(new Vector3(0, 0, 90f));
			}

			gravityTrigger.transform.localPosition = gravityTriggerPos;

			if (lastGravityDirection == Vector2.down)
			{
				if (randomInt == 0)
				{
					lastGravityDirection = Vector2.right;

					instancePosition.x += lastBlockLength / 2f;
					instancePosition.x += blockHeight / 2f;
					instancePosition.y += (chunk.GetBlockLength() - blockHeight) / 2f;

					gt.gravityDirection = Vector2.right;
					gt.playerDirection = Vector2.up;

					chunk.transform.Rotate(new Vector3(0, 0, 90f));
				}
				else
				{
					lastGravityDirection = Vector2.left;

					instancePosition.x += lastBlockLength / 2f;
					instancePosition.x += blockHeight / 2f;
					instancePosition.y -= (chunk.GetBlockLength() - blockHeight) / 2f;

					gt.gravityDirection = Vector2.left;
					gt.playerDirection = Vector2.down;

					chunk.transform.Rotate(new Vector3(0, 0, -90f));
				}
			}
			else if (lastGravityDirection == Vector2.right)
			{
				if (randomInt == 0)
				{
					lastGravityDirection = Vector2.up;

					instancePosition.x += blockHeight / 2f;
					instancePosition.x -= chunk.GetBlockLength() / 2f;
					instancePosition.y += blockHeight / 2f;
					instancePosition.y += lastBlockLength / 2f;

					gt.gravityDirection = Vector2.up;
					gt.playerDirection = Vector2.left;

					chunk.transform.Rotate(new Vector3(0, 0, 180f));
				}
				else
				{
					lastGravityDirection = Vector2.down;

					instancePosition.x -= blockHeight / 2f;
					instancePosition.x += chunk.GetBlockLength() / 2f;
					instancePosition.y += blockHeight / 2f;
					instancePosition.y += lastBlockLength / 2f;

					gt.gravityDirection = Vector2.down;
					gt.playerDirection = Vector2.right;
				}
			}
			else if (lastGravityDirection == Vector2.left)
			{
				if (randomInt == 0)
				{
					lastGravityDirection = Vector2.down;

					instancePosition.x -= blockHeight / 2f;
					instancePosition.x += chunk.GetBlockLength() / 2f;
					instancePosition.y -= lastBlockLength / 2f;
					instancePosition.y -= blockHeight / 2f;

					gt.gravityDirection = Vector2.down;
					gt.playerDirection = Vector2.right;
				}
				else
				{
					lastGravityDirection = Vector2.up;

					instancePosition.x += blockHeight / 2f;
					instancePosition.x -= chunk.GetBlockLength() / 2f;
					instancePosition.y -= lastBlockLength / 2f;
					instancePosition.y -= blockHeight / 2f;

					gt.gravityDirection = Vector2.up;
					gt.playerDirection = Vector2.left;

					chunk.transform.Rotate(new Vector3(0, 0, 180f));
				}
			}
			else
			{
				if (randomInt == 0)
				{
					lastGravityDirection = Vector2.left;

					instancePosition.x -= lastBlockLength / 2f;
					instancePosition.x -= blockHeight / 2f;
					instancePosition.y -= (chunk.GetBlockLength() - blockHeight) / 2f;

					gt.gravityDirection = Vector2.left;
					gt.playerDirection = Vector2.down;

					chunk.transform.Rotate(new Vector3(0, 0, -90f));
				}
				else
				{
					lastGravityDirection = Vector2.right;

					instancePosition.x -= lastBlockLength / 2f;
					instancePosition.x -= blockHeight / 2f;
					instancePosition.y += (chunk.GetBlockLength() - blockHeight) / 2f;

					gt.gravityDirection = Vector2.right;
					gt.playerDirection = Vector2.up;

					chunk.transform.Rotate(new Vector3(0, 0, 90f));
				}
			}

			lastBlockLength = chunk.GetBlockLength();
			chunk.gravityDirection = lastGravityDirection;
			
			chunkObject.transform.position = instancePosition;

			yield return new WaitForEndOfFrame();
		}
	}
}
