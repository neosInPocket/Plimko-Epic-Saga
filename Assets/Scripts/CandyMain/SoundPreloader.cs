using System.Linq;
using UnityEngine;

public class SoundPreloader : MonoBehaviour
{
	[SerializeField] private AudioSource musicSource;

	private void Awake()
	{
		SoundPreloader[] soundPreloader = FindObjectsByType<SoundPreloader>(sortMode: FindObjectsSortMode.None);

		if (soundPreloader.Length == 1)
		{
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			var thisSound = soundPreloader.FirstOrDefault(x => x.gameObject.scene.name != "DontDestroyOnLoad");
			Destroy(thisSound.gameObject);
		}
	}

	private void Start()
	{
		Set(DataControls.DataObject.soundValues[0]);
	}

	public void Set(bool volumeValue)
	{
		musicSource.volume = volumeValue == true ? 1f : 0f;
	}
}
