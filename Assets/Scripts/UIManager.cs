using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager Current { get; private set; }

	[SerializeField] Text textCurrentMeters;
    [SerializeField] Text textRecordMeters;

	[SerializeField] GameObject panelNewRecord;
	[SerializeField] Text textPanelRecordMeter;

	private void Awake()
	{
		Current = this;
	}
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
		GameManager.Current.ReloadScene();
	}
}
