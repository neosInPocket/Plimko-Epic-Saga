using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsPreload : MonoBehaviour
{
	[SerializeField] private TMP_Text coinsPreloadText;

	private void Start()
	{
		coinsPreloadText.text = DataControls.DataObject.coinsStacks.ToString();
	}

	public void Preload()
	{
		coinsPreloadText.text = DataControls.DataObject.coinsStacks.ToString();
	}
}
