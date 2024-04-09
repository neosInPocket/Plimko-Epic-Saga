using UnityEngine;
using UnityEngine.SceneManagement;

public class PreloadNewScene : MonoBehaviour
{
	public void PreloadNewSceneAction()
	{
		SceneManager.LoadScene("HeadSideScene");
	}

	public void PreloadOldSceneAction()
	{
		SceneManager.LoadScene("HeadScene");
	}
}
