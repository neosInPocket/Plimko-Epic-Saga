using System;
using System.Collections;
using UnityEngine;

public class CandyCoin : MonoBehaviour
{
	[SerializeField] private GameObject glowObject;
	[SerializeField] private MeshRenderer meshRenderer;
	[SerializeField] private GameObject collectGlow;
	[SerializeField] private float rotateSpeed;
	private Vector3 currentEulerAngles;

	public Action<CandyCoin> OnCandyCollected { get; set; }
	private bool candyEnabled => meshRenderer.enabled;
	[SerializeField] public float defaultScale;

	private void Start()
	{
		currentEulerAngles = transform.eulerAngles;
	}

	private void Update()
	{
		currentEulerAngles.y += 20 * Time.deltaTime;
		transform.rotation = Quaternion.Euler(currentEulerAngles);
	}

	public void CollectCandy()
	{
		if (!candyEnabled) return;
		meshRenderer.enabled = false;
		OnCandyCollected?.Invoke(this);

		StartCoroutine(DestroyCandy());
	}

	public void SetScale(float value)
	{
		transform.localScale = Vector3.one * value * defaultScale;
	}

	private IEnumerator DestroyCandy()
	{
		collectGlow.SetActive(true);
		glowObject.SetActive(false);
		yield return new WaitForSeconds(1f);
		Destroy(gameObject);
	}
}
