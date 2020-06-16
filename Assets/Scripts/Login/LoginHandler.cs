using GameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginHandler : MonoBehaviour, IHandler {
    public void MessageReceive(SocketModel model) {
        switch (model.command) {
            case LoginProtocol.LOGIN_SRES:
                Login(model.GetMessage<int>());
                break;
            case LoginProtocol.REG_SRES:
                Register(model.GetMessage<int>());
                break;
        }
    }

    private void Login(int value) {
        switch (value) {
            case 0:
                Application.LoadLevel(1);
                break;
            case -1:
                WarningManager.errors.Add("账号不存在");
                break;
            case -2:
                WarningManager.errors.Add("账号在线");
                break;
            case -3:
                WarningManager.errors.Add("密码错误");
                break;
            case -4:
                WarningManager.errors.Add("输入不合法");
                break;
        }
    }

    private void Register(int value) {
        switch (value) {
            case 0:
                WarningManager.errors.Add("注册成功");
                break;
            case 1:
                WarningManager.errors.Add("注册失败, 账号重复");
                break;
        }
    }
}
