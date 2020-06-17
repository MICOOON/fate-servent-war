using GameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 加载场景
        this.WriteMessage(Protocol.TYPE_FIGHT, 0, FightProtocol.ENTER_CREQ, null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
