using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Network;
using UnityEngine;

namespace Assets.Scripts.Client
{
	public class EInterpManager : MonoBehaviour
	{
		public static EInterpManager instance;

		// Use this for initialization
		void Awake()
		{
			if (instance == null)
				instance = this;

		}
		// Update is called once per frame
		void Update()
		{
			ShipInterpMovement();
		}

		public void ShipInterpMovement()
		{
			var now = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0));

			var currInterpolationTime = now.TotalSeconds;
			var clientRenderTime = currInterpolationTime - SocketManager.timeBetweenTick;
			foreach (var shipEntity in WorldManager.instance.shipEntities)
			{
				//we dont need to interpolate current player.
				if (shipEntity.PositionEntries.Count == 0)
					continue;

				while (shipEntity.PositionEntries.Count >= 2 && shipEntity.PositionEntries[1].updateTime <= clientRenderTime)
				{
					shipEntity.PositionEntries = shipEntity.PositionEntries.Skip(1).ToList();
				}

				// Interpolate between the two surrounding authoritative positions.
				if (shipEntity.PositionEntries.Count >= 2 && clientRenderTime >= shipEntity.PositionEntries[0].updateTime && clientRenderTime <= shipEntity.PositionEntries[1].updateTime)
				{
				
					float x0 = shipEntity.PositionEntries[0].position.x;
					float x1 = shipEntity.PositionEntries[1].position.x;
					float z0 = shipEntity.PositionEntries[0].position.z;
					float z1 = shipEntity.PositionEntries[1].position.z;
					double t0 = shipEntity.PositionEntries[0].updateTime;
					double t1 = shipEntity.PositionEntries[1].updateTime;

					var interpX = x0 + (x1 - x0) * (clientRenderTime - t0) / (t1 - t0);
					var interpZ = z0 + (z1 - z0) * (clientRenderTime - t0) / (t1 - t0);

					var directionVector = new Vector3((float)interpX, shipEntity.transform.position.y, (float)interpZ);
					shipEntity.transform.position = directionVector;
				}

			}
		}
	}
}
