using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[SerializeField] Text textCurrentMeters;
    [SerializeField] Text textRecordMeters;

	[SerializeField] GameObject panelNewRecord;
	[SerializeField] Text textPanelRecordMeter;

	public void UpdateTextsMeter(float newCurrentMeterCount, float newRecordMeterCount)
	{
		textCurrentMeters.text = string.Format("current: {0:0}m", newCurrentMeterCount);
		textRecordMeters.text = string.Format("record: {0:0}m", newRecordMeterCount);
	}
	public void ShowPanelNewRecord(float newRecordMeterCount)
	{
		textPanelRecordMeter.text = string.Format("{0:0}m", newRecordMeterCount);
		panelNewRecord.SetActive(true);
	}
	public void OnClickPanelNewRecord()
	{
		GameManager.ReloadScene();
	}
}
