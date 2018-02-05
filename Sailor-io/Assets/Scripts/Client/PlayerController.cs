using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Network;
using Assets.Scripts.Network.Controllers;
using UnityEngine;

[System.Serializable]
public class PlayerController : PlayerEntity {

    public enum Direction {
        Right,
        Left,
        Forward,
        Back
    }

    public Transform FL_C;
    public Transform FR_C;
    public Transform BR_C;
    public Transform BL_C;

    private void Awake() {
        Ship = transform.root.GetComponent<ShipController>();
    }

    private void FixedUpdate() {

        if (Input.GetKey(KeyCode.W) && IsWalkAvailable(Direction.Forward)) {
            transform.position += (transform.forward * MovementSpeed * Time.deltaTime);

            if (GameManager.instance.offlineMode == false)
			    NetInputController.SendPlayerMovementInput(transform.forward, Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) && IsWalkAvailable(Direction.Back)) {
            transform.position += (-transform.forward * MovementSpeed * Time.deltaTime);
            
            if (GameManager.instance.offlineMode == false)
                NetInputController.SendPlayerMovementInput(-transform.forward, Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.A) && IsWalkAvailable(Direction.Left)) {
            transform.position += (-transform.right * MovementSpeed * Time.deltaTime);

            if (GameManager.instance.offlineMode == false)
                NetInputController.SendPlayerMovementInput(-transform.right, Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.D) && IsWalkAvailable(Direction.Right)) {
            transform.position += (transform.right * MovementSpeed * Time.deltaTime);

            if (GameManager.instance.offlineMode == false)
                NetInputController.SendPlayerMovementInput(transform.right, Time.deltaTime);
        }

        transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
    }

    bool IsWalkAvailable(Direction direction) {
        Vector3 tempPos = transform.localPosition;

        switch (direction) {
            case Direction.Right:
                tempPos += (transform.right * (MovementSpeed + 1f) * Time.deltaTime);
            break;
            case Direction.Left:
                tempPos += (-transform.right * (MovementSpeed + 1f) * Time.deltaTime);
            break;
            case Direction.Forward:
                tempPos += (transform.forward * (MovementSpeed + 1f) * Time.deltaTime);
            break;
            case Direction.Back:
                tempPos += (-transform.forward * (MovementSpeed + 1f) * Time.deltaTime);
            break;
        }

        //Debug.Log("1:" + (tempPos.z < FL_C.localPosition.z));
        //Debug.Log("2:" + (tempPos.z < FR_C.localPosition.z));
        //Debug.Log("3:" + (tempPos.x < FR_C.localPosition.x));
        //Debug.Log("4:" + (tempPos.x > FL_C.localPosition.x));
        //Debug.Log("5:" + (tempPos.z > BL_C.localPosition.z));
        //Debug.Log("6:" + (tempPos.z > BR_C.localPosition.z));
        //Debug.Log("7:" + (tempPos.x < BR_C.localPosition.x));
        //Debug.Log("8:" + (tempPos.x > BL_C.localPosition.x));

        if (tempPos.z < FL_C.localPosition.z && 
            tempPos.z < FR_C.localPosition.z && 
            tempPos.x < FR_C.localPosition.x && 
            tempPos.x > FL_C.localPosition.x && 
            tempPos.z > BL_C.localPosition.z &&
            tempPos.z > BR_C.localPosition.z &&
            tempPos.x < BR_C.localPosition.x &&
            tempPos.x > BL_C.localPosition.x
            )
            return true;
        else
            return false;
    }

    void OnTriggerEnter(Collider other) {
        if (other.name == "R_Rudder") {
            Ship.GetComponent<ShipController>().rotatingRight = true;
            Ship.GetComponent<ShipController>().rotatingLeft = false;

        } else if (other.name == "L_Rudder") {
            Ship.GetComponent<ShipController>().rotatingRight = false;
            Ship.GetComponent<ShipController>().rotatingLeft = true;
        }
    }

    void OnTriggerExit(Collider other) {
        Ship.GetComponent<ShipController>().rotatingRight = false;
        Ship.GetComponent<ShipController>().rotatingLeft = false;
    }

    //private void Fire() {
    //    if (Input.GetKeyDown(KeyCode.Tab)) {
    //cannonbollPath = new Vector3[20];
    //Vector3 vEnd = cannonboll.transform.GetChild(0).position;
    //Vector3 vStart = cannonboll.transform.position;

    //float dX = vEnd.x - vStart.x;
    //float dY = vEnd.y - vStart.y;
    //float dZ = vEnd.z - vStart.z;

    //float vX = dX * 300 * 0.02f * 2;
    //float vY = dY * 300 / 50 * 2;
    //float vZ = dZ * 300 * 0.02f * 2;

    //for (int ii = 0; ii < 20; ii++) {
    //    float posX = vX * ii * timeStep + Physics.gravity.x;
    //    float posY = vY * ii * timeStep + 0.5f * Physics.gravity.y * Mathf.Pow(ii * timeStep, 2);
    //    float posZ = vZ * ii * timeStep + Physics.gravity.z;
    //    cannonbollPath[ii] = new Vector3(posX, posY, posZ) + vEnd;

    //    this.GetComponent<LineRenderer>().SetPosition(ii, new Vector3(posX, posY, posZ) + vEnd);
    //}

    //// GameObject newCannonBall = (GameObject)Instantiate(Resources.Load("CannonBalls/CannonBall"), cannonbollPath[0], Quaternion.identity);
    //// LeanTween.move(newCannonBall, cannonbollPath, 10f).setEaseInOutQuad();
    //// newCannonBall.transform.LookAt((vEnd - vStart).normalized + newCannonBall.transform.position);
    //// newCannonBall.GetComponent<Rigidbody>().AddForce((vEnd - vStart) * 600);
    //ISFSObject param = new SFSObject();
    //param.PutFloat("posX", cannonbollPath[0].x); param.PutFloat("dirX", (vEnd - vStart).x);
    //param.PutFloat("posY", cannonbollPath[0].y); param.PutFloat("dirY", (vEnd - vStart).y);
    //param.PutFloat("posZ", cannonbollPath[0].z); param.PutFloat("dirZ", (vEnd - vStart).z);

    //ServerController.sfs.Send(new ExtensionRequest(CMD.fire, param, ServerController.sfs.LastJoinedRoom));
    //}
    //}

}
