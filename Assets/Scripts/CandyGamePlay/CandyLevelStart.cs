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
	[SerializeField] private GameObject screenStart;
	private int candiesTargetAction;
	private int collectedCandies;

	private void Awake()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	public virtual void Start()
	{
		candiesTargetAction = DataControls.DataObject.runningLevel * 4 - 1;

		LevelInfoGetter();
		orbitsSpawner.SetData();

		if (DataControls.DataObject.recover)
		{
			DataControls.DataObject.recover = false;
			DataControls.Save();
			educationSystem.EducateSystem();
			educationSystem.EducationEnded += OnEducationSystemEnd;
		}
		else
		{
			OnEducationSystemEnd();
		}
	}

	public virtual void OnEducationSystemEnd()
	{
		educationSystem.EducationEnded -= OnEducationSystemEnd;

		if (screenStart != null)
		{
			screenStart.SetActive(true);
		}

		Touch.onFingerDown += StartTapGame;
	}

	public virtual void StartTapGame(Finger finger)
	{
		if (screenStart != null)
		{
			screenStart.SetActive(false);
		}

		Touch.onFingerDown -= StartTapGame;

		orbitsSpawner.EnableStatus(true);
		playCandy.IsBallEnabled = true;
		playCandy.CoinCollected += CoinCollectAction;
		playCandy.Lose += LoseCondition;
	}

	public void CoinCollectAction()
	{
		collectedCandies++;

		if (collectedCandies >= candiesTargetAction)
		{
			orbitsSpawner.EnableStatus(false);
			playCandy.IsBallEnabled = false;
			playCandy.CoinCollected -= CoinCollectAction;
			playCandy.Lose -= LoseCondition;

			candyBattleResult.ShowBattleResult(candiesTargetAction);
		}

		candiesAmount.text = $"{collectedCandies}/{candiesTargetAction}";
		candiesImage.fillAmount = (float)collectedCandies / (float)candiesTargetAction;
	}

	public void LoseCondition()
	{
		orbitsSpawner.EnableStatus(false);
		candyBattleResult.ShowBattleResult(0);
		playCandy.IsBallEnabled = false;
		playCandy.CoinCollected -= CoinCollectAction;
		playCandy.Lose -= LoseCondition;
	}

	public void LevelInfoGetter()
	{
		levelInformation.text = $"level {DataControls.DataObject.runningLevel}";
		candiesAmount.text = $"{collectedCandies}/{candiesTargetAction}";
		candiesImage.fillAmount = (float)collectedCandies / (float)candiesTargetAction;
	}
}
