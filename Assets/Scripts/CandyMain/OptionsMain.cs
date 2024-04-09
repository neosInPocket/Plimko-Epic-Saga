using UnityEngine;
using UnityEngine.UI;

public class OptionsMain : MonoBehaviour
{
	[SerializeField] private Image musicImage;
	[SerializeField] private Image effectsImage;
	[SerializeField] private Color inactiveColor;
	private SoundPreloader activeSound;

	private void Start()
	{
		activeSound = FindFirstObjectByType<SoundPreloader>();
	}

	public void SetMusic()
	{
		musicImage.color = musicImage.color == Color.white ? inactiveColor : Color.white;

		bool musicState = false;

		if (musicImage.color == Color.white)
		{
			musicState = true;
		}

		activeSound.Set(musicState);
		DataControls.DataObject.volumeStatuses[0] = musicState;
		DataControls.Save();
	}

	public void SetEffects()
	{
		effectsImage.color = effectsImage.color == Color.white ? inactiveColor : Color.white;

		bool effectsState = false;

		if (effectsImage.color == Color.white)
		{
			effectsState = true;
		}

		DataControls.DataObject.volumeStatuses[1] = effectsState;
		DataControls.Save();
	}
}
