using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
			SocketManager.instance.io.On(BaseEvent.connect.ToString(), ConnectionSuccess);
		}


		#region Callbacks
		/// <summary>
		/// ConnectionSuccess function is callback when fired after connected successfully to socket
		/// </summary>
		/// <param name="e"></param>
		public static void ConnectionSuccess(SocketIOEvent e)
		{
			Debug.Log("[CONNECTED] to: " + SocketManager.instance.io.url);
		}
		#endregion
	}
}
