using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShipController : ShipEntity
{

	public bool rotatingRight, rotatingLeft;

	private float _rotationY, _rotationZ;

	void FixedUpdate()
	{
        IsSupplyInRange();

		if (GameManager.instance.offlineMode)
		{
			transform.Translate(Vector3.forward * MovementSpeed * Time.deltaTime);

			if (rotatingRight)
			{
				_rotationY += Time.deltaTime * RotationSpeed;
				if (_rotationZ > -15f)
					_rotationZ -= Time.deltaTime * SlopeSpeed;
				transform.rotation = Quaternion.Euler(0, _rotationY, _rotationZ);

			}
			else if (rotatingLeft)
			{
				_rotationY -= Time.deltaTime * RotationSpeed;
				if (_rotationZ < 15f)
					_rotationZ += Time.deltaTime * SlopeSpeed;
				transform.rotation = Quaternion.Euler(0, _rotationY, _rotationZ);
			}
			else
			{
				if (_rotationZ > 0)
				{
					_rotationZ -= Time.deltaTime * SlopeSpeed;
				}
				else if (_rotationZ < 0)
				{
					_rotationZ += Time.deltaTime * SlopeSpeed;
				}
				transform.rotation = Quaternion.Euler(0, _rotationY, _rotationZ);
			}
		}
	}

    bool IsSupplyInRange() {
        float distance = 0f;

        foreach (SupplyEntity supply in WorldManager.instance.supplyEntities) {
            Vector2 shipPosition = new Vector2(transform.position.x, transform.position.z);
            Vector2 supplyPosition = new Vector2(supply.transform.position.x, supply.transform.position.z);

            distance = Vector2.Distance(shipPosition, supplyPosition);

            if (distance <= SupplyCollectorRange) {
                Debug.Log("Request collect that supply to server: " + supply.name);
                //Send information to server.
                //When server approved that request somewhere in listener function, 
                //use that method to destroy this supply from client's scene.
                //Probably this method will be in supplyEntities list in WorldManager for every client.

                //OnSupplyCollected();
                //Give some golds to players in ship.
                foreach (PlayerEntity playerEntity in PlayerEntities) {
                    playerEntity.Gold += supply.Income;
                }

                //Now destroy it. 
                //DONT FORGET TO DO THIS FOR EVERY PLAYER'S WORLDMANAGER.
                WorldManager.instance.supplyEntities.Remove(supply);
                Destroy(supply.gameObject);

                return true;
            }
        }
        return false;
    }

}
