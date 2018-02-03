using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyGenerator : MonoBehaviour {

    public GameObject[] supplyPrefabs;

    private Transform parentObject;

    private void Awake() {
        if (GameObject.Find("_GeneratedSupplies") == null) {
            parentObject = new GameObject("_GeneratedSupplies").transform;
        } else {
            parentObject = GameObject.Find("_GeneratedSupplies").transform;
        }
    }

    public SupplyEntity InstantiateNewSupply(string id, string assetName, Vector3 position) {
        SupplyEntity prefab = GetPrefabFromAssetName(assetName);

        if (prefab == null) {
            Debug.LogError("!Assign a SUPPLY!");
            return null;
        }

        SupplyEntity entity = Instantiate(prefab.gameObject, position, Quaternion.identity, parentObject).GetComponent<SupplyEntity>();
        entity.ID = id;
        return entity;
    }

    private SupplyEntity GetPrefabFromAssetName(string assetName) {
        foreach (GameObject supplyPrefab in supplyPrefabs) {
            if (supplyPrefab.name == assetName) {
                return supplyPrefab.GetComponent<SupplyEntity>();
            }
        }
        Debug.LogError("Return default supply!");
        return supplyPrefabs[0].GetComponent<SupplyEntity>();
    }

}
