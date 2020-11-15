using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	static float meterCount;

	static UIManager uiManager;

	void Start()
	{
		uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
	}

	public static void LoseGame()
	{
		meterCount = 0;

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		Physics2D.gravity = new Vector2(0, -9.81f);
	}

	public static void IncreaseMeters(float increase)
	{
		meterCount += increase;
		uiManager.UpdateTextMeter(meterCount);
	}
}
