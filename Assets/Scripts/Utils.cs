using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    static public float ConvertBlockScaleToPosition(int length)
	{
		return length / 100f;
	}
}
