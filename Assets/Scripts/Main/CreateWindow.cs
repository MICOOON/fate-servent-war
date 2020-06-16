using GameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateWindow : MonoBehaviour
{
    [SerializeField]
    private InputField inputField;

    [SerializeField]
    private Button button;

    private void Start() {
        Close();
    }

    public void Click() {
        if (inputField.text == string.Empty || inputField.text.Length > 6) {
            WarningManager.errors.Add("请输入正确的昵称");
            return;
        }

        button.enabled = false;

        NetIO.Instance.Write(Protocol.TYPE_USER, 0, UserProtocol.CREATE_CREQ, inputField.text);
    }

    public void Open() {
        gameObject.SetActive(true);
        button.enabled = true;
    }

    public void Close() {
        gameObject.SetActive(false);
    }
}
