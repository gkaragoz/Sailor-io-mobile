using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Network;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour
{
	public static DebugManager instance;

	public Text latency;
	public Text userPos;
	public Text userAngle;
	public Text usersShipAngle;
	public Text usersShipVector;
	public Text totalUserCount;


	private void Awake()
	{
		if (instance == null)
			instance = this;

	}

}
