using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class EducationSystem : MonoBehaviour
{
	[SerializeField] private string[] characterTextes;
	[SerializeField] private TMP_Text characterCaptionText;
	[SerializeField] private GameObject arrowPointer;
	[SerializeField] private OrbitsSpawner orbitsSpawner;
	[SerializeField] private float pointerAmp;
	[SerializeField] private float pointeFreq;
	[SerializeField] private Transform ball;
	[SerializeField] private Transform startCoin;
	[SerializeField] private Transform popupsPositions;
	private Action EducationEnded;
	private Action currentPhrase;

	public void Educate(Action educationEndAction)
	{
		EducationEnded = educationEndAction;
		gameObject.SetActive(true);
		Touch.onFingerDown += SkipPhrase;
		currentPhrase = StartPhrase;
		SkipPhrase(null);
	}

	private void SkipPhrase(Finger finger)
	{
		currentPhrase();
	}

	private void StartPhrase()
	{
		characterCaptionText.text = characterTextes[0]; // welcomer
		currentPhrase = ArrowPointer1;
	}

	private void ArrowPointer1()
	{
		arrowPointer.gameObject.SetActive(true);

		characterCaptionText.text = characterTextes[1]; // ball pointer
		currentPhrase = ArrowPointer2;
		StartCoroutine(Pointer(ball.transform.position, false));
	}

	private void ArrowPointer2()
	{
		StopAllCoroutines();
		characterCaptionText.text = characterTextes[2]; // sphere pointer
		currentPhrase = ArrowPointer3;

		var position = new Vector3(-orbitsSpawner.orbits[0].Radius, 0, 0);
		StartCoroutine(Pointer(position, false));
	}

	private void ArrowPointer3()
	{
		StopAllCoroutines();
		arrowPointer.gameObject.SetActive(true);
		characterCaptionText.text = characterTextes[3]; // coin pointer
		currentPhrase = ArrowPointer4;

		var position = startCoin.transform.position;
		StartCoroutine(Pointer(position, false));
	}

	private void ArrowPointer4()
	{
		StopAllCoroutines();
		arrowPointer.gameObject.SetActive(false);

		characterCaptionText.text = characterTextes[4]; // progress pointer
		currentPhrase = ArrowPointer5;
	}

	private void ArrowPointer5()
	{
		StopAllCoroutines();
		arrowPointer.gameObject.SetActive(true);
		characterCaptionText.text = characterTextes[5]; // popup pointers
		currentPhrase = EndPointer;

		var position = popupsPositions.transform.position;
		StartCoroutine(Pointer(position, true));
	}

	private void EndPointer()
	{
		StopAllCoroutines();

		characterCaptionText.text = characterTextes[6];
		currentPhrase = ReturnPositive;
		arrowPointer.gameObject.SetActive(false);
	}

	private void ReturnPositive()
	{
		Touch.onFingerDown += SkipPhrase;
		EducationEnded();
		gameObject.SetActive(false);
	}

	private IEnumerator Pointer(Vector3 position, bool flipY)
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
