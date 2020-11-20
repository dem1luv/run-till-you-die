using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkObject : MonoBehaviour
{
    SpriteRenderer sr;
    Color objectColor;
    public int id = -1;

    void Start()
    {
        Transform nextParent = transform.parent;
        Chunk chunk = nextParent.GetComponent<Chunk>();
        while (!(chunk = nextParent.GetComponent<Chunk>()))
        {
            nextParent = nextParent.parent;
        }
        id = chunk.id;

        sr = GetComponent<SpriteRenderer>();
        objectColor = sr.color;

        objectColor.a = 0;
        sr.color = objectColor;

        StartCoroutine("Show");

        GameEvents.GetCurrent().onChunkDestroy += OnChunkDestroy;
    }
    IEnumerator Show()
	{
        for(int i = 0; i < 25; i++)
		{
            objectColor.a += 0.04f;
            sr.color = objectColor;
            yield return new WaitForSeconds(0.02f);
        }
	}
    IEnumerator Destroy()
	{
        for(int i = 0; i < 25; i++)
		{
            objectColor.a -= 0.04f;
            sr.color = objectColor;
            yield return new WaitForSeconds(0.02f);
        }
	}
    private void OnChunkDestroy(int id)
	{
        if (id == this.id)
		{
            StartCoroutine("Destroy");
        }
    }
    private void OnDestroy()
    {
        GameEvents.GetCurrent().onChunkDestroy -= OnChunkDestroy;
    }
}
