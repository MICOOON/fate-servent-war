using GameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSkill : MonoBehaviour
{
    // 不写访问修饰符, 默认就是 private
    Transform target;
    int skill;
    int userId;

    public void Init(Transform target, int skill, int userId) {
        this.target = target;
        this.skill = skill;
        this.userId = userId;
    }

    // Update is called once per frame
    void Update()
    {
        if (target) {
            transform.position = Vector3.Lerp(transform.position, target.position, 0.5F);
            if (Vector3.Distance(transform.position, target.position) < 0.3F) {
                // 向服务器发送消息, 伤害目标// TODO 服务端的DTO没有定义
                //DamageDTO dto = new DamageDTO();
                //dto.skill = skill;
                //dto.userId = userId;
                //dto.target = new int[][] { new int[] { target.GetComponent<PlayerCon>().data.id } };
                //this.WriteMessage(Protocol.TYPE_FIGHT, 0, FightProtocol.DAMAGE_CREQ, dto);
                Destroy(gameObject);
            }
        }
    }
}
