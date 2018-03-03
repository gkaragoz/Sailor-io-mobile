using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour {
	public float shipSpeed;
	public float shipRotationSpeed;
	public float wheelRotationSpeed;
	public float wheelRotationAmount;
	public GameObject wheel;
	public float cameraPanAmount;

	private float shipRotation = 0;
	private float wheelRotation = 0;

	private int turnRightHash = Animator.StringToHash("Turn Right");
	private int turnLeftHash = Animator.StringToHash("Turn Left");
	private int idleHash = Animator.StringToHash("Idle");

	public GameObject captain;
	private int steerRightHash = Animator.StringToHash("Steer Right");
	private int steerLeftHash = Animator.StringToHash("Steer Left");
	private int idleCaptainHash = Animator.StringToHash("Steer Idle");

	private float animationSpeed;

	void Start () {
		animationSpeed = captain.GetComponent<Animator> ().speed;
	}

	void Update () {
		//move ship forward
		transform.Translate (Vector3.forward * shipSpeed * Time.deltaTime);

		RotateWheel ();
	}

	private void RotateWheel(){

		//if turning left
		if ((Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) && wheelRotation > -wheelRotationAmount) {

			wheel.transform.Rotate (0, 0, wheelRotationSpeed * Time.deltaTime);
			wheelRotation -= wheelRotationSpeed * Time.deltaTime;
			shipRotation -= shipRotationSpeed * Time.deltaTime;

			GetComponent<Animator> ().SetTrigger (turnLeftHash);
			captain.GetComponent<Animator> ().SetTrigger (steerLeftHash);
			PlayCaptainAnimation (1);

			//if ship is at the rightmost point and returning to normal position, play animation at double speed.
			if (shipRotation > 0) {
				PlayCaptainAnimation (1);
			}

		//if turning right
		} else if ((Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) && wheelRotation < wheelRotationAmount) {
			
			wheel.transform.Rotate (0, 0, -wheelRotationSpeed * Time.deltaTime);
			wheelRotation += wheelRotationSpeed * Time.deltaTime;
			shipRotation += shipRotationSpeed * Time.deltaTime;

			GetComponent<Animator> ().SetTrigger (turnRightHash);
			captain.GetComponent<Animator> ().SetTrigger (steerRightHash);
			PlayCaptainAnimation (1);

			//if ship is at the rightmost point and returning to normal position, play animation at double speed.
			if (shipRotation < 0) {
				PlayCaptainAnimation (2);
			}

		//if keys are released, wheel return back to normal rotation
		} else if (!Input.GetKey (KeyCode.A) &&
			!Input.GetKey (KeyCode.D) &&
			!Input.GetKey (KeyCode.LeftArrow) &&
			!Input.GetKey (KeyCode.RightArrow)) {

			GetComponent<Animator> ().SetTrigger (idleHash);
			PlayCaptainAnimation (1);

			//when angle is close to zero, round it to zero
			if (Mathf.Abs (shipRotation) < shipRotationSpeed * Time.deltaTime) {
				shipRotation = 0;
				captain.GetComponent<Animator> ().SetTrigger (idleCaptainHash);

			}

			//wheel return back to normal rotation

			//if wheel was on the left
			if (shipRotation < 0) {
				wheelRotation += wheelRotationSpeed * Time.deltaTime;
				shipRotation += shipRotationSpeed * Time.deltaTime;
				wheel.transform.Rotate (0, 0, -wheelRotationSpeed * Time.deltaTime);
				captain.GetComponent<Animator> ().SetTrigger (steerRightHash);
				PlayCaptainAnimation (2);

				//if wheel was on the right
			} else if (shipRotation > 0) {
				wheelRotation -= wheelRotationSpeed * Time.deltaTime;
				shipRotation -= shipRotationSpeed * Time.deltaTime;
				wheel.transform.Rotate (0, 0, wheelRotationSpeed * Time.deltaTime);
				captain.GetComponent<Animator> ().SetTrigger (steerLeftHash);
				PlayCaptainAnimation (2);

			}

		} else {
			PauseAnimation ();
		}

		RotateShip ();
		PanCamera ();
	}

	private void RotateShip(){
		//rotate ship around y while turning
		transform.RotateAround (transform.position, Vector3.up, shipRotation * shipRotationSpeed * Time.deltaTime);
	}

	private void PanCamera(){
		//pan camera
		Camera.main.transform.localPosition = new Vector3 (shipRotation * cameraPanAmount, Camera.main.transform.localPosition.y, Camera.main.transform.localPosition.z);
	}

	private void PauseAnimation(){
		//Pause captain animation at the very end of wheel rotation.
		AnimatorClipInfo[] animatorClipInfos = captain.GetComponent<Animator> ().GetCurrentAnimatorClipInfo (0);
		captain.GetComponent<Animator> ().Play (animatorClipInfos [0].clip.name, 0, 0.5f);

		//make wheel rotation zero so that it aligns with the captain's hands.
		wheel.transform.localRotation = Quaternion.Euler (Vector3.zero);
	}

	private void PlayCaptainAnimation(float speedMultiplier = 1){
		captain.GetComponent<Animator> ().speed = animationSpeed * speedMultiplier;
	}
}
