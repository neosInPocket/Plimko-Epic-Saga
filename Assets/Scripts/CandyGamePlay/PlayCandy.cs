using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayCandy : MonoBehaviour
{
	[SerializeField] private Material invincibleMaterial;
	[SerializeField] private Material normalMaterial;
	[SerializeField] private MeshRenderer meshRenderer;
	[SerializeField] private float rotateSpeed;
	[SerializeField] private float rotateSpeedMultiplier;
	[SerializeField] private float maxScale;
	[SerializeField] private GameObject explosion;
	public bool IsBallEnabled
	{
		get => isBallEnabled;
		set
		{
			isBallEnabled = value;
			if (value)
			{
				Touch.onFingerDown += OnBallFingerDown;
			}
			else
			{
				Touch.onFingerDown -= OnBallFingerDown;
			}
		}
	}

	private bool isBallEnabled;

	private Vector3 currentScale;
	private float maxRadius;
	public Action CoinCollected { get; set; }
	public Action Lose { get; set; }

	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	public void SetBallData(float maxRadius)
	{
		this.maxRadius = maxRadius;
	}

	private void OnBallFingerDown(Finger finger)
	{

	}

	private void OnTriggerEnter(Collider collider)
	{
		if (collider.TryGetComponent<CandyCoin>(out CandyCoin coin))
		{
			CoinCollected?.Invoke();
			coin.CollectCandy();
			return;
		}

		if (collider.TryGetComponent<CandySphere>(out CandySphere sphere))
		{
			Destroyed();
			return;
		}
	}

	private void Update()
	{
		if (maxRadius != 0)
		{
			GetCurrentScaleFromRadius();
		}

		if (!IsBallEnabled) return;

		transform.RotateAround(Vector3.zero, Vector2.up, rotateSpeed * Time.deltaTime * Mathf.Deg2Rad * rotateSpeedMultiplier);
	}

	private void GetCurrentScaleFromRadius()
	{
		var radius = Vector3.Distance(Vector3.zero, transform.position);
		float radiusRatio = radius / maxRadius;

		transform.localScale = Vector3.one * radiusRatio * maxScale;
	}

	private bool isInvincible;

	public void SetInvincible(bool value)
	{
		if (value)
		{
			meshRenderer.material = invincibleMaterial;
			isInvincible = true;
		}
		else
		{
			meshRenderer.material = normalMaterial;
			isInvincible = false;
		}
	}

	public void Destroyed()
	{
		IsBallEnabled = false;
		Lose?.Invoke();
		meshRenderer.enabled = false;
		explosion.SetActive(true);
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= OnBallFingerDown;
	}
}
