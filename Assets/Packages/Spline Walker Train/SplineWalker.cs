using UnityEngine;

public class SplineWalker : MonoBehaviour {

	public BezierSpline spline;

	public float speed;

	public float speedSensitivity;

	public bool lookForward;

	public SplineWalkerMode mode;

	private float splineLenght;

	private float progress;
	
	private bool goingForward = true;

	private void Start()
	{
		splineLenght = spline.GetLength();
	}
	
	private void Update () {
		ChangeSpeed();
		if (goingForward) {
			progress += (Time.deltaTime / splineLenght * speed);
			if (progress > 1f) {
				if (mode == SplineWalkerMode.Once) {
					progress = 1f;
				}
				else if (mode == SplineWalkerMode.Loop) {
					progress -= 1f;
				}
				else {
					progress = 2f - progress;
					goingForward = false;
				}
			}
		}
		else
		{
			progress -= Time.deltaTime / splineLenght * speed;
			if (progress < 0f) {
				progress = - progress;
				goingForward = true;
			}
		}
		
		Vector3 position = spline.GetPoint(progress);
		transform.localPosition = position;
		if (lookForward) {
			transform.LookAt(position + spline.GetDirection(progress));
		}
	}

	private void ChangeSpeed()
	{
		if (Input.GetKey(KeyCode.UpArrow))
			speed += Time.deltaTime * speedSensitivity;
		if (Input.GetKey(KeyCode.DownArrow))
			speed -= Time.deltaTime * speedSensitivity;
	}
}