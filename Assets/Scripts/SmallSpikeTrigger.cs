using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSpikeTrigger : MonoBehaviour
{
    [SerializeField] SmallSpike smallSpike;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
			smallSpike.Fall();
	}
}
