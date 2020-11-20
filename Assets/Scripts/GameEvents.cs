using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    private static GameEvents current;

	private void Awake()
	{
		current = this;
	}
	public static GameEvents GetCurrent()
	{
		return current;
	}
	public event Action<int> onChunkDestroy;
	public void ChunkDestroy(int id)
	{
		onChunkDestroy?.Invoke(id);
	}
}
