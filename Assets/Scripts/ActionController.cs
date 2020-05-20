using UnityEngine;
using System.Collections;

public class ActionController : MonoBehaviour {
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        EasyJoystick.On_JoystickMove += OnJoystickMove;
        EasyJoystick.On_JoystickMoveEnd += OnJoystickMoveEnd;
    }

    void OnDisable()
    {
        EasyJoystick.On_JoystickMove -= OnJoystickMove;
        EasyJoystick.On_JoystickMoveEnd -= OnJoystickMoveEnd;
    }

    void OnDestroy()
    {
        EasyJoystick.On_JoystickMove -= OnJoystickMove;
        EasyJoystick.On_JoystickMoveEnd -= OnJoystickMoveEnd;
    }


    void OnJoystickMoveEnd(MovingJoystick move)
    {
        if (move.joystickName == "MoveJoystick")
        {
            //GetComponent<Animation>().CrossFade("idle");
            animator.SetInteger("state", ActionState.IDLE);
        }
    }
    void OnJoystickMove(MovingJoystick move)
    {
        if (move.joystickName != "MoveJoystick")
        {
            return;
        }
        

        float joyPositionX = move.joystickAxis.x;
        float joyPositionY = move.joystickAxis.y;

        if (joyPositionY != 0 || joyPositionX != 0)
        {
            //设置角色的朝向（朝向当前坐标+摇杆偏移量）
           transform.LookAt(new Vector3(transform.position.x + joyPositionX, transform.position.y, transform.position.z + joyPositionY));
            //移动玩家的位置（按朝向位置移动）
            transform.Translate(Vector3.forward * Time.deltaTime * 5);
            //播放奔跑动画
            //GetComponent<Animation>().CrossFade("run");
            animator.SetInteger("state", ActionState.RUN);
        }
    }

    /**
     * 切换到站立状态
     */
    public void OnIdle()
    {
        animator.SetInteger("state", ActionState.IDLE);
    }

    /**
     * 释放技能
     */
    public void OnSkill()
    {
        animator.SetInteger("state", ActionState.SKILL);
    }
}
