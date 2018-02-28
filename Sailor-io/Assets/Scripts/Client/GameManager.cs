using Assets.Scripts.Client;
using Assets.Scripts.Network;
using SocketIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public bool offlineMode = false;
    public GameObject offlineShipPrefab;

    private SocketManager socketManager;
    private EInterpManager einterpManager;
    private SocketIOComponent socketIOComponent;

    private void Awake() {
        if (instance == null)
            instance = this;
    }

    private void Start() {
        SetStatusOfNetworkComponents();
    }

    private void SetStatusOfNetworkComponents() {
        socketManager = GetComponentInChildren<SocketManager>(true);
        einterpManager = GetComponentInChildren<EInterpManager>(true);
        socketIOComponent = GetComponentInChildren<SocketIOComponent>(true);

        if (offlineMode) {
            socketManager.enabled = false;
            einterpManager.enabled = false;
            socketIOComponent.enabled = false;

            UIManager.instance.ShowOfflineModeUI();

            GameObject offlineShip = Instantiate(offlineShipPrefab);
            UltimateOrbitCamera.instance.SetTarget(offlineShip.transform);
        }
    }

    public void StartGame() {
        socketManager.enabled = true;
        einterpManager.enabled = true;
        socketIOComponent.enabled = true;
        UltimateOrbitCamera.instance.enabled = true;
    }
}
