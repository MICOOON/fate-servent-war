using GameProtocol.dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void RefreshView(SelectRoomDTO room);

public delegate void SelectHero(int heroId);

public delegate void ReadySelect();

public class SelectEventUtil
{
    public static RefreshView refreshView;

    public static SelectHero selectHero;

    public static ReadySelect readySelect;
}
