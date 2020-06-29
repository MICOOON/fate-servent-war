using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameProtocol;
using GameProtocol.dto.fight;

public class FightHandler : MonoBehaviour, IHandler
{
    [SerializeField]
    private Transform[] blueTowerPositions; // 蓝方防御塔位置

    [SerializeField]
    private Transform[] redTowerPositions; // 红方防御塔位置

    [SerializeField]
    private Transform blueHeroBirthplace; // 蓝方玩家起始位置

    [SerializeField]
    private Transform redHeroBirthplace; // 红方玩家起始位置

    private FightRoomModel fightRoom;

    private Dictionary<int, PlayerCon> models = new Dictionary<int, PlayerCon>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MessageReceive(SocketModel model) {
        switch (model.command) {
            case FightProtocol.START_BRO:
                StartGame(model.GetMessage<FightRoomModel>());
                break;
            case FightProtocol.MOVE_BRO:
                Move(model.GetMessage<MoveDTO>());
                break;
            case FightProtocol.ATTACK_BRO:
                //Attack(model.GetMessage<AttackDTO>());
                break;
            case FightProtocol.DAMAGE_BRO:
                //Damage(model.GetMessage<DamegeDTO>());
                break;
        }
    }

    public void Damage(DamageDTO value) {
        // int[]中第一个是id号, 第二个指的是伤害值, 第三个指的是是否死亡, 0代表死亡, 1代表没有死亡
        foreach (int[] item in value.target) {
            // 获取敌方模型
            PlayerCon pc = models[item[0]];
            pc.data.hp -= item[1];
            // 实例化掉血数字
            pc.HpChange();
            // 刷新界面
            if (pc.data.id == GameData.user.id) {
                FightManager.instance.RefreshView(pc.data);
            }
            // 判断是否死亡
            if (item[2] == 0) {
                // 击杀的是英雄或者小兵
                if (item[0] >= 0) {
                    pc.gameObject.SetActive(false);
                    if (pc.data.id == GameData.user.id) {
                        FightManager.instance.dead = true;
                    }
                } else {
                    Destroy(pc.gameObject);
                }
            }
        }
    }

    // 游戏开始
    // 进入场景后加载模型
    private void StartGame(FightRoomModel model) {
        // 加载模型
        fightRoom = model;
        int myTeam = -1;
        foreach (var item in model.teamOne) {
            if (item.id == GameData.user.id) {
                myTeam = item.team;
            }
        }
        if (myTeam == -1) {
            foreach (var item in model.teamTwo) {
                if (item.id == GameData.user.id) {
                    myTeam = item.team;
                }
            }
        }

        // 加载队伍一的模型
        foreach (var fightModel in fightRoom.teamOne) {
            GameObject obj;
            PlayerCon pc;
            if (fightModel.type == ModelType.HUMAN) {
                // 加载人物模型
                obj = Instantiate(Resources.Load<GameObject>("Prefabs/Human/" + fightModel.code), blueHeroBirthplace.position, Quaternion.identity);
                pc = obj.GetComponent<PlayerCon>();
                // 人物初始化
                pc.Init((FightPlayerModel) fightModel, myTeam);
            } else {
                // 加载建筑模型
                obj = Instantiate(Resources.Load<GameObject>("Prefabs/Build/1_" + fightModel.code), blueTowerPositions[fightModel.code - 1].position, Quaternion.identity);
                pc = obj.GetComponent<PlayerCon>();
            }
            this.models.Add(fightModel.id, pc);

            if (fightModel.id == GameData.user.id) {
                FightManager.instance.InitView((FightPlayerModel) fightModel, pc);
            }
        }

        foreach (var fightModel in fightRoom.teamTwo) {
            GameObject obj;
            PlayerCon pc;
            if (fightModel.type == ModelType.HUMAN) {
                obj = Instantiate(Resources.Load<GameObject>("Prefabs/Human/" + fightModel.code), redHeroBirthplace.position, Quaternion.identity);
                pc = obj.GetComponent<PlayerCon>();
                pc.Init((FightPlayerModel)fightModel, myTeam);
            } else {
                obj = Instantiate(Resources.Load<GameObject>("Prefabs/Build/2_" + fightModel.code), redTowerPositions[fightModel.code - 1].position, Quaternion.identity);
                pc = obj.GetComponent<PlayerCon>();
            }
            this.models.Add(fightModel.id, pc);

            if (fightModel.id == GameData.user.id) {
                FightManager.instance.InitView((FightPlayerModel)fightModel, pc);
            }
        }
    }

    // 移动
    public void Move(MoveDTO value) {
        Vector3 target = new Vector3(value.x, value.y, value.z);
        models[value.userId].SendMessage("Move", target);
    }

    // 普通攻击
    //public void Attack(AttackDTO dto) {
    //    PlayerCon obj = models[dto.userId];
    //    PlayerCon target = models[dto.targetId];
    //    // 调用攻击的方法
    //    obj.Attack(new Transform[] {target.transform});
    //}
}
