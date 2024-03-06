using UnityEngine;

public class OrbitRenderer : MonoBehaviour
{
	[SerializeField] private float tubeRadius;
	public float Radius => transform.localScale.x * 3f / 2f - tubeRadius;
}
