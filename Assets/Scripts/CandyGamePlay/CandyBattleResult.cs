using TMPro;
using UnityEngine;

public class CandyBattleResult : MonoBehaviour
{
	[SerializeField] private TMP_Text resultText;
	[SerializeField] private TMP_Text rareCandiesAmount;

	public void ShowBattleResult(int coinsAmount)
	{
		gameObject.SetActive(true);

		if (coinsAmount <= 0)
		{
			resultText.text = "YOU LOSE";
		}
		else
		{
			resultText.text = "YOU WIN!";
		}

		rareCandiesAmount.text = coinsAmount.ToString();
	}
}
