using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandler {
    void MessageReceive(SocketModel model);
}
