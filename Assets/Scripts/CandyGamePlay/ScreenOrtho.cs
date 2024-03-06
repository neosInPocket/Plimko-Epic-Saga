using UnityEngine;

public static class ScreenOrtho
{
	public static Vector2 ScreenOrthoSize()
	{
		var ySize = Camera.main.orthographicSize;
		float screenRatioValue = (float)Screen.width / (float)Screen.height;
		var xSize = screenRatioValue * ySize;
		return new Vector2(xSize, ySize);
	}

	public static Vector2 GetWorldPoint(Vector2 pointFromScreen)
	{
		var screenSize = ScreenOrthoSize();
		var xPos = 2 * pointFromScreen.x / Screen.width * screenSize.x - screenSize.x;
		var yPos = 2 * pointFromScreen.y / Screen.height * screenSize.y - screenSize.y;

		return new Vector2(xPos, yPos);
	}
}
