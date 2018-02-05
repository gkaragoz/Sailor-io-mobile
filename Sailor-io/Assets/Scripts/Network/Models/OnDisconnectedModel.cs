using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Network.ApiModels;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Network.Models
{
	[Serializable]
	public class OnDisconnectedModel
	{
		public string userId;
		[CanBeNull] public string shipId;
		public static OnDisconnectedModel CreateFromJSON(string jsonString)
		{
			return JsonUtility.FromJson<OnDisconnectedModel>(jsonString);
		}
	}
}
