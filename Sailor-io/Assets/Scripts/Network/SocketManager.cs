using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Network.Controllers;
using Assets.Scripts.Network.Models;
using SocketIO;
using UnityEngine;

namespace Assets.Scripts.Network
{
	public class SocketManager : MonoBehaviour
	{
		public static SocketManager instance;
		public SocketIOComponent io;

		public static List<PlayerMovementRecModel> pendingInputs;
		public static int nonAckInputIndex;
		public static int latency;
		public static int tickRate;
		public static float timeBetweenTick;

		void Awake()
		{
			if (instance == null)
				instance = this;

			DontDestroyOnLoad(instance);
			pendingInputs = new List<PlayerMovementRecModel>();

		}
		void Start()
		{
			io = GetComponent<SocketIOComponent>();

			//Register Listener Events
			NetEventController.RegisterEvents();
		}

		void FixedUpdate()
		{
			ClientEntityInterpolation();
		}

		public void ClientEntityInterpolation()
		{
			#region Player_EntityInterpolation

			var now = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0));

			var currInterpolationTime = now.TotalSeconds;
			//var clientRenderTime = currInterpolationTime - GameManager.timeBetweenTick;
			var clientRenderTime = currInterpolationTime - timeBetweenTick;


			//foreach (var interpolatedPlayer in GameManager.PlayerList)
			//{

			//	var movementSpeed = interpolatedPlayer.GetComponent<PlayerManager>().GetMovementSpeed();
			//	//we dont need to interpolate current player.
			//	if (interpolatedPlayer.isMe || interpolatedPlayer.positionBuffer.Count == 0)
			//		continue;



			//	while (interpolatedPlayer.positionBuffer.Count >= 2 && interpolatedPlayer.positionBuffer[1].updateTime <= clientRenderTime)
			//	{
			//		interpolatedPlayer.positionBuffer = interpolatedPlayer.positionBuffer.Skip(1).ToList();
			//		Debug.Log("total position buffer-> " + interpolatedPlayer.positionBuffer.Count);

			//	}

			//	// Interpolate between the two surrounding authoritative positions.
			//	if (interpolatedPlayer.positionBuffer.Count >= 2 && clientRenderTime >= interpolatedPlayer.positionBuffer[0].updateTime && clientRenderTime <= interpolatedPlayer.positionBuffer[1].updateTime)
			//	{
			//		// Find the two authoritative positions surrounding the rendering timestamp.
			//		Debug.Log("pot[0] -> " + interpolatedPlayer.positionBuffer[0].updateTime);
			//		Debug.Log("curr -> " + (double)clientRenderTime);
			//		Debug.Log("pot[1] -> " + interpolatedPlayer.positionBuffer[1].updateTime);
			//		float x0 = interpolatedPlayer.positionBuffer[0].x;
			//		float x1 = interpolatedPlayer.positionBuffer[1].x;
			//		float y0 = interpolatedPlayer.positionBuffer[0].y;
			//		float y1 = interpolatedPlayer.positionBuffer[1].y;
			//		double t0 = interpolatedPlayer.positionBuffer[0].updateTime;
			//		double t1 = interpolatedPlayer.positionBuffer[1].updateTime;

			//		//float total = (float)(t1 - t0);
			//		//float portion = (float)(clientRenderTime - t0);
			//		//var ratio = portion / total;

			//		//var interpX = Mathf.Lerp(x0, x1, ratio);
			//		//var interpY = Mathf.Lerp(y0, y1, ratio);

			//		var interpX = x0 + (x1 - x0) * (clientRenderTime - t0) / (t1 - t0);
			//		var interpY = y0 + (y1 - y0) * (clientRenderTime - t0) / (t1 - t0);


			//		var directionVector = new Vector2((float)interpX, (float)interpY);
			//		interpolatedPlayer.transform.position = directionVector;
			//	}

			//}

			#endregion
		}
	}
}