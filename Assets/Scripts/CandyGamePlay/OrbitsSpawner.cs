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
	private CandyCoin currentCandy;
	private List<OrbitRenderer> orbits;
	private List<CandySphere> spheres;

	public void SetData()
	{
		currentCandy = startCandy;
		orbits = new List<OrbitRenderer>();
		spheres = new List<CandySphere>();

		var screenSize = ScreenOrtho.ScreenOrthoSize();
		float maxOrbitScale = 2 * screenSize.x * maxWidthViaScreenSize / defaultWidth;
		float minOrbitScale = 2 * screenSize.x * minWidthViaScreenSize / defaultWidth;
		float deltaScale = (maxOrbitScale - minOrbitScale) / (spawnCount - 1);
		float currentScale = maxOrbitScale;

		startCandy.transform.position = new Vector3(3f / 2f * currentScale - tubeRadius, 0, 0);
		startCandy.OnCandyCollected += OnCandyCollected;
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
			orbits.Add(orbit);
			spheres.Add(sphere);

			currentScale -= deltaScale;
		}
	}

	private void OnCandyCollected(CandyCoin candyCoin)
	{
		candyCoin.OnCandyCollected -= OnCandyCollected;
		var randomAngle = Random.Range(0, 2 * Mathf.PI);
		var randomOrbit = orbits[Random.Range(0, orbits.Count)];
		var xPosition = Mathf.Cos(randomAngle) * randomOrbit.transform.localScale.x * 3f / 2f;
		var yPosition = Mathf.Sin(randomAngle) * randomOrbit.transform.localScale.x * 3f / 2f;
		var newCAndyPosition = new Vector3(xPosition, 0, yPosition);

		var newCandy = Instantiate(candyPrefab, newCAndyPosition, Quaternion.identity, transform);
		newCandy.SetScale(randomOrbit.transform.localScale.x);
		newCandy.OnCandyCollected += OnCandyCollected;
	}

	public void Enable(bool enabled)
	{
		foreach (var sphere in spheres)
		{
			sphere.EnabledRotation = enabled;
		}
	}

	public void DecreaseAllSpeed(bool value, float multiplier = 0)
	{
		spheres.ForEach(x => x.DecreaseSpeed(value, multiplier));
	}
}
