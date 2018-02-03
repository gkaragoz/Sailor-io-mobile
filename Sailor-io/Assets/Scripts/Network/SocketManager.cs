using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Network.Controllers;
using SocketIO;
using UnityEngine;

namespace Assets.Scripts.Network
{
	public class SocketManager : MonoBehaviour
	{
		public static SocketManager instance;
		public SocketIOComponent io;

		void Awake()
		{

			if (instance == null)
				instance = this;
			DontDestroyOnLoad(instance);

		}
		void Start()
		{
			io = GameObject.Find("SocketIO").GetComponent<SocketIOComponent>();

			//Register Listener Events
			NetEventController.RegisterEvents();

		}
	}
}