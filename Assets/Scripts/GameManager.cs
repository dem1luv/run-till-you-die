using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Current { get; private set; }

	public float CurrentMeterCount { get; private set; }
	private float recordMeterCount;

	public bool IsLosed { get; private set; } = false;

	private UIManager uiManager;

	private void Awake()
	{
		Current = this;
	}
	private void Start()
	{
		uiManager = UIManager.Current;

		if (PlayerPrefs.HasKey("recordMeterCount"))
			recordMeterCount = PlayerPrefs.GetFloat("recordMeterCount");
		else
			recordMeterCount = 0;

		uiManager.UpdateTextsMeter(0, recordMeterCount);
	}
	private IEnumerator LoseGame()
	{
		IsLosed = true;
		Player.Current.Die();

		if (CurrentMeterCount != 0)
		{
			PlayerPrefs.SetFloat("audioCockdillac.time", Player.Current.audioCockdillac.time);
			yield return new WaitForSeconds(1.5f);
		}

		if ((int)CurrentMeterCount > (int)recordMeterCount)
		{
			recordMeterCount = CurrentMeterCount;
			PlayerPrefs.SetFloat("recordMeterCount", recordMeterCount);

			uiManager.ShowPanelNewRecord(recordMeterCount);
		}
		else
			ReloadScene();
	}
	public void ReloadScene()
	{
		CurrentMeterCount = 0;

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		IsLosed = false;
		Physics2D.gravity = new Vector2(0, -9.81f);
	}
	public void IncreaseMeters(float increase)
	{
		CurrentMeterCount += increase;
		uiManager.UpdateTextsMeter(CurrentMeterCount, recordMeterCount);
	}
	private void OnApplicationQuit()
	{
		PlayerPrefs.DeleteKey("audioCockdillac.time");
	}
}
