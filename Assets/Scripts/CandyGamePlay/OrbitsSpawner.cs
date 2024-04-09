using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrbitsSpawner : MonoBehaviour
{
	[SerializeField] private OrbitRenderer orbitRendererPrefab;
	[SerializeField] private CandyCoin candyPrefab;
	[SerializeField] private CandyCoin startCandy;
	[SerializeField] private CandySphere candySpheresPrefab;
	[SerializeField] private int spawnCount;
	[Range(0, 1f)]
	[SerializeField] private float minWidthViaScreenSize;
	[Range(0, 1f)]
	[SerializeField] private float maxWidthViaScreenSize;
	[SerializeField] private float defaultWidth = 3;
	[SerializeField] private Vector3 defaultRotation;
	[SerializeField] private float tubeRadius;
	[SerializeField] private PlayCandy playCandy;
	private CandyCoin runningCandy;
	public List<OrbitRenderer> orbitsList;
	public List<CandySphere> spheresList;

	public void SetData()
	{
		runningCandy = startCandy;
		spheresList = new List<CandySphere>();
		orbitsList = new List<OrbitRenderer>();


		var screenSize = ScreenOrtho.ScreenOrthoSize();
		float maxOrbitScale = GetMaxOrbitScale(screenSize);
		float minOrbitScale = GetMinOrbitScale(screenSize); ;
		float deltaScale = GetDeltaScale(maxOrbitScale, minOrbitScale);
		float currentScale = maxOrbitScale;

		startCandy.transform.position = new Vector3(3f / 2f * currentScale - tubeRadius, 0, 0);
		startCandy.OnCandyCollected += CandyCollectedOption;
		startCandy.SetScale(currentScale);
		playCandy.transform.position = new Vector3(0, 0, 3f / 2f * currentScale - tubeRadius);
		playCandy.SetBallData(3f / 2f * currentScale - tubeRadius);

		for (int i = 0; i < spawnCount; i++)
		{
			var orbit = Instantiate(orbitRendererPrefab, Vector2.zero, Quaternion.Euler(defaultRotation), transform);
			var sphere = Instantiate(candySpheresPrefab, transform);
			sphere.SetMaterial();
			sphere.transform.localScale = sphere.DefaultScale * currentScale * Vector3.one;
			sphere.transform.position = new Vector3(-3f / 2f * currentScale + tubeRadius, 0, 0);
			orbit.transform.localScale = Vector3.one * currentScale;
			orbitsList.Add(orbit);
			spheresList.Add(sphere);

			currentScale -= deltaScale;
		}
	}

	public float GetMaxOrbitScale(Vector2 screenSize)
	{
		return 2 * screenSize.x * maxWidthViaScreenSize / defaultWidth;
	}

	public float GetMinOrbitScale(Vector2 screenSize)
	{
		return 2 * screenSize.x * minWidthViaScreenSize / defaultWidth;
	}

	public float GetDeltaScale(float maxOrbitScale, float minOrbitScale)
	{
		return (maxOrbitScale - minOrbitScale) / (spawnCount - 1);
	}

	private void CandyCollectedOption(CandyCoin coin)
	{
		coin.OnCandyCollected -= CandyCollectedOption;
		var angle = Random.Range(0, 2 * Mathf.PI);
		var orb = orbitsList[Random.Range(0, orbitsList.Count)];
		var x = Mathf.Cos(angle) * orb.transform.localScale.x * 3f / 2f;
		var y = Mathf.Sin(angle) * orb.transform.localScale.x * 3f / 2f;
		var newCAndyPosition = new Vector3(x, 0, y);

		var newCandyObject = Instantiate(candyPrefab, newCAndyPosition, Quaternion.identity, transform);
		newCandyObject.SetScale(orb.transform.localScale.x);
		newCandyObject.OnCandyCollected += CandyCollectedOption;
	}

	public void EnableStatus(bool status)
	{
		foreach (var sphere in spheresList)
		{
			sphere.EnabledRotation = status;
		}
	}

	public void DecreaseVelocity(bool value, float multiplier = 0)
	{
		spheresList.ForEach(x => x.DecreaseSpeed(value, multiplier));
	}
}
