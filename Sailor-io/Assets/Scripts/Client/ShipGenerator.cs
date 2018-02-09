using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Network;
using Assets.Scripts.Network.Models;
using SailorIO.Models;
using UnityEngine;

namespace Assets.Scripts.Client
{
	public class ShipGenerator : MonoBehaviour
	{
		public GameObject[] shipPrefabs;
		private Transform parentObject;

		private void Awake()
		{
			if (GameObject.Find("_Ships") == null)
			{
				parentObject = new GameObject("_Ships").transform;
			}
			else
			{
				parentObject = GameObject.Find("_Ships").transform;
			}
		}

		public ShipEntity InstantiateNewShip(Ship shipFromServer, Vector3 position)
		{
			ShipEntity prefab = GetPrefabFromAssetName(shipFromServer.AssetType.ToString());

			if (prefab == null)
			{
				Debug.LogError("!Assign a SHIP!");
				return null;
			}

			ShipEntity entity = Instantiate(prefab.gameObject, position, Quaternion.identity, parentObject).GetComponent<ShipEntity>();


			entity.Id = "ship:" + shipFromServer.Id;
			entity.name = "ship:"+shipFromServer.CaptainUserId;
			entity.Name = "user:" + shipFromServer.CaptainUserId + "'s Ship";
			//entity.MaxSailorsCount = shipFromServer.maxSailorsCount;
			//entity.MaxSuppliesCount = shipFromServer.maxSuppliesCount;
			entity.CurrentSailorsCount = shipFromServer.CurrentSailorsCount;
			entity.CurrentSuppliesCount = shipFromServer.CurrentSuppliesCount;
			entity.CaptainUserId = shipFromServer.CaptainUserId;
			entity.MovementSpeed = shipFromServer.MovementSpeed;
			entity.SlopeSpeed = shipFromServer.SlopeSpeed;
			entity.RotationSpeed = shipFromServer.RotationSpeed;
			entity.CurrentHealth = shipFromServer.CurrentHealth;

			if(SocketManager.userSlotId == entity.CaptainUserId)
			{
				entity.isMe = true;
			}
			else
			{
				entity.isMe = false;
			}

			return entity;
		}

		private ShipEntity GetPrefabFromAssetName(string assetName)
		{
			foreach (GameObject shipPrefab in shipPrefabs)
			{
				if (shipPrefab.name == assetName)
				{
					return shipPrefab.GetComponent<ShipEntity>();
				}
			}
			Debug.LogError("Return default ship!");
			return shipPrefabs[0].GetComponent<ShipEntity>();
		}
	}
}
