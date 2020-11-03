using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneratorManager : MonoBehaviour
{
    [SerializeField] GameObject chunkPrefab;

	Vector2 instancePosition = Vector2.zero;

	int lastBlockLength = 0;

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

			instancePosition = chunkObject.transform.position;
			instancePosition.x += Utils.ConvertBlockScaleToPosition(lastBlockLength) / 2f;
			lastBlockLength = chunk.GetBlockLength();
			instancePosition.x += Utils.ConvertBlockScaleToPosition(lastBlockLength) / 2f;

			chunkObject.transform.position = instancePosition;

			yield return new WaitForSeconds(0.5f);
		}
	}
}
