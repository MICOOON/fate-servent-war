using GameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetMessageUtil : MonoBehaviour
{
    IHandler login;
    IHandler user;
    IHandler match;
    IHandler select;
    IHandler fight;

    // Start is called before the first frame update
    void Start()
    {
        login = GetComponent<LoginHandler>();
        user = GetComponent<UserHandler>();
        match = GetComponent<MatchHandler>();
        select = GetComponent<SelectHandler>();
        fight = GetComponent<FightHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        while (NetIO.Instance.models.Count > 0) {
            SocketModel model = NetIO.Instance.models[0];
            // 处理
            StartCoroutine("MessageReceive", model);
            NetIO.Instance.models.RemoveAt(0);
        }
    }

    private void MessageReceive(SocketModel model) {
        switch (model.type) {
            case Protocol.TYPE_LOGIN:
                login.MessageReceive(model);
                break;
            case Protocol.TYPE_USER:
                user.MessageReceive(model);
                break;
            case Protocol.TYPE_MATCH:
                match.MessageReceive(model);
                break;
            case Protocol.TYPE_SELECT:
                select.MessageReceive(model);
                break;
            case Protocol.TYPE_FIGHT:
                fight.MessageReceive(model);
                break;
        }
    }
}
