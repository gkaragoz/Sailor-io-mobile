using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Network.Models;
using UnityEngine;

namespace Assets.Scripts.Network.ApiModels
{

	[Serializable]
	public class WorldUpdateModel
	{
		[NonSerialized]
		public long currTime;

		[NonSerialized]
		public SupplyCrate[] supplyCrates;

		[NonSerialized]
		public ShipModel[] shipModels;

		[NonSerialized]
		public string updateTime;

		[NonSerialized]
		public int svTickRate;

		[NonSerialized]
		public int updatePassTime;

		public SupplyCrate[] c
		{
			get { return supplyCrates; }
		}

		public ShipModel[] s
		{
			get { return shipModels; }
		}

		public int r {
			get { return svTickRate; }
		}

		public long t
		{
			get { return currTime; }
		}

		public string u
		{
			get { return updateTime; }
		}

		public int p
		{
			get { return updatePassTime; }
		}


		public static WorldUpdateModel CreateFromJSON(string jsonString)
		{
			return JsonUtility.FromJson<WorldUpdateModel>(jsonString);
		}
	}
}
