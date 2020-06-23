using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionState : MonoBehaviour {
    // 动画状态表
    // 0:待机 1:跑 2:攻击 3:技能 4:死亡
    public const int IDLE = 0;
    public const int RUN = 1;
    public const int ATTACK = 2;
    public const int SKILL = 3;
    public const int DEAD = 4;
}
