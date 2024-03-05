using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UpgradeItem")]
public class StoreItem : ScriptableObject
{
	[SerializeField] private int cost;
	[SerializeField] private string description;
	[SerializeField] private int index;

	public int Cost => cost;
	public string Description => description;
	public int Index => index;
}
