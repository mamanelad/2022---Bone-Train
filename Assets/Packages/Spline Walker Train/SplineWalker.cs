using UnityEngine;

public class SplineWalker : MonoBehaviour {

	public BezierSpline spline;

	public float speed;

	public float speedSensitivity;

	public bool lookForward;

	public SplineWalkerMode mode;

	public float SplineLenght;

	public float Progress { get; set; }
	
	private bool goingForward = true;

	private void Start()
	{
		SplineLenght = spline.GetLength();
	}
	
	private void Update ()
	{
		ChangeSpeed();
		SplineLenght = spline.GetLength();
		if (goingForward) {
			Progress += (Time.deltaTime / SplineLenght * speed);
			if (Progress > 1f) {
				if (mode == SplineWalkerMode.Once) {
					Progress = 1f;
				}
				else if (mode == SplineWalkerMode.Loop) {
					Progress -= 1f;
				}
				else {
					Progress = 2f - Progress;
					goingForward = false;
				}
			}
		}
		else
		{
			Progress -= Time.deltaTime / SplineLenght * speed;
			if (Progress < 0f) {
				Progress = - Progress;
				goingForward = true;
			}
		}
		
		Vector3 position = spline.GetPoint(Progress);
		transform.localPosition = position;
		if (lookForward) {
			transform.LookAt(position + spline.GetDirection(Progress));
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