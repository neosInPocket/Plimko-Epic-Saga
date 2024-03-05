using System;
using System.Collections.Generic;

[Serializable]
public class DataObject
{
	public int currentLevel;
	public int coinsStacks;
	public List<int> isUpgraded;
	public List<bool> soundValues;
	public bool recovery;

	public DataObject GetDefaultData(DataObject defaultValues)
	{
		var dataObject = new DataObject();

		dataObject.currentLevel = defaultValues.currentLevel;
		dataObject.coinsStacks = defaultValues.coinsStacks;
		dataObject.isUpgraded = defaultValues.isUpgraded;
		dataObject.soundValues = defaultValues.soundValues;
		dataObject.recovery = defaultValues.recovery;

		return dataObject;
	}
}
