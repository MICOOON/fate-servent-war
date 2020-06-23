using GameProtocol.dto.fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCon : MonoBehaviour
{
    public FightPlayerModel data;

    protected Animator anim;
    protected NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 移动
    public void Move(Vector3 target) {
        agent.ResetPath();
        agent.SetDestination(target);
        anim.SetInteger("state", ActionState.RUN);
    }

    // 申请攻击 攻击的目标
    public virtual void Attack(Transform[] target) {

    }

    // 攻击动画播放结束回调方法
    public virtual void Attacked() {

    }

    // 服务器申请技能释放
    public virtual void Skill() {

    }

    // 技能释放回调方法
    public virtual void Skilled() {

    }
}
