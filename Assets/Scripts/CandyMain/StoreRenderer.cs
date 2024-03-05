using System.Collections.Generic;
using UnityEngine;

public class StoreRenderer : MonoBehaviour
{
	[SerializeField] private List<StoreItemRenderer> storeItemRenderers;
	[SerializeField] private List<CoinsPreload> coinsPreloads;

	private void Start()
	{
		RefreshItems();
	}

	public void RefreshItems()
	{
		storeItemRenderers.ForEach(x => x.RefreshItem());
		coinsPreloads.ForEach(x => x.Preload());
	}
}
