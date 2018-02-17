using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private float dist;
	
	private float orbitSpeedX;
	private float orbitSpeedY;
	private float zoomSpeed;
	
	public float rotXSpeedModifier=0.25f;
	public float rotYSpeedModifier=0.25f;
	public float zoomSpeedModifier=5;
	
	public float minRotX=-20;
	public float maxRotX=70;
	
	void Start () {
		dist=transform.localPosition.z;
	}
	
	void OnEnable() {
		Gesture.onDraggingE += OnDragging;
		Gesture.onPinchE += OnPinch;
	}
	
	void OnDisable() {
		Gesture.onDraggingE -= OnDragging;
		Gesture.onPinchE -= OnPinch;
	}

	void Update () {
		//get the current rotation
		float x=transform.rotation.eulerAngles.x;
		float y=transform.rotation.eulerAngles.y;
		
		//make sure x is between -180 to 180 so we can clamp it propery later
		if(x>180) x-=360;
		
		//calculate the x and y rotation
		Quaternion rotationY=Quaternion.Euler(0, y, 0)*Quaternion.Euler(0, orbitSpeedY, 0);
		Quaternion rotationX=Quaternion.Euler(Mathf.Clamp(x+orbitSpeedX, minRotX, maxRotX), 0, 0);
		
		//apply the rotation
		transform.parent.rotation=rotationY*rotationX;
		
		//calculate the zoom and apply it
		dist+=Time.deltaTime*zoomSpeed*0.01f;
		dist=Mathf.Clamp(dist, -15, -3);
		transform.localPosition=new Vector3(0, 0, dist);
		
		//reduce all the speed
		orbitSpeedX*=(1-Time.deltaTime*12);
		orbitSpeedY*=(1-Time.deltaTime*3);
		zoomSpeed*=(1-Time.deltaTime*4);
		
		//use mouse scroll wheel to simulate pinch, sorry I sort of cheated here
		zoomSpeed+=Input.GetAxis("Mouse ScrollWheel")*500*zoomSpeedModifier;
	}
	
	//called when one finger drag are detected
	void OnDragging(DragInfo dragInfo) {
		//vertical movement is corresponded to rotation in x-axis
		orbitSpeedX=-dragInfo.delta.y*rotXSpeedModifier;
		//horizontal movement is corresponded to rotation in y-axis
		orbitSpeedY=dragInfo.delta.x*rotYSpeedModifier;
	}
	
	//called when pinch is detected
	void OnPinch(PinchInfo pinfo) {
		zoomSpeed-=pinfo.magnitude*zoomSpeedModifier;
	}
}
