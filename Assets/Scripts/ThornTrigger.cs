using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornTrigger : MonoBehaviour
{
    [SerializeField] Thorn thorn;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
			thorn.Fall();
	}
}
