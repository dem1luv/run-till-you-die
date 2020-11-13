using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    static public float ConvertBlockScaleToPosition(float length)
	{
		return length / 100f;
	}
	static public float ConvertPositionToBlockScale(float length)
	{
		return length * 100f;
	}
}
