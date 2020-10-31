using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	void Update()
    {
        if (Input.GetMouseButton(0))
		{
			transform.Translate(Vector2.right * Time.deltaTime);
		}
    }
}
