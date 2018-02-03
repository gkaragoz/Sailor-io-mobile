using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Scripts.Network.ApiModels;

public class WorldManager : MonoBehaviour {

    public static WorldManager instance;

    [Header("Initialize")]
    public Transform mapObject;

    [Header("Good to know")]
    public Vector3 MAP_CENTER;
    public float MAP_LENGTH;
    public float MAP_WIDTH;
    public float MAP_HEIGHT;

    public List<SupplyEntity> supplyEntities = new List<SupplyEntity>();

    private SupplyGenerator supplyGenerator; 

    private void Awake() {
        if (instance == null)
            instance = this;

        supplyGenerator = GetComponent<SupplyGenerator>();
    }

    public void BuildWorld(WorldInfoModel worldInfoModel) {
        Vector3 centerPos = new Vector3(worldInfoModel.offSetX, 0f, worldInfoModel.offSetZ);
        mapObject = Instantiate(mapObject, centerPos, Quaternion.identity);
        mapObject.transform.localScale = new Vector3(worldInfoModel.length * 0.1f, 1f, worldInfoModel.width * 0.1f);
        mapObject.name = "Sea";
    }

    public void InstantiateSupply(string supplyId, string assetName, Vector3 position) {
        if (supplyEntities.Count <= 0) {
            supplyEntities.Add(supplyGenerator.InstantiateNewSupply(supplyId, assetName, position));
            return;
        }

        //Nobody collected that supply yet.
        if(supplyEntities.Where(supply => supply.ID == supplyId).Any()) {
            return;
        }

        supplyEntities.Add(supplyGenerator.InstantiateNewSupply(supplyId, assetName, position));
    }

    public Vector3 GetMapCenter() {
        if (mapObject == null) {
            Debug.LogError("Assign a map!");
            return Vector3.zero;
        }
        MAP_CENTER = new Vector3(mapObject.position.x, mapObject.position.y, mapObject.position.z);
        return MAP_CENTER;
    }

    public float GetMapHeight() {
        if (mapObject == null) {
            Debug.LogError("Assingn a map!");
            return 100f;
        }
        float centerOffset = GetMapCenter().y;
        MAP_HEIGHT = centerOffset + (mapObject.GetComponent<MeshRenderer>().bounds.size.y * 0.5f);
        return MAP_HEIGHT;
    }

    public float GetMapLength() {
        if (mapObject == null) {
            Debug.LogError("Assign a map!");
            return 100f;
        }
        float centerOffset = GetMapCenter().x;
        MAP_LENGTH = centerOffset + (mapObject.GetComponent<MeshRenderer>().bounds.size.x * 0.5f);
        return MAP_LENGTH;
    }

    public float GetMapWidth() {
        if (mapObject == null) {
            Debug.LogError("Assign a map!");
            return 100f;
        }
        float centerOffset = GetMapCenter().z;
        MAP_WIDTH = centerOffset + (mapObject.GetComponent<MeshRenderer>().bounds.size.z * 0.5f);
        return MAP_WIDTH;
    }

}
