using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Network.Utils
{
	public enum WorldEvent
	{
		getWorldInfo,
		worldUpdate
	}
	public enum PlayerEvent
	{
		playerMouseRotation,
		playerMove
	}

	public enum BaseEvent
	{
		connect,
		disconnect,
	}
}
