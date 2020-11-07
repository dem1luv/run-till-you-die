using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
	[SerializeField] Animator spikeAlarmAnimator;

	float startDelay;
	float upDelay;
	float downDelay;
	
	void Start()
	{
		StartCoroutine("Move");

		startDelay = Random.Range(1f, 3f);
		upDelay = Random.Range(2f, 5f);
		downDelay = Random.Range(1f, 2f);
	}

	IEnumerator Move()
	{
		yield return new WaitForSeconds(startDelay);
		while (true)
		{
			transform.Translate(new Vector2(0, -1.46f));

			yield return new WaitForSeconds(upDelay - 2f);
			
			spikeAlarmAnimator.SetInteger("state", 1);
			yield return new WaitForSeconds(1f);
			spikeAlarmAnimator.SetInteger("state", 0);
			yield return new WaitForSeconds(1f);

			transform.Translate(new Vector2(0, 1.46f));

			yield return new WaitForSeconds(downDelay);
		}
	}
}
