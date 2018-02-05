using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Client;
using UnityEngine;
using Assets.Scripts.Network.ApiModels;
using Assets.Scripts.Network.Models;

public class WorldManager : MonoBehaviour
{

	public static WorldManager instance;

	[Header("Initialize")]
	public Transform mapObject;

	[Header("Good to know")]
	public Vector3 MAP_CENTER;
	public float MAP_LENGTH;
	public float MAP_WIDTH;
	public float MAP_HEIGHT;

	public List<SupplyEntity> supplyEntities = new List<SupplyEntity>();
	public List<ShipEntity> shipEntities = new List<ShipEntity>();

	private SupplyGenerator supplyGenerator;
	private ShipGenerator shipGenerator;

	private void Awake()
	{
		if (instance == null)
			instance = this;

		supplyGenerator = GetComponent<SupplyGenerator>();
		shipGenerator = GetComponent<ShipGenerator>();
	}

	public void BuildWorld(WorldInfoModel worldInfoModel)
	{
		Vector3 centerPos = new Vector3(worldInfoModel.offSetX, 0f, worldInfoModel.offSetZ);
		mapObject = Instantiate(mapObject, centerPos, Quaternion.identity);
		mapObject.transform.localScale = new Vector3(worldInfoModel.length * 0.1f, 1f, worldInfoModel.width * 0.1f);
		mapObject.name = "Sea";
	}

	public void InstantiateSupply(string supplyId, string assetName, Vector3 position)
	{
		if (supplyEntities.Count <= 0)
		{
			supplyEntities.Add(supplyGenerator.InstantiateNewSupply(supplyId, assetName, position));
			return;
		}

		//Nobody collected that supply yet.
		if (supplyEntities.Where(supply => supply.ID == supplyId).Any())
		{
			return;
		}

		supplyEntities.Add(supplyGenerator.InstantiateNewSupply(supplyId, assetName, position));
	}

	public void UpdateShips(ShipModel shipFromServer)
	{
		ShipEntity currentShip = shipEntities.Where(x => x.Id == shipFromServer.Id).SingleOrDefault();
		if (currentShip == null)
		{
			var shipPos = new Vector3(shipFromServer.pos_x, shipFromServer.pos_y, shipFromServer.pos_z);
			var newShip = this.shipGenerator.InstantiateNewShip(shipFromServer, shipPos);
			if (newShip != null)
			{
				shipEntities.Add(newShip);
			}
		}
		else
		{
			
			var shipObject = GameObject.Find(shipFromServer.Id).GetComponent<ShipEntity>();

			#region EntityInterpolation

			var clientInputTs = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
			var positionVector3 = new Vector3(shipFromServer.pos_x, shipFromServer.pos_y, shipFromServer.pos_z);
			if (shipFromServer.captainUserId == shipObject.CaptainUserId)
			{
				DebugManager.instance.usersShipVector.text = string.Format("Ship Server Pos: x:{0} y: {1} z: {2}", (float)shipFromServer.pos_x,
					(float)shipFromServer.pos_y, (float)shipFromServer.pos_z);
				DebugManager.instance.usersShipAngle.text = string.Format("Ship Server Angle: {0}", (float)shipFromServer.viewAngle);
			}

			shipObject.PositionEntries.Add(new
				PositionEntryModel
			{
				position = positionVector3,
				updateTime = clientInputTs
			});

			#endregion

			shipObject.MovementSpeed = shipFromServer.movementSpeed;
			shipObject.CurrentHealth = shipFromServer.currentHealth;
		}
	}

	//make tranformation
	public Vector3 GetMapCenter()
	{
		if (mapObject == null)
		{
			Debug.LogError("Assign a map!");
			return Vector3.zero;
		}
		MAP_CENTER = new Vector3(mapObject.position.x, mapObject.position.y, mapObject.position.z);
		return MAP_CENTER;
	}

	public float GetMapHeight()
	{
		if (mapObject == null)
		{
			Debug.LogError("Assingn a map!");
			return 100f;
		}
		float centerOffset = GetMapCenter().y;
		MAP_HEIGHT = centerOffset + (mapObject.GetComponent<MeshRenderer>().bounds.size.y * 0.5f);
		return MAP_HEIGHT;
	}

	public float GetMapLength()
	{
		if (mapObject == null)
		{
			Debug.LogError("Assign a map!");
			return 100f;
		}
		float centerOffset = GetMapCenter().x;
		MAP_LENGTH = centerOffset + (mapObject.GetComponent<MeshRenderer>().bounds.size.x * 0.5f);
		return MAP_LENGTH;
	}

	public float GetMapWidth()
	{
		if (mapObject == null)
		{
			Debug.LogError("Assign a map!");
			return 100f;
		}
		float centerOffset = GetMapCenter().z;
		MAP_WIDTH = centerOffset + (mapObject.GetComponent<MeshRenderer>().bounds.size.z * 0.5f);
		return MAP_WIDTH;
	}

}
