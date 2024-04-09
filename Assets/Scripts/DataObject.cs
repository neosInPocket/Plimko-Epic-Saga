using System;
using System.Collections.Generic;

[Serializable]
public class DataObject
{
	public int runningLevel;
	public int candiesStacks;
	public List<int> upgradedSections;
	public List<bool> volumeStatuses;
	public bool recover;

	public DataObject DefaultsAdapter(DataObject defaultValues)
	{
		var dataObject = new DataObject();

		dataObject.runningLevel = defaultValues.runningLevel;
		dataObject.candiesStacks = defaultValues.candiesStacks;
		dataObject.upgradedSections = defaultValues.upgradedSections;
		dataObject.volumeStatuses = defaultValues.volumeStatuses;
		dataObject.recover = defaultValues.recover;

		return dataObject;
	}
}
