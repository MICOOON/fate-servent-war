using GameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    [SerializeField]
    private Text matchText;

    [SerializeField]
    private Text nameText;

    [SerializeField]
    private CreateWindow createWindow;

    // Start is called before the first frame update
    void Start()
    {
        if (GameData.user == null) {
            NetIO.Instance.Write(Protocol.TYPE_USER, 0, UserProtocol.INFO_CREQ, null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OpenCreateWindow() {
        createWindow.Open();
    }

    private void CloseCreateWindow() {
        createWindow.Close();
    }

    // 匹配的逻辑
    public void Match() {
        if (matchText.text == "开始排队") {
            // 如果是开始排队
            matchText.text = "取消排队";
            NetIO.Instance.Write(Protocol.TYPE_MATCH, 0, MatchProtocol.ENTER_CREQ, null);
        } else {
            // 如果是排队中
            matchText.text = "开始排队";
            NetIO.Instance.Write(Protocol.TYPE_MATCH, 0, MatchProtocol.LEAVE_CREQ, null);
        }
    }

    // 刷新的逻辑
    public void RefreshView() {
        nameText.text = GameData.user.name + "等级" + GameData.user.level;
    }
}
