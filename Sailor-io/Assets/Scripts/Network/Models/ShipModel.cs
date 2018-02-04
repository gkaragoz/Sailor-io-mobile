using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

namespace Assets.Scripts.Network.Models
{

	[Serializable]
	public class ShipModel
	{
		public float pos_x;
		public float pos_z;
		public float pos_y;
		public string Id;
		public string assetName;
		public string captainUserId;
		public int maxSuppliesCount;
		public int currentSuppliesCount;
		public int maxSailorsCount;
		public int currentSailorsCount;
		public int maxHealth;
		public int currentHealth;
		public float slopeSpeed;
		public float rotationSpeed;
		public float movementSpeed;
		[CanBeNull] public InputModel[] inputs;

	}
}
