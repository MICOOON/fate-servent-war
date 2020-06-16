using GameProtocol;
using GameProtocol.dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour {
    // 登录面板
    [SerializeField]
    private InputField account;
    [SerializeField]
    private InputField password;
    [SerializeField]
    private Button loginBtn;
    // 注册面板
    [SerializeField]
    private InputField regAccount;
    [SerializeField]
    private InputField regPassword;
    [SerializeField]
    private InputField regRepeatPassword;
    [SerializeField]
    private Button registerBtn;
    [SerializeField]
    private GameObject registerPanel;
    [SerializeField]
    private GameObject gameManager;

    NetIO io;

    private void Start() {
        // 隐藏注册面板
        registerPanel.SetActive(false);
        io = NetIO.Instance;
    }

    // 点击登录
    public void LoginClick() {
        if (account.text.Length == 0 || account.text.Length > 6) {
            WarningManager.errors.Add("账号不合法");
            Debug.Log("账号不合法");
            return;
        }
        if (password.text.Length == 0 || password.text.Length > 6) {
            Debug.Log("密码不合法");
            return;
        }

        // 禁用登陆按钮
        //loginBtn.interactable = false;
        //Debug.Log("登陆成功");

        // 调用协议向服务器发起通信
        AccountInfoDTO dto = new AccountInfoDTO();
        dto.account = account.text;
        dto.password = password.text;
        this.WriteMessage(Protocol.TYPE_LOGIN, 0, LoginProtocol.LOGIN_CREQ, dto);
    }

    // 点击注册
    public void RegisterClick() {
        if (regAccount.text.Length == 0 || regAccount.text.Length > 6) {
            Debug.Log("账号不合法");
            return;
        }
        if (regPassword.text.Length == 0 || regPassword.text.Length > 6) {
            Debug.Log("密码不合法");
            return;
        }
        if (regPassword.text != regRepeatPassword.text) {
            Debug.Log("密码不一致");
            return;
        }

        // 禁用登陆按钮
        //registerBtn.interactable = false;
        //Debug.Log("注册成功");

        AccountInfoDTO dto = new AccountInfoDTO();
        dto.account = regAccount.text;
        dto.password = regPassword.text;
        // 发送请求
        io.Write(Protocol.TYPE_LOGIN, 0, LoginProtocol.REG_CREQ, dto);

        // 关闭面板
        CloseRegisterPanel();
    }

    // 打开注册面板
    public void OpenRegisterPanel() {
        registerPanel.SetActive(true);
    }

    // 关闭注册面板
    public void CloseRegisterPanel() {
        // 清空文本框
        regAccount.text = string.Empty;
        regPassword.text = string.Empty;
        regRepeatPassword.text = string.Empty;

        // 隐藏面板
        registerPanel.SetActive(false);
    }
}
