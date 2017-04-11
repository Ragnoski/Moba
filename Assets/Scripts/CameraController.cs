using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform relativeTransform;
	public int boundary;
	public int speed;


	private int _screenwidth;
	private int _screenheight;


	private void Start () {
		_screenwidth = Screen.width;
		_screenheight = Screen.height;
	}

	private void Update () {
		if (Input.mousePosition.x > _screenwidth - boundary) {
			transform.Translate(Vector3.right * speed * Time.deltaTime, relativeTransform);
		}

		if (Input.mousePosition.x < 0 + boundary) {
			transform.Translate(-Vector3.right * speed * Time.deltaTime, relativeTransform);
		}

		if (Input.mousePosition.y > _screenheight + boundary) {
			transform.Translate(Vector3.forward * speed * Time.deltaTime, relativeTransform);
		}

		if (Input.mousePosition.y < 0 + boundary) {
			transform.Translate(-Vector3.forward * speed * Time.deltaTime, relativeTransform);
		}
	}


}
