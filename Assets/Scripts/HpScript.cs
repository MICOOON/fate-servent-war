using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpScript : MonoBehaviour {
    [SerializeField]
    private Transform hpPrefab;

    private float hpValue;

    public float HpValue {
        get {
            return hpValue;
        }
        set {
            hpValue = value;
            hpPrefab.localScale = new Vector3(hpValue, 1, 1);
            hpPrefab.localPosition = new Vector3((1 - hpValue) * -0.8F, 0);
        }
    }
}
