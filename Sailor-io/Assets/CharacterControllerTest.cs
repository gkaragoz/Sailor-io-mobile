using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerTest : MonoBehaviour {
	public float runSpeed;
	public float turnSpeed;
	private float lastFiredAt = 0;
	private int runHash = Animator.StringToHash("Run");
	private int idleHash = Animator.StringToHash("Idle");
	private int idleCaptainHash = Animator.StringToHash("Steer Idle");

	private bool running, idle;
	public GameObject pirate;
	public GameObject ship;
	private bool steering;

	// Use this for initialization
	void Start () {
		idle = true;
		running = false;
		steering = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!steering) {
			if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.S)) {
				running = true;
				idle = false;
				pirate.GetComponent<Animator> ().SetTrigger (runHash);
				transform.Translate (Vector3.forward * runSpeed * Input.GetAxis ("Vertical") * Time.deltaTime);
				transform.RotateAround (transform.position, transform.up, turnSpeed * Input.GetAxis ("Horizontal") * Time.deltaTime);
			} else {
				running = false;
				idle = true;
				pirate.GetComponent<Animator> ().SetTrigger (idleHash);
			}
		}

		if (Input.GetKeyDown (KeyCode.E)) {
			if (!steering) {
				//if pirate was not steering before and just started steering now
				ship.GetComponent<ShipMovementTest> ().captainAtWheel = true;
				transform.position = ship.GetComponent<ShipMovementTest> ().captainPosition.position;
				transform.rotation = ship.GetComponent<ShipMovementTest> ().captainPosition.rotation;
				pirate.GetComponent<Animator> ().SetTrigger (idleCaptainHash);
				steering = true;
				idle = false;
			} else {
				ship.GetComponent<ShipMovementTest> ().captainAtWheel = false;
				pirate.GetComponent<Animator> ().SetTrigger (idleHash);
				steering = false;
				idle = true;
			}
		}
	}
}
