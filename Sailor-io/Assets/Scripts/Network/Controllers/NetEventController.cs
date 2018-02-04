﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Network.ApiModels;
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
			//GameManager.timeBetweenTick = (float)updateModel.updatePassTime / 1000;

			//DebugManager.instance.latencyTxt.text = "Latency: " + latency.ToString() + " ms";
			//GameManager.latency = (int)latency;
			//GameManager.tickRate = updateModel.svTickRate;
			//DebugManager.instance.serverTickRate.text = "Tick Rate: " + updateModel.svTickRate.ToString();

            //HandleSupplies
			#region SupplyCrateStatus
			foreach (var supply in updateModel.supplyCrates)
			{
                Vector3 position = new Vector3(supply.pos_x, supply.pos_y, supply.pos_z);
                WorldManager.instance.InstantiateSupply(supply.supplyId, supply.assetName, position);
			}
			#endregion

			#region Ships

			foreach (var ship in updateModel.shipModels)
			{
				Vector3 position = new Vector3(ship.pos_x, ship.pos_y, ship.pos_z);
				//WorldManager.instance.InstantiateSupply(supply.supplyId, supply.assetName, position);
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
