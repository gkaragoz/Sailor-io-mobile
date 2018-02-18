using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Network.Models;
using Assets.Scripts.Network.Utils;
using FlatBuffers;
using SailorIO.ClientInputModel;
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

			SocketManager.pendingUserInputs.Add(newRequest);
			#endregion

			JSONObject userData = new JSONObject();
			userData.AddField("sequenceId", SocketManager.nonAckInputIndex);
			userData.AddField("deltaTime", dtTime.ToString());
			userData.AddField("vcX", directionVector.x);
			userData.AddField("vcZ", directionVector.z);
			userData.AddField("vcY", directionVector.y);
			var pendingInputLen = SocketManager.pendingUserInputs.Count;
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
			SocketManager.pendingUserInputs.Add(newRequest);
			#endregion

			userData.AddField("sequenceId", SocketManager.nonAckInputIndex);
			userData.AddField("deltaTime", dtTime.ToString());
			userData.AddField("vcX", directionVector.x);
			userData.AddField("vcZ", directionVector.z);
			userData.AddField("vcY", directionVector.y);
			var pendingInputLen = SocketManager.pendingUserInputs.Count;
			Debug.Log("[Player Movement Input Sent]");
			SocketManager.instance.io.Emit(PlayerEvent.playerMove.ToString(), userData);

		}

		/// <summary>
		/// wasd Input function 
		/// </summary>
		/// <param name="e"></param>
		public static void SendShipSupplyFeedInput(string shipIdstr, Vector3 supplyPos)
		{

			int shipId = int.Parse(shipIdstr.Split(':')[1]);
			FlatBufferBuilder fbb = new FlatBufferBuilder(1);
			FeedShip.StartFeedShip(fbb);
			FeedShip.AddShipId(fbb, shipId);
			FeedShip.AddSupplyPos(fbb, Vec3.CreateVec3(fbb, supplyPos.x, supplyPos.y, supplyPos.z));
			var feedShipOffset = FeedShip.EndFeedShip(fbb);

			ClientInput.StartClientInput(fbb);
			ClientInput.AddEventType(fbb, ClientEventTypes.FeedShip);
			ClientInput.AddEvent(fbb, feedShipOffset.Value);
			var clientInput = ClientInput.EndClientInput(fbb);
			ClientInput.FinishClientInputBuffer(fbb, clientInput);
			SocketManager.instance.io.Emit(fbb);

			Debug.Log("[Supply Feed]");

		}

		/// <summary>
		/// Send new player request
		/// </summary>
		public static void SendNewPlayerInput()
		{
			
			FlatBufferBuilder fbb = new FlatBufferBuilder(1);

			NewPlayer.StartNewPlayer(fbb);
			//TODO: SEND CLIENT INFO
			var newPlayerOffset = NewPlayer.EndNewPlayer(fbb);

			ClientInput.StartClientInput(fbb);
			ClientInput.AddEventType(fbb, ClientEventTypes.NewPlayer);
			ClientInput.AddEvent(fbb, newPlayerOffset.Value);
			var clientInput = ClientInput.EndClientInput(fbb);
			ClientInput.FinishClientInputBuffer(fbb, clientInput);
			SocketManager.instance.io.Emit(fbb);
			
			Debug.Log("[SENDING NEW PLAYER REQUEST INPUT]");

		}


		public static void SendGetWorldInfo()
		{
			FlatBufferBuilder fbb = new FlatBufferBuilder(1);

			ClientInput.StartClientInput(fbb);
			ClientInput.AddEventType(fbb, ClientEventTypes.GetWorldInfo);
			var clientInput = ClientInput.EndClientInput(fbb);
			ClientInput.FinishClientInputBuffer(fbb, clientInput);
			SocketManager.instance.io.Emit(fbb);

			Debug.Log("[SENDING GET WORLD INFO INPUT]");

		}

		public static void SendBuyShipInput()
		{
			FlatBufferBuilder fbb = new FlatBufferBuilder(1);
			BuyNewShip.StartBuyNewShip(fbb);

			//TODO CHANGE SHIPTYPES
			BuyNewShip.AddShip(fbb, ShipTypes.RAFT1);
			//TODO: SEND CLIENT INFO
			var newPlayerOffset = BuyNewShip.EndBuyNewShip(fbb);
			ClientInput.StartClientInput(fbb);
			ClientInput.AddEventType(fbb, ClientEventTypes.BuyNewShip);
			ClientInput.AddEvent(fbb, newPlayerOffset.Value);
			var clientInput = ClientInput.EndClientInput(fbb);
			ClientInput.FinishClientInputBuffer(fbb, clientInput);
			SocketManager.instance.io.Emit(fbb);

			Debug.Log("[SENDING BUY SHIP INPUT]");

		}
	}
}
