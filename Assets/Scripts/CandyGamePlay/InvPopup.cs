using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvPopup : MonoBehaviour
{
	[SerializeField] private PlayCandy playCandy;
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
		if (DataControls.DataObject.isUpgraded[1] == 0)
		{
			button.interactable = false;
			amountText.color = Color.red;
		}
		amountText.text = DataControls.DataObject.isUpgraded[1].ToString();
	}

	public void StartPopup()
	{
		fillImage.gameObject.SetActive(true);
		fillImage.fillAmount = 1f;
		currentTime = 0;
		button.interactable = false;
		DataControls.DataObject.isUpgraded[1]--;
		amountText.text = DataControls.DataObject.isUpgraded[1].ToString();
		DataControls.Save();

		if (DataControls.DataObject.isUpgraded[1] <= 0)
		{
			button.interactable = false;
			amountText.color = Color.red;
		}

		StartCoroutine(StartTimer());
	}

	private IEnumerator StartTimer()
	{
		playCandy.SetInvincible(true);

		while (fillImage.fillAmount > 0)
		{
			currentTime += Time.deltaTime;
			fillImage.fillAmount = 1f - currentTime / 3f;
			yield return null;
		}

		playCandy.SetInvincible(false);
		fillImage.gameObject.SetActive(false);

		if (DataControls.DataObject.isUpgraded[1] <= 0)
		{
			button.interactable = false;
		}
		else
		{
			button.interactable = true;
		}
	}
}
