using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierMove : MonoBehaviour
{
    private NavMeshAgent nav;

    private Animation ani;

    public Transform target;

    public Transform[] towers;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animation>();
        target = FindTarget();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move() {
        if (target == null)
        {
            target = FindTarget();
            return;
        }
        ani.CrossFade("Run");
        nav.SetDestination(target.position);
    }

    Transform FindTarget() {
        for (int i = 0; i < towers.Length; i++)
        {
            if (towers[i] != null)
            {
                return towers[i];
            }
        }
        return null;
    }
}
