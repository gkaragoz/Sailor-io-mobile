using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Network.ApiModels;
using Assets.Scripts.Network.Models;
using Assets.Scripts.Network.Utils;
using SocketIO;
using UnityEngine;

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
			SocketManager.instance.io.On(WorldEvent.getWorldInfo.ToString(), OnWorldInfoDownloaded);
			SocketManager.instance.io.On(PlayerEvent.playerDisconnected.ToString(), OnPlayerDisconnected);
		}

		/// <summary>
		/// ConnectionSuccess function is callback when fired after connected successfully to socket
		/// </summary>
		/// <param name="e"></param>
		public static void OnConnectionSuccess(SocketIOEvent e)
		{
			Debug.Log("[CONNECTED] to: " + SocketManager.instance.io.url);
			Debug.Log("[MY CLIENT ID] > " + SocketManager.instance.io.sid);
			SocketManager.instance.io.Emit(WorldEvent.getWorldInfo.ToString());
		}
		/// <summary>
		/// ConnectionSuccess function is callback when fired after connected successfully to socket
		/// </summary>
		/// <param name="e"></param>
		public static void OnPlayerDisconnected(SocketIOEvent e)
		{
			Debug.Log("[Disconnected]" + SocketManager.instance.io.sid);
			var onDcModel = OnDisconnectedModel.CreateFromJSON(e.data.ToString());
			if (onDcModel.shipId != null)
			{
				var shipEntity = WorldManager.instance.shipEntities.Where(x => x.Id == onDcModel.shipId).SingleOrDefault();
				if (shipEntity != null)
				{
					GameObject.Find(shipEntity.name);
					UnityEngine.Object.Destroy(GameObject.Find(shipEntity.name));
					WorldManager.instance.shipEntities.RemoveAll(x => x.name == shipEntity.Id);
				}
			}
		}


		/// <summary>
		/// World Update function 
		/// </summary>
		/// <param name="e"></param>
		public static void OnWorldUpdate(SocketIOEvent e)
		{
			var updateModel = WorldUpdateModel.CreateFromJSON(e.data.ToString());
			var now = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
			//ALL PLAYERS CURRENT STATE UPDATES
			var updateTime = double.Parse(updateModel.updateTime);
			var currentClientTime = now.TotalSeconds;
			var latency = currentClientTime - updateTime;
			DebugManager.instance.latency.text = "Latency: " + (float)latency*100 + " ms";
			DebugManager.instance.totalUserCount.text = "Total Ship Count: " + updateModel.shipModels.Length;
			SocketManager.timeBetweenTick = (float)updateModel.updatePassTime / 1000;
			SocketManager.latency = (int)latency;
			SocketManager.tickRate = updateModel.svTickRate;

            //HandleSupplies
			#region SupplyCrateStatus
			foreach (var supply in updateModel.supplyCrates)
			{
                Vector3 position = new Vector3(supply.pos_x, supply.pos_y, supply.pos_z);
                WorldManager.instance.InstantiateSupply(supply.supplyId, supply.assetName, position);
			}
			#endregion

			#region Ships
			foreach (var shipFromServer in updateModel.shipModels)
			{
				WorldManager.instance.UpdateShips(shipFromServer);				
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
			var worldInfoModel = WorldInfoModel.CreateFromJSON(e.data.ToString());

            WorldManager.instance.BuildWorld(worldInfoModel);

			SocketManager.instance.io.Emit(PlayerEvent.playerNew.ToString());
			SocketManager.instance.io.On(WorldEvent.worldUpdate.ToString(), OnWorldUpdate);

			JSONObject userData = new JSONObject();
			userData.AddField("shipType", Ships.Raft1.ToString());
			SocketManager.instance.io.Emit(ShipEvent.newShip.ToString(), userData);
		}
	}
}
