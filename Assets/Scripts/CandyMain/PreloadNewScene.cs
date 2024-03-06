using UnityEngine;
using UnityEngine.SceneManagement;

public class PreloadNewScene : MonoBehaviour
{
	public void PreloadNewSceneAction()
	{
		SceneManager.LoadScene("CandyGameScene");
	}
}
