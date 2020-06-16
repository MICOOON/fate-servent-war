using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningWindow : MonoBehaviour
{
    [SerializeField]
    private Text msgPanel;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // 弹出警告信息
    public void ShowWarningMsg(string msg) {
        msgPanel.text = msg;
        OpenWarningWindow();
    }

    // 打开警告面板
    public void OpenWarningWindow() {
        this.gameObject.SetActive(true);
    }

    // 关闭警告面板
    public void CloseWarningWindow() {
        this.gameObject.SetActive(false);
    }
}
