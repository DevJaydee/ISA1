using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSCam : MonoBehaviour
{
	#region Variables
	[Header("Camera Movement")]
	[SerializeField] private bool canMove = true;
	[SerializeField] private float panSpeed = 20f;
	[SerializeField] private float panBorderThickness = 10f;
	[SerializeField] private float panSpeedMultiplier = 2f;
	[SerializeField] private float scrollSpeed = 100f;
	[SerializeField] private Vector2 panLimit = Vector2.zero;
	[SerializeField] private Vector2 zoomLimit = Vector2.zero;

	[Header("Camera Rotation")]
	[SerializeField] private bool canRotate = true;
	[SerializeField] Camera cam = null;
	[SerializeField] private float cameraSensitivity = 90;
	[SerializeField] private float rotationX = 0.0f;
	[SerializeField] private float rotationY = 0.0f;
	#endregion

	#region Monobehaviour Callbacks
	private void Start()
	{
		cam = Camera.main;
	}

	private void Update()
	{
		if(canMove) CameraMovement();
		if(canRotate) RotateCamera();
	}
	#endregion

	#region Private Voids
	/// <summary>
	/// Moves the camera dependiong input. Does not rotate cam.
	/// </summary>
	private void CameraMovement()
	{
		Vector3 pos = transform.position;

		float finalPanSpeed = Input.GetKey(KeyCode.LeftShift) ? panSpeed * panSpeedMultiplier : panSpeed;

		// Forwards
		if(Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panBorderThickness)
			pos.z -= finalPanSpeed * Time.deltaTime;

		// Backwards
		if(Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorderThickness)
			pos.z += finalPanSpeed * Time.deltaTime;

		// Left
		if(Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBorderThickness)
			pos.x += finalPanSpeed * Time.deltaTime;

		// Right
		if(Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorderThickness)
			pos.x -= finalPanSpeed * Time.deltaTime;

		// Up and Down
		float scroll = Input.GetAxis("Mouse ScrollWheel");
		pos.y -= scroll * scrollSpeed * Time.deltaTime;

		pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);    // Left, Right
		pos.y = Mathf.Clamp(pos.y, zoomLimit.x, zoomLimit.y);  // Up, Down
		pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);    // Forward, Backward

		transform.position = pos;
	}

	private void RotateCamera()
	{
		if(Input.GetMouseButton(1))
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;

			rotationX += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
			rotationY += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
			rotationY = Mathf.Clamp(rotationY, -90, 90);

			cam.transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
			cam.transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);
		}
		else
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}
	#endregion
}
