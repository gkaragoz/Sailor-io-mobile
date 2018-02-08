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
			SocketManager.instance.io.On(EventTypes.WorldInfoUpdate.ToString(), OnWorldInfoDownloaded);
			SocketManager.instance.io.On(PlayerEvent.playerDisconnected.ToString(), OnPlayerDisconnected);
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

			SocketManager.instance.io.Emit(ClientEventEnum.GET_WORLD_INFO);
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
			ByteBuffer bb = new ByteBuffer(e.rawData);
			var updateModel = UpdateModel.GetRootAsUpdateModel(bb);
			
			var now = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
			//ALL PLAYERS CURRENT STATE UPDATES
			var updateTime = updateModel.UpdateTime;
			var currentClientTime = now.TotalSeconds;
			var latency = currentClientTime - updateTime;
			DebugManager.instance.latency.text = "Latency: " + (float)latency*100 + " ms";
			DebugManager.instance.totalUserCount.text = "Total Ship Count: " + updateModel.ShipModelsLength;
			SocketManager.timeBetweenTick = (float)updateModel.UpdatePassTime / 1000;
			SocketManager.latency = (int)latency;
			//Debug.Log("Time between tick: "+ SocketManager.timeBetweenTick);
            //HandleSupplies
			//#region SupplyCrateStatus
			//foreach (var supply in updateModel.SupplyCrates)
			//{
			//	if (supply.isDeath)
			//	{
			//		var deadSupply = WorldManager.instance.supplyEntities.Where(x => x.ID == supply.supplyId).SingleOrDefault();
			//		WorldManager.instance.supplyEntities.Remove(deadSupply);
			//		continue;
			//	}
   //             Vector3 position = new Vector3(supply.pos_x, supply.pos_y, supply.pos_z);
   //             WorldManager.instance.InstantiateSupply(supply.supplyId, supply.assetName, position);
			//}
			//#endregion

			//#region Ships
			//foreach (var shipFromServer in updateModel.shipModels)
			//{
			//	WorldManager.instance.UpdateShips(shipFromServer);				
			//}
			//#endregion
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
			SocketManager.instance.io.Emit(PlayerEvent.playerNew.ToString());
			SocketManager.instance.io.On(EventTypes.UpdateModel.ToString(), OnWorldUpdate);

			JSONObject userData = new JSONObject();
			userData.AddField("shipType", Ships.Raft1.ToString());
			SocketManager.instance.io.Emit(ShipEvent.newShip.ToString(), userData);
		}
	}
}
