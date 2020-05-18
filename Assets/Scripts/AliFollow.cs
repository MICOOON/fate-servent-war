using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliFollow : MonoBehaviour
{
    [SerializeField]
    private Transform ali;

    public float pyOffset;
    public float pzOffset;

    // Update is called once per frame
    void Update()
    {
        var px = ali.position.x;
        var py = ali.position.y;
        var pz = ali.position.z;

        transform.position = new Vector3(px, py + pyOffset, pz + pzOffset);
    }
}
