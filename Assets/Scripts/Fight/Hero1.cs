using GameProtocol;
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

    public override void BaseSkill(int code, Transform[] target, Vector3 ps) {
        //SkillAtkModel atk = new SkillAtkModel();
        //switch (code) {
        //    case 1:
        //        atk.skill = code;
        //        atk.position = new float[] { ps.x, ps.y, ps.z };
        //        atk.type = 1;
        //        this.WriteMessage(Protocol.TYPE_FIGHT, 0, FightProtocol.SKILL_CREQ, atk);
        //        break;
        //    case 2:
        //        break;
        //    case 3:
        //        break;
        //    case 4:
        //        break;
        //}
        
    }
}
