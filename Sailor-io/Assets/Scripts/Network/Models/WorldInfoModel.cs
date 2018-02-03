using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Network.ApiModels
{
	[Serializable]
	public class WorldInfoModel
	{
		public int height;
		public int width;
		public int length;
		public int offSetX;
		public int offSetZ;
		public float deltaTime;
		public float supplyRespawnSec;

		public static WorldInfoModel CreateFromJSON(string jsonString)
		{
			return JsonUtility.FromJson<WorldInfoModel>(jsonString);
		}
	}
}
