using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierBean : MonoBehaviour {
    private NavMeshAgent nav;

    private Animation ani;

    public Transform target;

    public Transform[] towers;

    public List<Transform> enemies = new List<Transform>();

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
        enemies.RemoveAll(t => t == null);
        if (enemies.Count > 0) {
            return enemies[0];
        }

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

    public void Attack() {
        if (target == null) {
            target = FindTarget();
            return;
        }

        float distance = Vector3.Distance(transform.position, target.position);
        if (distance > 5) {
            return;
        }

        HpChange hpChange = target.GetComponent<HpChange>();
        float damage = Random.Range(0.1F, 0.6F);
        hpChange.beDamaged(damage);

        if (hpChange.hpScript.HpValue <= 0) {
            Destroy(target.gameObject);

            if (enemies.Contains(target.transform)) {
                enemies.Remove(target.transform);
            } else {
                for (int i = 0; i < towers.Length; i++) {
                    if (towers[i] == target.transform) {
                        towers[i] = null;
                    }
                }
            }

            nav.SetDestination(target.transform.position);
            nav.speed = 3.5F;
            ani.CrossFade("Run");
        }
    }

    private void OnTriggerEnter(Collider collider) {
        if (collider.tag.Equals(GameConsts.PLAYER)) {
            enemies.Add(collider.transform);
        } else if (collider.tag.Equals(GameConsts.SOLDIER)) {
            SoldierBean soldierBean = collider.GetComponent<SoldierBean>();
            if (soldierBean.type != type) {
                enemies.Add(soldierBean.transform);
            }
        }
        if (enemies.Count > 0) {
            Transform newTarget = enemies[0];
            target = newTarget;
        }
    }
}
