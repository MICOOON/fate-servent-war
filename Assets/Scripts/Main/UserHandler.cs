using GameProtocol;
using GameProtocol.dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserHandler : MonoBehaviour, IHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MessageReceive(SocketModel model) {
        switch(model.command) {
            case UserProtocol.INFO_SRES:
                Info(model.GetMessage<UserDTO>());
                break;
            case UserProtocol.CREATE_SRES:
                Create(model.GetMessage<bool>());
                break;
            case UserProtocol.ONLINE_SRES:
                Online(model.GetMessage<UserDTO>());
                break;
        }
    }

    private void Info(UserDTO user) {
        if (user == null) {
            // 显示创建面板
            SendMessage("OpenCreateWindow");
        } else {
            this.WriteMessage(Protocol.TYPE_USER, 0, UserProtocol.ONLINE_CREQ, null);
        }
    }

    private void Create(bool value) {
        if (value) {
            // 英雄创建失败
            WarningManager.errors.Add("创建成功");
            SendMessage("CloseCreateWindow");
            this.WriteMessage(Protocol.TYPE_USER, 0, UserProtocol.ONLINE_CREQ, null);
        } else {
            WarningManager.errors.Add("创建失败");
            SendMessage("OpenCreateWindow");
        }
    }

    private void Online(UserDTO user) {
        GameData.user = user;
        SendMessage("RefreshView");
        WarningManager.errors.Add("登陆成功");
    }
}
