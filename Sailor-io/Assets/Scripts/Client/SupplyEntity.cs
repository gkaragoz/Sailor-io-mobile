using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SupplyEntity : MonoBehaviour {

    public string _id;
    public int _income;

    public string ID {
        get { return _id; }
        set { _id = value; }
    }

    public int Income {
        get { return _income; }
        set { _income = value; }
    }
}
