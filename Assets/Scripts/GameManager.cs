using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	static float currentMeterCount;
	static float recordMeterCount;
	
	static bool isLosed = false;
	static public bool IsLosed
	{
		get { return isLosed; }
	}

	static UIManager uiManager;

	void Start()
	{
		uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

		if (PlayerPrefs.HasKey("recordMeterCount"))
			recordMeterCount = PlayerPrefs.GetFloat("recordMeterCount");
		else
			recordMeterCount = 0;

		uiManager.UpdateTextsMeter(0, recordMeterCount);
	}

	public static void LoseGame()
	{
		isLosed = true;
		if ((int)currentMeterCount > (int)recordMeterCount)
		{
			recordMeterCount = currentMeterCount;
			PlayerPrefs.SetFloat("recordMeterCount", recordMeterCount);

			uiManager.ShowPanelNewRecord(recordMeterCount);
		}
		else
			ReloadScene();
	}

	public static void ReloadScene()
	{
		currentMeterCount = 0;

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		isLosed = false;
		Physics2D.gravity = new Vector2(0, -9.81f);
	}

	public static void IncreaseMeters(float increase)
	{
		currentMeterCount += increase;
		uiManager.UpdateTextsMeter(currentMeterCount, recordMeterCount);
	}
}
