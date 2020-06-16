using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IOExtend {
    public static void WriteMessage(this MonoBehaviour monoBehaviour, byte type, int area, int command, object message) {
        NetIO.Instance.Write(type, area, command, message);
    }
}
