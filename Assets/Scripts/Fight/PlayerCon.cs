using GameProtocol.dto.fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCon : MonoBehaviour
{
    [HideInInspector]
    public FightPlayerModel data;

    protected Animator anim;
    protected NavMeshAgent agent;

    // 角色头顶信息
    [SerializeField]
    private PlayerTitle title;

    protected int state = ActionState.IDLE;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void HpChange() {
        title.HpChange(1F * data.hp / data.maxMp);
    }

    // 移动
    public void Move(Vector3 target) {
        agent.ResetPath();
        agent.SetDestination(target);
        anim.SetInteger("state", ActionState.RUN);
        state = ActionState.RUN;
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

    // 本地操作, 申请技能释放
    public virtual void BaseSkill(int code, Transform[] target, Vector3 ps) {

    }

    public void Init(FightPlayerModel data, int myTeam) {
        this.data = data;
        title.Init(data, data.team == myTeam);
        if (data.team != myTeam) {
            this.gameObject.layer = LayerMask.NameToLayer("Enemy");
        } else {
            this.gameObject.layer = LayerMask.NameToLayer("Friend");
        }
    }
}
