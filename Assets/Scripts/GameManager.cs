using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static void LoseGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		Physics2D.gravity = new Vector2(0, -9.81f);
	}
}
