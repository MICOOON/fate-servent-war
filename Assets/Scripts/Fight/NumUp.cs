using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Remove", 1.5F);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime);
    }

    void Remove() {
        Destroy(gameObject);
    }
}
