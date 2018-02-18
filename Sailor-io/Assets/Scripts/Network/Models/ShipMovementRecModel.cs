using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Network.Models
{
	public class ShipMovementRecModel
	{
		public float vcX;
		public float vcZ;
		public float vcY;
		public int inputSequenceId;
		public long inputTime;
		public float dtTime;
		public string playerEventName;
	}
}
