using TMPro;
using UnityEngine;

public class LevelPreload : MonoBehaviour
{
	[SerializeField] private TMP_Text levelPreloadText;

	private void Start()
	{
		levelPreloadText.text = "LEVEL" + " " + DataControls.DataObject.runningLevel.ToString();
	}
}
