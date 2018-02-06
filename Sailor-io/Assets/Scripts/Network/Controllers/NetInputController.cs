using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Network.Models;
using Assets.Scripts.Network.Utils;
using UnityEngine;

namespace Assets.Scripts.Network.Controllers
{
	public static class NetInputController
	{
		/// <summary>
		/// Mouse Rotation Input function 
		/// </summary>
		/// <param name="e"></param>
		public static void SendPlayerRotationInput(Vector3 directionVector, float dtTime)
		{

			#region ServerReconciliation
			PlayerMovementRecModel newRequest = new PlayerMovementRecModel();
			newRequest.vcX = directionVector.x;
			newRequest.vcZ = directionVector.z;
			newRequest.vcY = directionVector.y;
			newRequest.inputSequenceId = SocketManager.nonAckInputIndex++;
			newRequest.dtTime = dtTime;
			newRequest.playerEventName = PlayerEvent.playerMouseRotation.ToString();
			newRequest.inputTime = DateTime.Now.Second + DateTime.Now.Millisecond;

			SocketManager.pendingInputs.Add(newRequest);
			#endregion

			JSONObject userData = new JSONObject();
			userData.AddField("sequenceId", SocketManager.nonAckInputIndex);
			userData.AddField("deltaTime", dtTime.ToString());
			userData.AddField("vcX", directionVector.x);
			userData.AddField("vcZ", directionVector.z);
			userData.AddField("vcY", directionVector.y);
			var pendingInputLen = SocketManager.pendingInputs.Count;
			Debug.Log("[Player Rotation Input Sent]");
			SocketManager.instance.io.Emit(PlayerEvent.playerMouseRotation.ToString(), userData);

		}
		/// <summary>
		/// wasd Input function 
		/// </summary>
		/// <param name="e"></param>
		public static void SendPlayerMovementInput(Vector3 directionVector, float dtTime)
		{
			JSONObject userData = new JSONObject();

			#region ServerReconciliation
			PlayerMovementRecModel newRequest = new PlayerMovementRecModel();
			newRequest.vcX = directionVector.x;
			newRequest.vcZ = directionVector.z;
			newRequest.vcY = directionVector.y;
			newRequest.inputSequenceId = SocketManager.nonAckInputIndex++;
			newRequest.dtTime = dtTime;
			newRequest.playerEventName = PlayerEvent.playerMove.ToString();
			newRequest.inputTime = DateTime.Now.Second + DateTime.Now.Millisecond;
			SocketManager.pendingInputs.Add(newRequest);
			#endregion

			userData.AddField("sequenceId", SocketManager.nonAckInputIndex);
			userData.AddField("deltaTime", dtTime.ToString());
			userData.AddField("vcX", directionVector.x);
			userData.AddField("vcZ", directionVector.z);
			userData.AddField("vcY", directionVector.y);
			var pendingInputLen = SocketManager.pendingInputs.Count;
			Debug.Log("[Player Movement Input Sent]");
			SocketManager.instance.io.Emit(PlayerEvent.playerMove.ToString(), userData);

		}

		/// <summary>
		/// wasd Input function 
		/// </summary>
		/// <param name="e"></param>
		public static void SendShipSupplyFeedInput(string shipId, string supplyId)
		{
			JSONObject userData = new JSONObject();

			userData.AddField("shipId", shipId);
			userData.AddField("supplyId", supplyId);

			SocketManager.instance.io.Emit(SupplyEvent.feedShip.ToString(), userData);

		}
	}
}
