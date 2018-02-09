using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Client;
using UnityEngine;
using Assets.Scripts.Network.ApiModels;
using Assets.Scripts.Network.Models;
using SailorIO.Models;

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

	public void BuildWorld(UpdateModel data)
	{
		Vector3 centerPos = new Vector3(data.WorldInfo.Value.OffSetX, 0f, data.WorldInfo.Value.OffSetZ);
		mapObject = Instantiate(mapObject, centerPos, Quaternion.identity);
		mapObject.transform.localScale = new Vector3(data.WorldInfo.Value.Length * 0.1f, 1f, data.WorldInfo.Value.Width * 0.1f);
		mapObject.name = "Sea";
	}

	public void BuildSupplies(UpdateModel data)
	{

		for (int i = 0; i < data.SupplyCratesLength; i++)
		{
			var supply = data.SupplyCrates(i).Value;
			var supplyPosition = new Vector3(
				supply.Pos.Value.X,
				supply.Pos.Value.Y,
				supply.Pos.Value.Z);
			var assetName = supply.AssetId.ToString();


			var supplyId = supplyPosition.x.ToString() + supplyPosition.y.ToString() + supplyPosition.z.ToString();
			InstantiateSupply(supplyId, assetName, supplyPosition);
		}
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

	public void UpdateShips(Ship shipFromServer)
	{
		ShipEntity currentShip = shipEntities.Where(x => x.Id == "ship:"+shipFromServer.Id).SingleOrDefault();
		if (currentShip == null)
		{
			var shipPos = new Vector3(shipFromServer.Pos.Value.X, shipFromServer.Pos.Value.Y, shipFromServer.Pos.Value.Z);
			
			var newShip = this.shipGenerator.InstantiateNewShip(shipFromServer, shipPos);
			if (newShip != null)
			{
				shipEntities.Add(newShip);
			}
		}
		else
		{
			
			var shipObject = GameObject.Find("ship:"+shipFromServer.Id).GetComponent<ShipEntity>();

			#region EntityInterpolation

			var clientInputTs = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
			var positionVector3 = new Vector3(shipFromServer.Pos.Value.X,
												shipFromServer.Pos.Value.Y,
												shipFromServer.Pos.Value.Z);
			if (shipFromServer.CaptainUserId == shipObject.CaptainUserId)
			{
				DebugManager.instance.usersShipVector.text = string.Format("Ship Server Pos: x:{0} y: {1} z: {2}", positionVector3.x,
					positionVector3.y, positionVector3.z);
				DebugManager.instance.usersShipAngle.text = string.Format("Ship Server Angle: {0}", (float)shipFromServer.ViewAngle);
			}

			shipObject.PositionEntries.Add(new
				PositionEntryModel
			{
				position = positionVector3,
				updateTime = clientInputTs
			});

			#endregion

			shipObject.MovementSpeed = shipFromServer.MovementSpeed;
			shipObject.CurrentHealth = shipFromServer.CurrentHealth;
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
