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
        }
    }

    // 游戏开始
    private void StartGame(FightRoomModel model) {
        // 加载模型
        fightRoom = model;

        // 加载队伍一的模型
        foreach (var fightModel in fightRoom.teamOne) {
            GameObject obj;
            if (fightModel.type == ModelType.HUMAN) {
                // 加载人物模型
                obj = Instantiate(Resources.Load<GameObject>("Prefabs/Human/" + fightModel.code), blueHeroBirthplace.position, Quaternion.identity);
            } else {
                // 加载建筑模型
                obj = Instantiate(Resources.Load<GameObject>("Prefabs/Build/1_" + fightModel.code), blueTowerPositions[fightModel.code - 1].position, Quaternion.identity);
            }
        }

        foreach (var fightModel in fightRoom.teamTwo) {
            GameObject obj;
            if (fightModel.type == ModelType.HUMAN) {
                obj = Instantiate(Resources.Load<GameObject>("Prefabs/Human/" + fightModel.code), redHeroBirthplace.position, Quaternion.identity);
            } else {
                obj = Instantiate(Resources.Load<GameObject>("Prefabs/Build/2_" + fightModel.code), redTowerPositions[fightModel.code - 1].position, Quaternion.identity);
            }
        }
    }
}
