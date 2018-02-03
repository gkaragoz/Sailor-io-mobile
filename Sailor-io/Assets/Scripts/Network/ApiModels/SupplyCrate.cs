using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Network.ApiModels
{
	[Serializable]
	public class SupplyCrate
	{
		public double pos_x { get; set; }
		public double pos_z { get; set; }
		public double pos_y { get; set; }
		public string supplyId { get; set; }
		public int supplyIncome { get; set; }
		public string supplyName { get; set; }
	}
}
