using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class CandyLevelStart : MonoBehaviour
{
	[SerializeField] private OrbitsSpawner orbitsSpawner;
	[SerializeField] private PlayCandy playCandy;

	private void Awake()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	private void Start()
	{
		orbitsSpawner.SetData();
		orbitsSpawner.Enable(true);

		playCandy.IsBallEnabled = true;
	}
}
