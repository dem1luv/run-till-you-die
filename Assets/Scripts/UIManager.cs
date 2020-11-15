using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text textMeter;

	public void UpdateTextMeter(float newMeterCount)
	{
		textMeter.text = string.Format("{0:0}m", newMeterCount);
	}
}
