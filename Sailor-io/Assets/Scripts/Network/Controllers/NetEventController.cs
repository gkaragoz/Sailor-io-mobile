using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Network.ApiModels;
using Assets.Scripts.Network.Models;
using Assets.Scripts.Network.Utils;
using FlatBuffers;
using SailorIO.ClientInputModel;
using SailorIO.Models;
using SocketIO;
using UnityEngine;
using EventTypes = SailorIO.Models.EventTypes;

namespace Assets.Scripts.Network.Controllers
{
	public static class NetEventController
	{

		/// <summary>
		/// Register All SocketIO Events
		/// </summary>
		public static void RegisterEvents()
		{
			SocketManager.instance.io.On(BaseEvent.connect.ToString(), OnConnectionSuccess);
			SocketManager.instance.io.On(BaseEvent.disconnect.ToString(), OnServerDisconnected);
			SocketManager.instance.io.On(EventTypes.WorldInfoUpdate.ToString(), OnWorldInfoDownloaded);
		}

		/// <summary>
		/// ConnectionSuccess function is callback when fired after connected successfully to socket
		/// </summary>
		/// <param name="e"></param>
		public static void OnConnectionSuccess(SocketIOEvent e)
		{
			if (SocketManager.instance.io.runInLocalhost)
				Debug.Log("[CONNECTED] to: " + SocketManager.instance.io.localhostURL);
			else
				Debug.Log("[CONNECTED] to: " + SocketManager.instance.io.serverURL);

			NetInputController.SendGetWorldInfo();
		}
		/// <summary>
		/// ConnectionSuccess function is callback when fired after connected successfully to socket
		/// </summary>
		/// <param name="e"></param>
		public static void OnRemovePlayer(SocketIOEvent e)
		{
			Debug.Log("[PLAYER DISCONNECTED]");

			var data = e.data.RemovePlayerInfo.Value;
			var shipId = "ship:" + data.UserSlotId;
			var shipEntity = WorldManager.instance.shipEntities.Where(x => x.Id == shipId).SingleOrDefault();
			if (shipEntity != null)
			{
				GameObject.Find(shipEntity.name);
				UnityEngine.Object.Destroy(GameObject.Find(shipEntity.name));
				//WorldManager.instance.shipEntities.RemoveAll(x => x.name == shipEntity.Id);
			}


			//TODO: REMOVE PLAYER ENTITY TOO..
		}

		public static void OnServerDisconnected(SocketIOEvent e)
		{
			Debug.Log("[SERVER DISCONNECTED]");

		}
		public static void OnNewShipBought(SocketIOEvent e)
		{
			Debug.Log("[NEW SHIP BOUGHT]");

		}
		public static void OnShipSail(SocketIOEvent e)
		{
			Debug.Log("[USER SHIP SAIL]");


		}
		public static void OnNewPlayer(SocketIOEvent e)
		{
			Debug.Log("[NEW USER CREATED]");
			NetInputController.SendBuyShipInput();
			SocketManager.instance.io.On(EventTypes.UpdateModel.ToString(), OnWorldUpdate);
			SocketManager.instance.io.On(EventTypes.RemovePlayer.ToString(), OnRemovePlayer);

		}

		/// <summary>
		/// World Update function 
		/// </summary>
		/// <param name="e"></param>
		public static void OnWorldUpdate(SocketIOEvent e)
		{
			var now = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
			//ALL PLAYERS CURRENT STATE UPDATES
			var updateTime = e.data.UpdateTime;
			var currentClientTime = now.TotalSeconds;
			var latency = currentClientTime - updateTime;
			DebugManager.instance.latency.text = "Latency: " + (float)latency * 100 + " ms";
			DebugManager.instance.totalUserCount.text = "Total Ship Count: " + e.data.ShipModelsLength;
			SocketManager.timeBetweenTick = (float)e.data.UpdatePassTime / 1000;
			SocketManager.latency = (int)latency;
			#region SupplyCrateStatus
			for (int i = 0; i < e.data.SupplyCratesLength; i++)
			{
				var supply = e.data.SupplyCrates(i).Value;
				if (supply.IsDeath)
				{
					//var deadSupply = WorldManager.instance.supplyEntities.Where(x => x.ID == supply).SingleOrDefault();
					//WorldManager.instance.supplyEntities.Remove(deadSupply);
					continue;
				}
				else if (supply.IsNew)
				{
					var supplyPosition = new Vector3(
						supply.Pos.Value.X,
						supply.Pos.Value.Y,
						supply.Pos.Value.Z);
					var assetName = supply.AssetId.ToString();
					var supplyId = supplyPosition.x.ToString() + supplyPosition.y.ToString() + supplyPosition.z.ToString();
					WorldManager.instance.InstantiateSupply(supplyId, assetName, supplyPosition);
					//WorldManager.instance.InstantiateSupply(supply.supplyId, supply.assetName, position);
				}
			}

			#endregion

			#region Ships
			for (int i = 0; i < e.data.ShipModelsLength; i++)
			{
				var shipModel = e.data.ShipModels(i).Value;
				WorldManager.instance.UpdateShips(shipModel);
			}
			#endregion
		}

		/// <summary>
		/// World Update function 
		/// </summary>
		/// <param name="e"></param>
		public static void OnWorldInfoDownloaded(SocketIOEvent e)
		{
			Debug.Log("[WORLD INFO DOWNLOADED]");

			WorldManager.instance.BuildWorld(e.data);
			WorldManager.instance.BuildSupplies(e.data);
			SocketManager.userSlotId = e.data.WorldInfo.Value.UserSlotId;
			SocketManager.instance.io.On(EventTypes.NewPlayer.ToString(), OnNewPlayer);
			NetInputController.SendNewPlayerInput();

			SocketManager.instance.io.On(EventTypes.BuyNewShip.ToString(), OnNewShipBought);
			SocketManager.instance.io.On(EventTypes.SailShip.ToString(), OnShipSail);
			//SocketManager.instance.io.Emit(ShipEvent.newShip.ToString());
		}
	}
}
