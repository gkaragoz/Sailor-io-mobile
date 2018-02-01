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
    public float MAP_CENTER;
    [DisplayWithoutEdit]
    public float MAP_LENGTH;
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

    private Vector2 GetRandomPosition() {
        float posX = Random.Range(-MAP_LENGTH, MAP_LENGTH);
        float posY = Random.Range(-MAP_HEIGHT, MAP_HEIGHT);
        return new Vector2(posX, posY);
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
        get { return generatedSuppliesCount >= generateCount ? true : false; }
        set { isTotallyGenerated = value; }
    }

    private Vector2 GetMapCenter() {
        if (mapObject == null) {
            Debug.LogError("Assign a map!");
            return Vector2.zero;
        }
        return mapObject.position;
    }

	private float GetMapHeight() {
        if (mapObject == null) {
            Debug.LogError("Assingn a map!");
            return 100f;
        }
        return GetMapCenter().y + mapObject.localScale.y;
    }

    private float GetMapLength() {
        if (mapObject == null) {
            Debug.LogError("Assign a map!");
            return 100f;
        }
        return GetMapCenter().x + mapObject.localScale.x;
    }
}
