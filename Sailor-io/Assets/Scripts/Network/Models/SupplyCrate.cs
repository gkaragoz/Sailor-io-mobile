using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Network.ApiModels
{
	[Serializable]
	public class SupplyCrate
	{
		public double pos_x;
		public double pos_z;
		public double pos_y;
		public string supplyId;
		public int supplyIncome;
		public string assetName;
	}
}
