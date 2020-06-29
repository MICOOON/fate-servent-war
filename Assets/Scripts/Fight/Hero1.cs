using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero1 : PlayerCon {
    private Transform[] list;

    [SerializeField]
    private GameObject effect;

    public override void Attack(Transform[] target) {
        this.list = target;
        if (state == ActionState.RUN) {
            agent.CompleteOffMeshLink();
        }
        // 朝向
        transform.LookAt(target[0]);
        // 动画的转换
        state = ActionState.ATTACK;
        anim.SetInteger("state", ActionState.ATTACK);
    }

    public override void Attacked() {
        foreach (var item in list) {
            GameObject go = Instantiate(effect, transform.position, transform.rotation);
            // 粒子向敌方位移
            go.GetComponent<TargetSkill>().Init(list[0], -1, data.id);
            state = ActionState.IDLE;
            anim.SetInteger("state", ActionState.IDLE);
        }
    }
}
