using GameProtocol;
using GameProtocol.constans;
using GameProtocol.dto.fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightManager : MonoBehaviour
{
    // 单例模式
    public static FightManager instance;

    // 主相机
    private Camera cameraMain;

    // 相机水平方向
    private int cameraH;

    // 相机垂直方向
    private int cameraV;

    public PlayerCon myHero;

    // 英雄头像
    [SerializeField]
    private Image head;

    // 英雄名称
    [SerializeField]
    private Text nameText;

    // 英雄等级
    [SerializeField]
    private Text levelText;

    [SerializeField]
    private SkillGrid[] skills;

    // 当前英雄是否死亡
    public bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        cameraMain = Camera.main;
        // 加载场景
        this.WriteMessage(Protocol.TYPE_FIGHT, 0, FightProtocol.ENTER_CREQ, null);
    }

    // Update is called once per frame
    void Update()
    {
        switch (cameraH) {
            case 1:
                if (cameraMain.transform.position.x <= 70) {
                    cameraMain.transform.position = new Vector3(cameraMain.transform.position.x + cameraH, cameraMain.transform.position.y, cameraMain.transform.position.z);
                }
                break;
            case -1:
                if (cameraMain.transform.position.x >= -60) {
                    cameraMain.transform.position = new Vector3(cameraMain.transform.position.x + cameraH, cameraMain.transform.position.y, cameraMain.transform.position.z);
                }
                break;
        }
        switch (cameraV) {
            case 1:
                if (cameraMain.transform.position.z <= 30) {
                    cameraMain.transform.position = new Vector3(cameraMain.transform.position.x, cameraMain.transform.position.y, cameraMain.transform.position.z + cameraV);
                }
                break;
            case -1:
                if (cameraMain.transform.position.z >= -110) {
                    cameraMain.transform.position = new Vector3(cameraMain.transform.position.x, cameraMain.transform.position.y, cameraMain.transform.position.z + cameraV);
                }
                break;
        }
    }

    // 初始化界面
    public void InitView(FightPlayerModel model, PlayerCon hero) {
        myHero = hero;
        // 初始化英雄头像面板
        head.sprite = Resources.Load<Sprite>("HeroIcon/" + model.code);
        nameText.text = HeroData.heroMap[model.code].name;
        levelText.text = model.level.ToString();
        // 技能初始化
        for (int i = 0; i < model.skills.Length; i++) {
            skills[i].Init(model.skills[i]);
        }
    }

    // 刷新界面
    public void RefreshView(FightPlayerModel model) {
        levelText.text = model.level.ToString();
    }

    public void LookAt() {
        cameraMain.transform.position = myHero.transform.position + new Vector3(2, 14, -10);
    }

    // 相机横移, 向右传1, 向左传-1
    public void CameraHMove(int dir) {
        if (cameraH != dir) {
            cameraH = dir;
        }
    }

    // 相机纵移, 向上传1, 向下传-1
    public void CameraVMove(int dir) {
        if (cameraV != dir) {
            cameraV = dir;
        }
    }

    public void RightClick(Vector2 position) {
        Ray ray = cameraMain.ScreenPointToRay(position);
        RaycastHit[] hits = Physics.RaycastAll(ray, 200);

        for (int i = 0; i < hits.Length; i++) {
            RaycastHit item = hits[i];
            // 如果是敌方单位, 则进行普通攻击.
            if (item.transform.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
                PlayerCon con = item.transform.gameObject.GetComponent<PlayerCon>();
                if (Vector3.Distance(myHero.transform.position, item.transform.position) < con.data.aRange) {
                    this.WriteMessage(Protocol.TYPE_FIGHT, 0, FightProtocol.ATTACK_CREQ, con.data.id);
                }
            }
            // 如果是己方单位, 则无视
            // 如果是地板层, 则开始寻路
            if (item.transform.gameObject.layer == LayerMask.NameToLayer("Ground")) {
                MoveDTO dto = new MoveDTO();
                dto.x = item.point.x;
                dto.y = item.point.y;
                dto.z = item.point.z;

                this.WriteMessage(Protocol.TYPE_FIGHT, 0, FightProtocol.MOVE_CREQ, dto);
                return;
            }
        }
    }
}
