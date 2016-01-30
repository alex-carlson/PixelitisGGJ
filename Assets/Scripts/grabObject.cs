﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class grabObject : MonoBehaviour {

	public float speedH = 2.0f;
	public float speedV = 2.0f;

	private float yaw = 0.0f;
	private float pitch = 0.0f;

	public float force = 10;

	GameObject heldObject = null;
	public float arcHeight = 10;

	bool isHolding = false;
	Transform camFwd;
	public Sprite activeCursor;
	public Sprite inactiveCursor;
	GameObject cursor;

	void Start() {
		Cursor.lockState = CursorLockMode.Locked;
		cursor = GameObject.Find ("Cursor");
	}
	
	// Update is called once per frame
	void Update () {
		camFwd = Camera.main.transform;
		Vector3 fwd = transform.TransformDirection(Vector3.forward);

		RaycastHit hit;

		yaw += speedH * Input.GetAxis("Mouse X");
		pitch -= speedV * Input.GetAxis("Mouse Y");

		transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

		if (heldObject != null && isHolding == true) {
			heldObject.transform.position = Camera.main.transform.position + (camFwd.forward * 4);
			heldObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			heldObject.transform.rotation = Quaternion.identity;
		}

		if (Physics.Raycast(transform.position, fwd, out hit)) {
			if (hit.transform.tag == "Pickup") {

				cursor.GetComponent<Image> ().sprite = activeCursor;

				// clicked on a pickupable thing
				if (Input.GetButtonDown ("Fire1")) {
					heldObject = hit.transform.gameObject;

					if (isHolding == false) {
						isHolding = true;
						heldObject.GetComponent<AudioSource> ().Play ();
					} else {
						heldObject.GetComponent<Rigidbody> ().AddForce ((camFwd.forward * force) * 60);
						isHolding = false;
					}
				}
			} else {
				cursor.GetComponent<Image> ().sprite = inactiveCursor;
			}

		}
	}
}
