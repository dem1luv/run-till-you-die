using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
	[SerializeField] float startDelay = 1f;
	[SerializeField] float upDelay = 4f;
	[SerializeField] float downDelay = 4f;
	[Space]
	[SerializeField] Animator spikeAlarmAnimator;
	
	void Start()
	{
		StartCoroutine("Move");
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
