using UnityEngine;

public class CandySphere : MonoBehaviour
{
	[SerializeField] private MeshRenderer meshRenderer;
	[SerializeField] private Material[] materials;
	[SerializeField] private Vector2 rotateSpeeds;
	[SerializeField] private float rotateSpeedMultiplier;
	[SerializeField] private new Rigidbody rigidbody;
	[SerializeField] private float defaultScale;
	public float DefaultScale => defaultScale;
	private float rotateSpeed;
	private Vector3 currentPos => transform.position;
	private Vector3 rotationAxis;
	public bool EnabledRotation { get; set; }

	private void Start()
	{
		rotateSpeed = Random.Range(rotateSpeeds.x, rotateSpeeds.y);

		rotationAxis = Random.Range(0, 2) == 0 ? Vector3.up : Vector3.down;
	}

	private void Update()
	{
		if (!EnabledRotation) return;

		transform.RotateAround(Vector3.zero, rotationAxis, rotateSpeed * Time.deltaTime * Mathf.Deg2Rad * rotateSpeedMultiplier);
	}

	public void SetMaterial()
	{
		meshRenderer.material = materials[Random.Range(0, materials.Length)];
	}

	private float constantSpeed;

	public void DecreaseSpeed(bool value, float multiplier = 0)
	{
		if (value)
		{
			constantSpeed = rotateSpeed;
			rotateSpeed *= multiplier;
		}
		else
		{
			rotateSpeed = constantSpeed;
		}
	}
}
