using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
	Animator spikeAnim;
	[SerializeField] Animator spikeAlarmAnim;

	float startDelay;
	float upDelay;
	float downDelay;

	private void Start()
	{
		spikeAnim = GetComponent<Animator>();
	}

	public void Generate(float startDelay, float upDelay, float downDelay)
	{
		this.startDelay = startDelay;
		this.upDelay = upDelay;
		this.downDelay = downDelay;

		StartCoroutine("Move");
	}

	public void GenerateRandom()
	{
		startDelay = Random.Range(1f, 3f);
		upDelay = Random.Range(2f, 5f);
		downDelay = Random.Range(1f, 2f);

		StartCoroutine("Move");
	}

	IEnumerator Move()
	{
		yield return new WaitForSeconds(startDelay);
		while (true)
		{
			spikeAnim.SetInteger("state", 0);

			yield return new WaitForSeconds(upDelay - 2f + 0.5f);

			spikeAlarmAnim.SetInteger("state", 1);
			yield return new WaitForSeconds(1f);
			spikeAlarmAnim.SetInteger("state", 0);
			yield return new WaitForSeconds(1f);

			spikeAnim.SetInteger("state", 1);

			yield return new WaitForSeconds(downDelay - 0.5f);
		}
	}
}
