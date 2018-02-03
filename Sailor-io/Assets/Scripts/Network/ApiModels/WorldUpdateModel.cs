using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Network.ApiModels
{

	[Serializable]
	public class WorldUpdateModel
	{
		public long currTime;
		public SupplyCrate[] supplyCrates;
		public string updateTime;
		public int svTickRate;
		public int updatePassTime;

		public static WorldUpdateModel CreateFromJSON(string jsonString)
		{
			return JsonUtility.FromJson<WorldUpdateModel>(jsonString);
		}
	}
}
