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

			if (lastGravityDirection == Vector2.down)
			{
				if (randomInt == 0)
				{
					lastGravityDirection = Vector2.right;

					instancePosition.x += lastBlockLength / 2f;
					instancePosition.x += blockHeight / 2f;
					instancePosition.y += (chunk.GetBlockLength() - blockHeight) / 2f;

					Vector2 gravityTriggerPos = Vector2.zero;
					gravityTriggerPos.x -= chunk.GetBlockLength() / 2f;
					gravityTriggerPos.x += Utils.ConvertBlockScaleToPosition(gravityTriggerPrefab.transform.localScale.x) / 2f;
					gravityTriggerPos.x += blockHeight;
					gravityTriggerPos.y += blockHeight * 1.5f;
					GameObject gravityTrigger = Instantiate(gravityTriggerPrefab, chunk.transform);
					gravityTrigger.transform.localPosition = gravityTriggerPos;
					gravityTrigger.GetComponent<GravityTrigger>().gravityDirection = Vector2.right;
					gravityTrigger.GetComponent<GravityTrigger>().playerDirection = Vector2.up;

					chunk.transform.Rotate(new Vector3(0, 0, 90f));
				}
				else
				{
					lastGravityDirection = Vector2.left;

					instancePosition.x += lastBlockLength / 2f;
					instancePosition.x += blockHeight / 2f;
					instancePosition.y -= (chunk.GetBlockLength() - blockHeight) / 2f;

					Vector2 gravityTriggerPos = Vector2.zero;
					gravityTriggerPos.x -= chunk.GetBlockLength() / 2f;
					gravityTriggerPos.x += blockHeight / 1.5f;
					gravityTriggerPos.y += Utils.ConvertBlockScaleToPosition(gravityTriggerPrefab.transform.localScale.x) / 2f;
					gravityTriggerPos.y += blockHeight / 2f;
					GameObject gravityTrigger = Instantiate(gravityTriggerPrefab, chunk.transform);
					gravityTrigger.transform.localPosition = gravityTriggerPos;
					gravityTrigger.GetComponent<GravityTrigger>().gravityDirection = Vector2.left;
					gravityTrigger.GetComponent<GravityTrigger>().playerDirection = Vector2.down;
					gravityTrigger.transform.Rotate(new Vector3(0, 0, 90f));

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

					Vector2 gravityTriggerPos = Vector2.zero;
					gravityTriggerPos.x -= chunk.GetBlockLength() / 2f;
					gravityTriggerPos.x += Utils.ConvertBlockScaleToPosition(gravityTriggerPrefab.transform.localScale.x) / 2f;
					gravityTriggerPos.x += blockHeight;
					gravityTriggerPos.y += blockHeight * 1.5f;
					GameObject gravityTrigger = Instantiate(gravityTriggerPrefab, chunk.transform);
					gravityTrigger.transform.localPosition = gravityTriggerPos;
					gravityTrigger.GetComponent<GravityTrigger>().gravityDirection = Vector2.up;
					gravityTrigger.GetComponent<GravityTrigger>().playerDirection = Vector2.left;

					chunk.transform.Rotate(new Vector3(0, 0, 180f));
				}
				else
				{
					lastGravityDirection = Vector2.down;

					instancePosition.x -= blockHeight / 2f;
					instancePosition.x += chunk.GetBlockLength() / 2f;
					instancePosition.y += blockHeight / 2f;
					instancePosition.y += lastBlockLength / 2f;

					Vector2 gravityTriggerPos = Vector2.zero;
					gravityTriggerPos.x -= chunk.GetBlockLength() / 2f;
					gravityTriggerPos.x += blockHeight / 1.5f;
					gravityTriggerPos.y += Utils.ConvertBlockScaleToPosition(gravityTriggerPrefab.transform.localScale.x) / 2f;
					gravityTriggerPos.y += blockHeight / 2f;
					GameObject gravityTrigger = Instantiate(gravityTriggerPrefab, chunk.transform);
					gravityTrigger.transform.localPosition = gravityTriggerPos;
					gravityTrigger.GetComponent<GravityTrigger>().gravityDirection = Vector2.down;
					gravityTrigger.GetComponent<GravityTrigger>().playerDirection = Vector2.right;
					gravityTrigger.transform.Rotate(new Vector3(0, 0, 90f));
				}
			}
			else if (lastGravityDirection == Vector2.left)
			{
				if (randomInt == 0)
				{
					lastGravityDirection = Vector2.up;

					instancePosition.x += blockHeight / 2f;
					instancePosition.x -= chunk.GetBlockLength() / 2f;
					instancePosition.y -= lastBlockLength / 2f;
					instancePosition.y -= blockHeight / 2f;

					Vector2 gravityTriggerPos = Vector2.zero;
					gravityTriggerPos.x -= chunk.GetBlockLength() / 2f;
					gravityTriggerPos.x += blockHeight / 1.5f;
					gravityTriggerPos.y += Utils.ConvertBlockScaleToPosition(gravityTriggerPrefab.transform.localScale.x) / 2f;
					gravityTriggerPos.y += blockHeight / 2f;
					GameObject gravityTrigger = Instantiate(gravityTriggerPrefab, chunk.transform);
					gravityTrigger.transform.localPosition = gravityTriggerPos;
					gravityTrigger.GetComponent<GravityTrigger>().gravityDirection = Vector2.up;
					gravityTrigger.GetComponent<GravityTrigger>().playerDirection = Vector2.left;
					gravityTrigger.transform.Rotate(new Vector3(0, 0, 90f));

					chunk.transform.Rotate(new Vector3(0, 0, 180f));
				}
				else
				{
					lastGravityDirection = Vector2.down;

					instancePosition.x -= blockHeight / 2f;
					instancePosition.x += chunk.GetBlockLength() / 2f;
					instancePosition.y -= lastBlockLength / 2f;
					instancePosition.y -= blockHeight / 2f;

					Vector2 gravityTriggerPos = Vector2.zero;
					gravityTriggerPos.x -= chunk.GetBlockLength() / 2f;
					gravityTriggerPos.x += Utils.ConvertBlockScaleToPosition(gravityTriggerPrefab.transform.localScale.x) / 2f;
					gravityTriggerPos.x += blockHeight;
					gravityTriggerPos.y += blockHeight * 1.5f;
					GameObject gravityTrigger = Instantiate(gravityTriggerPrefab, chunk.transform);
					gravityTrigger.transform.localPosition = gravityTriggerPos;
					gravityTrigger.GetComponent<GravityTrigger>().gravityDirection = Vector2.down;
					gravityTrigger.GetComponent<GravityTrigger>().playerDirection = Vector2.right;
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

					Vector2 gravityTriggerPos = Vector2.zero;
					gravityTriggerPos.x -= chunk.GetBlockLength() / 2f;
					gravityTriggerPos.x += Utils.ConvertBlockScaleToPosition(gravityTriggerPrefab.transform.localScale.x) / 2f;
					gravityTriggerPos.x += blockHeight;
					gravityTriggerPos.y += blockHeight * 1.5f;
					GameObject gravityTrigger = Instantiate(gravityTriggerPrefab, chunk.transform);
					gravityTrigger.transform.localPosition = gravityTriggerPos;
					gravityTrigger.GetComponent<GravityTrigger>().gravityDirection = Vector2.left;
					gravityTrigger.GetComponent<GravityTrigger>().playerDirection = Vector2.down;

					chunk.transform.Rotate(new Vector3(0, 0, -90f));
				}
				else
				{
					lastGravityDirection = Vector2.right;

					instancePosition.x -= lastBlockLength / 2f;
					instancePosition.x -= blockHeight / 2f;
					instancePosition.y += (chunk.GetBlockLength() - blockHeight) / 2f;

					Vector2 gravityTriggerPos = Vector2.zero;
					gravityTriggerPos.x -= chunk.GetBlockLength() / 2f;
					gravityTriggerPos.x += blockHeight / 1.5f;
					gravityTriggerPos.y += Utils.ConvertBlockScaleToPosition(gravityTriggerPrefab.transform.localScale.x) / 2f;
					gravityTriggerPos.y += blockHeight / 2f;
					GameObject gravityTrigger = Instantiate(gravityTriggerPrefab, chunk.transform);
					gravityTrigger.transform.localPosition = gravityTriggerPos;
					gravityTrigger.GetComponent<GravityTrigger>().gravityDirection = Vector2.right;
					gravityTrigger.GetComponent<GravityTrigger>().playerDirection = Vector2.up;
					gravityTrigger.transform.Rotate(new Vector3(0, 0, 90f));

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
