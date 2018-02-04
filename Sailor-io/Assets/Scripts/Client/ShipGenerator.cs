using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Network.Models;
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

		public ShipEntity InstantiateNewShip(ShipModel shipFromServer, Vector3 position)
		{
			ShipEntity prefab = GetPrefabFromAssetName(shipFromServer.assetName);

			if (prefab == null)
			{
				Debug.LogError("!Assign a SHIP!");
				return null;
			}

			ShipEntity entity = Instantiate(prefab.gameObject, position, Quaternion.identity, parentObject).GetComponent<ShipEntity>();

			entity.Id = shipFromServer.Id;
			entity.name = shipFromServer.Id;
			entity.MaxSailorsCount = shipFromServer.maxSailorsCount;
			entity.CurrentSailorsCount = shipFromServer.currentSailorsCount;
			entity.MaxSuppliesCount = shipFromServer.maxSuppliesCount;
			entity.CurrentSuppliesCount = shipFromServer.currentSuppliesCount;

			entity.MovementSpeed = shipFromServer.movementSpeed;
			entity.SlopeSpeed = shipFromServer.slopeSpeed;
			entity.RotationSpeed = shipFromServer.rotationSpeed;

			entity.Health = shipFromServer.maxHealth;
			entity.CurrentHealth = shipFromServer.currentHealth;

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
