using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeedDecreasePopup : MonoBehaviour
{
	[SerializeField] private float decreaseAmount;
	[SerializeField] private OrbitsSpawner orbitsSpawner;
	[SerializeField] private Image fillImage;
	[SerializeField] private Button button;
	[SerializeField] private TMP_Text amountText;
	private float currentTime;

	public void Enable(bool value)
	{
		button.interactable = value;
	}

	private void Start()
	{
		if (DataControls.DataObject.isUpgraded[0] == 0)
		{
			button.interactable = false;
			amountText.color = Color.red;
		}
		amountText.text = DataControls.DataObject.isUpgraded[0].ToString();
	}

	public void StartPopup()
	{
		fillImage.gameObject.SetActive(true);
		fillImage.fillAmount = 1f;
		currentTime = 0;
		button.interactable = false;
		DataControls.DataObject.isUpgraded[0]--;
		amountText.text = DataControls.DataObject.isUpgraded[0].ToString();
		DataControls.Save();

		if (DataControls.DataObject.isUpgraded[0] <= 0)
		{
			button.interactable = false;
			amountText.color = Color.red;
		}

		StartCoroutine(StartTimer());
	}

	private IEnumerator StartTimer()
	{
		orbitsSpawner.DecreaseAllSpeed(true, decreaseAmount);

		while (fillImage.fillAmount > 0)
		{
			currentTime += Time.deltaTime;
			fillImage.fillAmount = 1f - currentTime / 5f;
			yield return null;
		}

		orbitsSpawner.DecreaseAllSpeed(false);
		fillImage.gameObject.SetActive(false);

		if (DataControls.DataObject.isUpgraded[0] <= 0)
		{
			button.interactable = false;
		}
		else
		{
			button.interactable = true;
		}
	}
}
