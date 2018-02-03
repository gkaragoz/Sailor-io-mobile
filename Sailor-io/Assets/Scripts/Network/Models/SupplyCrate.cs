using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Network.ApiModels
{
	[Serializable]
	public class SupplyCrate
	{
		public float pos_x;
		public float pos_z;
		public float pos_y;
		public string supplyId;
		public int supplyIncome;
		public string assetName;
	}
}
