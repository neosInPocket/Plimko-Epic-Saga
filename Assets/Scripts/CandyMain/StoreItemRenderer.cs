using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemRenderer : MonoBehaviour
{
	[SerializeField] private StoreItem storeItem;
	[SerializeField] private TMP_Text costValue;
	[SerializeField] private TMP_Text hasValue;
	[SerializeField] private Button buyButton;
	[SerializeField] private Button infoButton;
	[SerializeField] private TMP_Text buyButtonText;
	[SerializeField] private StoreRenderer storeRenderer;
	[SerializeField] private TMP_Text infoText;
	[SerializeField] private Image infoIcon;
	[SerializeField] private Sprite upgradeIcon;

	private void Start()
	{
		InitializeItem();
	}

	private void InitializeItem()
	{
		costValue.text = storeItem.Cost.ToString();
		buyButton.onClick.AddListener(BuyIndex);
		infoButton.onClick.AddListener(ShowUpgradeInfo);
	}

	public void RefreshItem()
	{
		hasValue.text = DataControls.DataObject.upgradedSections[storeItem.Index].ToString();

		if (DataControls.DataObject.candiesStacks >= storeItem.Cost)
		{
			buyButton.interactable = true;
			buyButtonText.color = Color.white;
			buyButtonText.text = "PURCHASE";
		}
		else
		{
			buyButton.interactable = false;
			buyButtonText.color = Color.red;
			buyButtonText.text = "NO COINS";
		}
	}

	public void BuyIndex()
	{
		DataControls.DataObject.candiesStacks -= storeItem.Cost;
		DataControls.DataObject.upgradedSections[storeItem.Index]++;
		DataControls.Save();

		storeRenderer.RefreshItems();
	}

	private void ShowUpgradeInfo()
	{
		infoText.transform.parent.parent.gameObject.SetActive(true);
		infoText.text = storeItem.Description;
		infoIcon.sprite = upgradeIcon;
	}
}
