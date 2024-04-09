using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class EducationSystem : MonoBehaviour
{
	[SerializeField] private string[] characterTextes;
	[SerializeField] private TMP_Text spaceText;
	[SerializeField] private GameObject arrowPointer;
	[SerializeField] private OrbitsSpawner orbitsSpawner;
	[SerializeField] private float pointerAmp;
	[SerializeField] private float pointeFreq;
	[SerializeField] private Transform player;
	[SerializeField] private Transform candyAtStart;
	public Action EducationEnded;
	private Action currentSection;

	public void EducateSystem()
	{
		gameObject.SetActive(true);
		Touch.onFingerDown += SkipPhrase;
		currentSection = WelcomerPhrase;
		SkipPhrase(null);
	}

	private void SkipPhrase(Finger finger)
	{
		currentSection();
	}

	private void WelcomerPhrase()
	{
		spaceText.text = characterTextes[0];
		currentSection = BallPointer;
	}

	private void BallPointer()
	{
		arrowPointer.gameObject.SetActive(true);

		spaceText.text = characterTextes[1];
		currentSection = SpherePointer;
		StartCoroutine(SetPointerActive(player.transform.position, false));
	}

	private void SpherePointer()
	{
		StopAllCoroutines();
		spaceText.text = characterTextes[2];
		currentSection = CandyPointer;

		var position = new Vector3(-orbitsSpawner.orbitsList[0].Radius, 0, 0);
		StartCoroutine(SetPointerActive(position, false));
	}

	private void CandyPointer()
	{
		StopAllCoroutines();
		arrowPointer.gameObject.SetActive(true);
		spaceText.text = characterTextes[3];
		currentSection = ProgressPointer;

		var position = candyAtStart.transform.position;
		StartCoroutine(SetPointerActive(position, false));
	}

	private void ProgressPointer()
	{
		StopAllCoroutines();
		arrowPointer.gameObject.SetActive(false);

		spaceText.text = characterTextes[4];
		currentSection = PopupPointers;
	}

	private void PopupPointers()
	{
		StopAllCoroutines();
		spaceText.text = characterTextes[5];
		currentSection = EndPointer;
	}

	private void EndPointer()
	{
		StopAllCoroutines();

		spaceText.text = characterTextes[6];
		currentSection = ReturnPositive;
		arrowPointer.gameObject.SetActive(false);
	}

	private void ReturnPositive()
	{
		Touch.onFingerDown -= SkipPhrase;
		EducationEnded();
		gameObject.SetActive(false);
	}

	private IEnumerator SetPointerActive(Vector3 position, bool flipY)
	{
		arrowPointer.transform.position = position;
		if (flipY)
		{
			var scale = arrowPointer.transform.localScale;
			scale.y = -1;
			arrowPointer.transform.localScale = scale;
		}

		float yPosition = position.y;
		float currentTime = 0;
		Vector3 currentPosition = position;
		float currentAdd;

		while (true)
		{
			currentAdd = pointerAmp * Mathf.Sin(currentTime * pointeFreq) - pointerAmp;
			currentPosition.y = position.y + currentAdd;
			arrowPointer.transform.position = currentPosition;
			currentTime += Time.deltaTime;
			yield return null;
		}
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= SkipPhrase;
	}
}
