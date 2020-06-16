using GameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchHandler : MonoBehaviour, IHandler
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
        switch (model.command) {
            case MatchProtocol.ENTER_SELECT_BRO:
                Application.LoadLevel(2);
                break;
        }
    }
}
