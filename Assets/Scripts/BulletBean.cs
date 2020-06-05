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
        if (target != collider.gameObject) {
            return;
        }
        if (collider.tag.Equals(GameConsts.SOLDIER)) {
            HpChange hpChange = collider.gameObject.GetComponent<HpChange>();
            hpChange.beDamaged(0.5F);
            if (hpChange.hpScript.HpValue <= 0) {
                tower.beAttackedSoldiers.Remove(collider.gameObject);
                Destroy(collider.gameObject);
            }
            Destroy(this.gameObject);
        } else if (collider.tag.Equals(GameConsts.PLAYER)) {
            HpChange hpChange = collider.gameObject.GetComponent<HpChange>();
            hpChange.beDamaged(0.5F);
            if (hpChange.hpScript.HpValue <= 0) {
                tower.beAttackedHeros.Remove(collider.gameObject);
                Destroy(collider.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    public void SetTarget(GameObject target) {
        this.target = target;
    }
}
