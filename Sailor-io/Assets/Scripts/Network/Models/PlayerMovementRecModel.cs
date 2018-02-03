using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Network.Utils;

namespace Assets.Scripts.Network.Models
{
	public class PlayerMovementRecModel
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
