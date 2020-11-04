using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneratorManager : MonoBehaviour
{
    [SerializeField] GameObject chunkPrefab;

	Vector2 instancePosition = Vector2.zero;
	Vector2 lastGravityDirection = Vector2.down;
	float lastBlockLength = 0;
	float blockHeight;

	private void Start()
	{
		StartCoroutine("GenerateChunks");
	}

	IEnumerator GenerateChunks()
	{
		while (true)
		{
			GameObject chunkObject = Instantiate(chunkPrefab, instancePosition, Quaternion.identity);
			Chunk chunk = chunkObject.GetComponent<Chunk>();

			chunk.Generate();

			blockHeight = chunk.GetBlockHeight();

			int randomInt = Random.Range(0, 3);

			if (lastGravityDirection == Vector2.down)
			{
				if (randomInt == 0)
				{
					lastGravityDirection = Vector2.right;

					instancePosition.x += lastBlockLength / 2f;
					instancePosition.x += blockHeight / 2f;
					instancePosition.y += (chunk.GetBlockLength() - blockHeight) / 2f;

					chunk.transform.Rotate(new Vector3(0, 0, 90f));
				}
				else if (randomInt == 1)
				{
					lastGravityDirection = Vector2.left;

					instancePosition.x += lastBlockLength / 2f;
					instancePosition.x += blockHeight / 2f;
					instancePosition.y -= (chunk.GetBlockLength() - blockHeight) / 2f;

					chunk.transform.Rotate(new Vector3(0, 0, -90f));
				}
				else if (randomInt == 2)
				{
					lastGravityDirection = Vector2.down;

					instancePosition.x += lastBlockLength / 2f;
					instancePosition.x += chunk.GetBlockLength() / 2f;
				}
			}
			else if (lastGravityDirection == Vector2.right)
			{
				if (randomInt == 0)
				{
					lastGravityDirection = Vector2.right;

					instancePosition.y += lastBlockLength / 2f;
					instancePosition.y += chunk.GetBlockLength() / 2f;

					chunk.transform.Rotate(new Vector3(0, 0, 90f));
				}
				else if (randomInt == 1)
				{
					lastGravityDirection = Vector2.up;

					instancePosition.x += blockHeight / 2f;
					instancePosition.x -= chunk.GetBlockLength() / 2f;
					instancePosition.y += blockHeight / 2f;
					instancePosition.y += lastBlockLength / 2f;

					chunk.transform.Rotate(new Vector3(0, 0, 180f));
				}
				else if (randomInt == 2)
				{
					lastGravityDirection = Vector2.down;

					instancePosition.x -= blockHeight / 2f;
					instancePosition.x += chunk.GetBlockLength() / 2f;
					instancePosition.y += blockHeight / 2f;
					instancePosition.y += lastBlockLength / 2f;
				}
			}
			else if (lastGravityDirection == Vector2.left)
			{
				if (randomInt == 0)
				{
					lastGravityDirection = Vector2.left;

					instancePosition.y -= lastBlockLength / 2f;
					instancePosition.y -= chunk.GetBlockLength() / 2f;

					chunk.transform.Rotate(new Vector3(0, 0, -90f));
				}
				else if (randomInt == 1)
				{
					lastGravityDirection = Vector2.up;

					instancePosition.x += blockHeight / 2f;
					instancePosition.x -= chunk.GetBlockLength() / 2f;
					instancePosition.y -= lastBlockLength / 2f;
					instancePosition.y -= blockHeight / 2f;

					chunk.transform.Rotate(new Vector3(0, 0, 180f));
				}
				else if (randomInt == 2)
				{
					lastGravityDirection = Vector2.down;

					instancePosition.x -= blockHeight / 2f;
					instancePosition.x += chunk.GetBlockLength() / 2f;
					instancePosition.y -= lastBlockLength / 2f;
					instancePosition.y -= blockHeight / 2f;
				}
			}
			else if (lastGravityDirection == Vector2.up)
			{
				if (randomInt == 0)
				{
					lastGravityDirection = Vector2.left;

					instancePosition.x -= lastBlockLength / 2f;
					instancePosition.x -= blockHeight / 2f;
					instancePosition.y -= (chunk.GetBlockLength() - blockHeight) / 2f;

					chunk.transform.Rotate(new Vector3(0, 0, -90f));
				}
				else if (randomInt == 1)
				{
					lastGravityDirection = Vector2.up;

					instancePosition.x -= lastBlockLength / 2f;
					instancePosition.x -= chunk.GetBlockLength() / 2f;

					chunk.transform.Rotate(new Vector3(0, 0, 180f));
				}
				else if (randomInt == 2)
				{
					lastGravityDirection = Vector2.right;

					instancePosition.x -= lastBlockLength / 2f;
					instancePosition.x -= blockHeight / 2f;
					instancePosition.y += (chunk.GetBlockLength() - blockHeight) / 2f;

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
