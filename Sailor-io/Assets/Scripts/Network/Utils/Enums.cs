using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Network.Utils
{
	public enum WorldEvent
	{
		getWorldInfo,
		worldUpdate,
		
	}
	public enum ShipEvent
	{
		newShip
	}

	public enum SupplyEvent
	{
		feedShip
	}
	public enum PlayerEvent
	{
		playerMouseRotation,
		playerMove,
		playerNew,
		playerDisconnected
	}
	public enum Ships
	{
		Raft1,
		Raft2,
		Raft3,
		Boat1,
		Boat2,
		Boat3,
		Destroyer1,
		Destroyer2,
		Destroyer3,
	}

	public enum BaseEvent
	{
		connect,
		disconnect,
	}
}
