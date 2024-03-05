using System.IO;
using UnityEngine;

public class DataControls : MonoBehaviour
{
	[SerializeField] private DataObject defaultDataControls;
	[SerializeField] private bool deleteData;
	private static string pathToSave;
	public static DataObject DataObject;
	private static DataObject DefaultDataControls;

	private void Awake()
	{
		DefaultDataControls = defaultDataControls;
		pathToSave = Application.persistentDataPath + "/DataControls" + ".json";

		if (deleteData)
		{
			NewDataControls();

		}
		else
		{
			LoadFrom();
		}
	}

	public static void Save()
	{
		if (File.Exists(pathToSave))
		{
			SetDataControls();

		}
		else
		{
			NewDataControls();
		}
	}

	private void LoadFrom()
	{
		if (File.Exists(pathToSave))
		{
			LoadHardware();

		}
		else
		{
			NewDataControls();
		}
	}

	private static void NewDataControls()
	{
		DataObject = new DataObject().GetDefaultData(DefaultDataControls);
		File.WriteAllText(pathToSave, JsonUtility.ToJson(DataObject));
	}

	private static void SetDataControls()
	{
		File.WriteAllText(pathToSave, JsonUtility.ToJson(DataObject));
	}

	private static void LoadHardware()
	{
		string file = File.ReadAllText(pathToSave);
		DataObject = JsonUtility.FromJson<DataObject>(file);
	}
}
