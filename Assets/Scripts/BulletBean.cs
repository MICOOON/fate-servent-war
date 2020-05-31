using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBean : MonoBehaviour {
    private GameObject target;
    private float speed = 20F;
    private Tower tower;

    private void Start() {
        tower = this.GetComponentInParent<Tower>();
        Destroy(this.gameObject, 1F);
    }

    private void Update() {
        if (target) {
            Vector3 vector = target.transform.position - this.transform.position;
            this.GetComponent<Rigidbody>().velocity = vector.normalized * speed;
        } else {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag.Equals(GameConsts.SOLDIER)) {
            tower.beAttackedSoldiers.Remove(collider.gameObject);
            Destroy(collider.gameObject);
            Destroy(this.gameObject);
        } else if (collider.gameObject.tag.Equals(GameConsts.PLAYER)) {
            tower.beAttackedHeros.Remove(collider.gameObject);
            Destroy(collider.gameObject);
            Destroy(this.gameObject);
        }
    }

    public void SetTarget(GameObject target) {
        this.target = target;
    }
}
