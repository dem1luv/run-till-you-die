using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private static GameManager current;
	public static GameManager Current
	{
		get { return current; }
	}

	static float currentMeterCount;
	public static float CurrentMeterCount
	{
		get { return currentMeterCount; }
	}
	static float recordMeterCount;
	
	static bool isLosed = false;
	static public bool IsLosed
	{
		get { return isLosed; }
	}

	static UIManager uiManager;

	private void Awake()
	{
		current = this;
	}
	void Start()
	{
		uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

		if (PlayerPrefs.HasKey("recordMeterCount"))
			recordMeterCount = PlayerPrefs.GetFloat("recordMeterCount");
		else
			recordMeterCount = 0;

		uiManager.UpdateTextsMeter(0, recordMeterCount);
	}

	private IEnumerator LoseGame()
	{
		isLosed = true;
		Player.Current.Die();

		if (currentMeterCount != 0)
		{
			PlayerPrefs.SetFloat("audioCockdillac.time", Player.Current.audioCockdillac.time);
			yield return new WaitForSeconds(1.5f);
		}

		if ((int)currentMeterCount > (int)recordMeterCount)
		{
			recordMeterCount = currentMeterCount;
			PlayerPrefs.SetFloat("recordMeterCount", recordMeterCount);

			uiManager.ShowPanelNewRecord(recordMeterCount);
		}
		else
			ReloadScene();
	}

	public void ReloadScene()
	{
		currentMeterCount = 0;

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		isLosed = false;
		Physics2D.gravity = new Vector2(0, -9.81f);
	}

	public void IncreaseMeters(float increase)
	{
		currentMeterCount += increase;
		uiManager.UpdateTextsMeter(currentMeterCount, recordMeterCount);
	}

	private void OnApplicationQuit()
	{
		PlayerPrefs.DeleteKey("audioCockdillac.time");
	}
}
