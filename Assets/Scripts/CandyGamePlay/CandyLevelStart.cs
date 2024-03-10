using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class CandyLevelStart : MonoBehaviour
{
	[SerializeField] private OrbitsSpawner orbitsSpawner;
	[SerializeField] private PlayCandy playCandy;
	[SerializeField] private TMP_Text candiesAmount;
	[SerializeField] private Image candiesImage;
	[SerializeField] private TMP_Text levelInformation;
	[SerializeField] private EducationSystem educationSystem;
	[SerializeField] private CandyBattleResult candyBattleResult;
	[SerializeField] private GameObject tapScreenGameStart;
	private int needeCandies;
	private int currentCollectedCandies;

	private void Awake()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	private void Start()
	{
		needeCandies = DataControls.DataObject.currentLevel * 4 - 1;

		SetLevelInformation();
		orbitsSpawner.SetData();



		if (DataControls.DataObject.recovery)
		{
			DataControls.DataObject.recovery = false;
			DataControls.Save();
			educationSystem.Educate(OnEducationSystemEnd);
		}
		else
		{
			OnEducationSystemEnd();
		}
	}

	private void OnEducationSystemEnd()
	{
		if (tapScreenGameStart != null)
		{
			tapScreenGameStart.SetActive(true);
		}

		Touch.onFingerDown += StartTapGame;
	}

	private void StartTapGame(Finger finger)
	{
		if (tapScreenGameStart != null)
		{
			tapScreenGameStart.SetActive(false);
		}

		Touch.onFingerDown -= StartTapGame;

		orbitsSpawner.Enable(true);
		playCandy.IsBallEnabled = true;
		playCandy.CoinCollected += OnCoinCollected;
		playCandy.Lose += OnLose;
	}

	private void OnCoinCollected()
	{
		currentCollectedCandies++;

		if (currentCollectedCandies >= needeCandies)
		{
			orbitsSpawner.Enable(false);
			playCandy.IsBallEnabled = false;
			playCandy.CoinCollected -= OnCoinCollected;
			playCandy.Lose -= OnLose;

			candyBattleResult.ShowBattleResult(needeCandies);
		}

		candiesAmount.text = $"{currentCollectedCandies}/{needeCandies}";
		candiesImage.fillAmount = (float)currentCollectedCandies / (float)needeCandies;
	}

	private void OnLose()
	{
		orbitsSpawner.Enable(false);
		candyBattleResult.ShowBattleResult(0);
		playCandy.IsBallEnabled = false;
		playCandy.CoinCollected -= OnCoinCollected;
		playCandy.Lose -= OnLose;
	}

	public void SetLevelInformation()
	{
		levelInformation.text = $"LEVEL {DataControls.DataObject.currentLevel}";
		candiesAmount.text = $"{currentCollectedCandies}/{needeCandies}";
		candiesImage.fillAmount = (float)currentCollectedCandies / (float)needeCandies;
	}
}
