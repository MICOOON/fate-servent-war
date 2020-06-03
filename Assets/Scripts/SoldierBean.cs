using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierBean : MonoBehaviour {
    private NavMeshAgent nav;

    private Animation ani;

    public Transform target;

    public Transform[] towers;

    public int type;

    // Start is called before the first frame update
    void Start() {
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animation>();
        target = FindTarget();
    }

    // Update is called once per frame
    void Update() {
        Move();
    }

    void Move() {
        if (target == null) {
            target = FindTarget();
            return;
        }
        ani.CrossFade("Run");
        nav.SetDestination(target.position);

        float distance = Vector3.Distance(transform.position, target.position);
        if (distance > 5) {
            nav.speed = 3.5F;
        } else {
            nav.speed = 0;
            Vector3 targetPos = target.position;
            Vector3 attackPos = new Vector3(targetPos.x, transform.position.y, targetPos.z);
            transform.LookAt(attackPos);
            ani.CrossFade("Attack1");
        }
    }

    Transform FindTarget() {
        for (int i = 0; i < towers.Length; i++) {
            if (towers[i] != null) {
                return towers[i];
            }
        }
        return null;
    }

    public void SetAreaMask(int road) {
        nav = GetComponent<NavMeshAgent>();
        nav.areaMask = road;
    }
}
