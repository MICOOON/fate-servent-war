using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningManager : MonoBehaviour
{
    public static List<string> errors = new List<string>();

    // 警告面板
    [SerializeField]
    private WarningWindow warningWindow;

    // Update is called once per frame
    void Update()
    {
        if (errors.Count > 0) {
            warningWindow.ShowWarningMsg(errors[0]);
            errors.RemoveAt(0);
        }
    }
}
