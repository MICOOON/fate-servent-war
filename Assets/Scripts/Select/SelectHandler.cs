using GameProtocol;
using GameProtocol.dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectHandler : MonoBehaviour, IHandler
{
    private SelectRoomDTO room;

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
            case SelectProtocol.DESTORY_BRO:
                break;
            case SelectProtocol.ENTER_SRES:
                Enter(model.GetMessage<SelectRoomDTO>());
                break;
            case SelectProtocol.ENTER_EXBRO:
                Enter(model.GetMessage<int>());
                break;
            case SelectProtocol.READY_BRO:
                Ready(model.GetMessage<SelectModel>());
                break;
            case SelectProtocol.FIGHT_BRO:
                break;
            case SelectProtocol.SELECT_BRO:
                Select(model.GetMessage<SelectModel>());
                break;
            case SelectProtocol.SELECT_SRES:
                break;
            case SelectProtocol.TALK_BRO:
                Talk(model.GetMessage<string>());
                break;
        }
    }

    private void Enter(SelectRoomDTO dto) {
        room = dto;
        // 刷新UI界面
        SelectEventUtil.refreshView(room);
    }

    private void Enter(int id) {
        if (room == null) {
            return;
        }

        foreach (var selectModel in room.teamOne) {
            if (selectModel.userId == id) {
                selectModel.enter = true;
                // 刷新UI界面
                SelectEventUtil.refreshView(room);
                return;
            }
        }

        foreach (var selectModel in room.teamTwo) {
            if (selectModel.userId == id ) {
                selectModel.enter = true;
                //刷新UI界面
                SelectEventUtil.refreshView(room);
                return;
            }
        }
    }

    private void Select(SelectModel model) {
        if (room == null) {
            return;
        }

        foreach (var selectModel in room.teamOne) {
            if (selectModel.userId == model.userId) {
                selectModel.hero = model.hero;
                // 刷新UI界面
                SelectEventUtil.refreshView(room);
                return;
            }
        }

        foreach (var selectModel in room.teamTwo) {
            if (selectModel.userId == model.userId) {
                selectModel.hero = model.hero;
                //刷新UI界面
                SelectEventUtil.refreshView(room);
                return;
            }
        }
    }

    private void Ready(SelectModel model) {
        if (GameData.user.id == model.userId) {
            SelectEventUtil.readySelect();
        }

        foreach (var selectModel in room.teamOne) {
            if (selectModel.userId == model.userId) {
                selectModel.hero = model.hero;
                selectModel.ready = true;
                // 刷新UI界面
                SelectEventUtil.refreshView(room);
                return;
            }
        }

        foreach (var selectModel in room.teamTwo) {
            if (selectModel.userId == model.userId) {
                selectModel.hero = model.hero;
                selectModel.ready = true;
                //刷新UI界面
                SelectEventUtil.refreshView(room);
                return;
            }
        }
    }

    private void Talk(string value) {
        SendMessage("TalkMessage", value);
    }
}
