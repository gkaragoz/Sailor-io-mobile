using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSupplyGenerator : MonoBehaviour {

    [Header("Initialize")]
    public GameObject supplyPrefab;
    public Transform mapObject;

    [Header("Settings")]
    public float generateRate;
    [Range(1, 100)]
    public float generateCount;

    [Header("Good to know")]
    [DisplayWithoutEdit]
    public Vector3 MAP_CENTER;
    [DisplayWithoutEdit]
    public float MAP_LENGTH;
    [DisplayWithoutEdit]
    public float MAP_WIDTH;
    [DisplayWithoutEdit]
    public float MAP_HEIGHT;

    [Header("Infos")]
    [DisplayWithoutEdit]
    public int generatedSuppliesCount;

    [DisplayWithoutEdit]
    public bool isTotallyGenerated;

    private Transform parentObject;

    private void Awake() {
        if (GameObject.Find("_GeneratedSupplies") == null) {
            parentObject = new GameObject("_GeneratedSupplies").transform;
        } else {
            parentObject = GameObject.Find("_GeneratedSupplies").transform;
        }

        StartToGenerate();
    }

    public void StartToGenerate() {
        StartCoroutine(Generate());
    }

    public void StopGenerating() {
        StopAllCoroutines();
    }

    private IEnumerator Generate() {
        while (IsTotallyGenerated) {
            InstantiateNewSupply();
            yield return new WaitForSeconds(generateRate);
        }
    }

    private Vector3 GetRandomPosition() {
        float posX = Random.Range(-1 * GetMapLength(), GetMapLength());
        float posY = GetMapHeight() * 0.5f;
        float posZ = Random.Range(-1 * GetMapWidth(), GetMapWidth());
        return new Vector3(posX, posY, posZ);
    }

    private void InstantiateNewSupply() {
        if (supplyPrefab == null) {
            Debug.LogError("!Assign a SUPPLY!");
            StopGenerating();
            return;
        }

        Instantiate(supplyPrefab, GetRandomPosition(), Quaternion.identity, parentObject);
        generatedSuppliesCount++;
    }

    private bool IsTotallyGenerated {
        get { return generatedSuppliesCount >= generateCount ? false : true; }
        set { isTotallyGenerated = value; }
    }

    private Vector3 GetMapCenter() {
        if (mapObject == null) {
            Debug.LogError("Assign a map!");
            return Vector3.zero;
        }
        MAP_CENTER = new Vector3(mapObject.position.x, mapObject.position.y, mapObject.position.z);
        return MAP_CENTER;
    }

	private float GetMapHeight() {
        if (mapObject == null) {
            Debug.LogError("Assingn a map!");
            return 100f;
        }
        float centerOffset = GetMapCenter().y;
        MAP_HEIGHT = centerOffset + (mapObject.GetComponent<MeshRenderer>().bounds.size.y * 0.5f);
        return MAP_HEIGHT;
    }

    private float GetMapLength() {
        if (mapObject == null) {
            Debug.LogError("Assign a map!");
            return 100f;
        }
        float centerOffset = GetMapCenter().x;
        MAP_LENGTH = centerOffset + (mapObject.GetComponent<MeshRenderer>().bounds.size.x * 0.5f);
        return MAP_LENGTH;
    }

    private float GetMapWidth() {
        if (mapObject == null) {
            Debug.LogError("Assign a map!");
            return 100f;
        }
        float centerOffset = GetMapCenter().z;
        MAP_WIDTH = centerOffset + (mapObject.GetComponent<MeshRenderer>().bounds.size.z * 0.5f);
        return MAP_WIDTH;
    }
}
