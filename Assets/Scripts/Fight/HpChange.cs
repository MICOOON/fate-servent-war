using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpChange : MonoBehaviour
{
    public HpScript hpScript;

    private void Start() {
        hpScript = GetComponentInChildren<HpScript>();
        hpScript.HpValue = 1;
    }

    public void beDamaged(float hp) {
        hpScript.HpValue -= hp;
    }
}
