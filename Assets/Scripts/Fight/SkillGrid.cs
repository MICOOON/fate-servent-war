﻿using GameProtocol;
using GameProtocol.dto.fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillGrid : MonoBehaviour
{
    [SerializeField]
    private Image mask; // 技能冷却遮罩

    [SerializeField]
    private Image backGround; // 技能图片

    private FightSkill skill;

    private bool sclied = false; // 技能遮罩是否正在冷却旋转中

    private float maxTime; // 技能冷却时间

    private float nowTime; // 现在剩余的冷却时间

    [SerializeField]
    private Image skillIntroduce;

    [SerializeField]
    private Button levelUpBtn;

    // 初始化技能图标
    public void Init(FightSkill skill) {
        this.skill = skill;
        Sprite sp = Resources.Load<Sprite>("SkillIcon/" + skill.code);
        backGround.sprite = sp;
        mask.gameObject.SetActive(true);
    }

    // 进行冷却
    public void SetMask(int time) {
        // 如果为0, 说明要取消冷却遮罩
        if (time == 0) {
            if (skill.level > 0 && !sclied) {
                mask.gameObject.SetActive(false);
                return;
            } else {
                mask.gameObject.SetActive(true);
                return;
            }
        }
        maxTime = time;
        nowTime = time;
        mask.gameObject.SetActive(true);
        sclied = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sclied) {
            nowTime -= Time.deltaTime;
            if (nowTime <= 0) {
                nowTime = 0;
                sclied = false;
                mask.gameObject.SetActive(false);
            }
            mask.fillAmount = nowTime / maxTime;
        }
    }

    // 获取焦点
    public void PointEnter() {
        // 打开介绍面板
        skillIntroduce.gameObject.SetActive(true);
    }

    // 失去焦点
    public void PointExit() {
        // 关闭介绍面板
        skillIntroduce.gameObject.SetActive(false);
    }

    public void SkillChange(FightSkill skill) {
        this.skill = skill;
    }

    public void SetBtnState(bool state) {
        levelUpBtn.interactable = state;
    }

    public void LevelUp() {
        // 向服务器发送消息, 申请技能升级
        this.WriteMessage(Protocol.TYPE_FIGHT, 0, FightProtocol.SKILL_UP_CREQ, skill.code);
    }

    // 鼠标左键点击技能指定
    public void PointClick() {
        if (nowTime > 0) {
            return;
        }
        FightManager.instance.skill = skill.code;
    }
}
