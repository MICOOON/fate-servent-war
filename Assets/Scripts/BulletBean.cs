using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBean : MonoBehaviour {
    private GameObject target;
    private float speed = 20F;

    private void Start()
    {
        Destroy(this.gameObject, 1F);
    }

    private void Update()
    {
        if (target)
        {
            Vector3 vector = target.transform.position - this.transform.position;
            this.GetComponent<Rigidbody>().velocity = vector.normalized * speed;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SetTarget(GameObject target) {
        this.target = target;
    }
}
