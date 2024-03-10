using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CandyBattleResult : MonoBehaviour
{
	[SerializeField] private TMP_Text winText;
	[SerializeField] private TMP_Text coinsAmountText;
	[SerializeField] private Button nextLevel;
	[SerializeField] private Button mainMenu;

	public void ShowBattleResult(int coinsAmount)
	{
		gameObject.SetActive(true);
		nextLevel.onClick.AddListener(() => SceneManager.LoadScene("CandyGameScene"));
		mainMenu.onClick.AddListener(() => SceneManager.LoadScene("CandyMain"));

		if (coinsAmount <= 0)
		{
			winText.text = "LOSE";
		}
		else
		{
			winText.text = "WIN!";
		}

		coinsAmountText.text = coinsAmount.ToString();
	}
}
