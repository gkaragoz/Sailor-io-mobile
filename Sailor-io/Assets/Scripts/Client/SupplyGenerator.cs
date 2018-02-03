using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyGenerator : MonoBehaviour {

    private Transform parentObject;

    private void Awake() {
        if (GameObject.Find("_GeneratedSupplies") == null) {
            parentObject = new GameObject("_GeneratedSupplies").transform;
        } else {
            parentObject = GameObject.Find("_GeneratedSupplies").transform;
        }
    }

    private GameObject InstantiateNewSupply(GameObject prefab, Vector3 position) {
        if (prefab == null) {
            Debug.LogError("!Assign a SUPPLY!");
            return null;
        }

        GameObject supply = Instantiate(prefab, position, Quaternion.identity, parentObject);
        return supply;
    }

}
