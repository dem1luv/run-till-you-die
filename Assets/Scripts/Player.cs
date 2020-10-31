using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] float movingSpeed = 1f;

	void Update()
    {
        if (Input.GetMouseButton(0))
		{
			transform.Translate(Vector2.right * movingSpeed * Time.deltaTime);
		}
    }
}
