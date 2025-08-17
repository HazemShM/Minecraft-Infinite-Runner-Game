using UnityEngine;

public class SpinCamera : MonoBehaviour
{
	private float speed = 8f;

	public void Update()
	{
		transform.localEulerAngles += new Vector3(0, speed * Time.deltaTime, 0);
	}
}