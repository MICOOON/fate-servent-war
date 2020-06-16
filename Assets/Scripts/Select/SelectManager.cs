using GameProtocol;
using GameProtocol.dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour
{
    [SerializeField]
    private SelectGrid[] left;

    [SerializeField]
    private SelectGrid[] right;

    [SerializeField]
    private GameObject heroGridPrefab;

    [SerializeField]
    private Transform heroGridParent;

    [SerializeField]
    private Button readyButton;

    [SerializeField]
    private InputField talkInput;//聊天输入框

    [SerializeField]
    private Text talkShow;//聊天显示框

    [SerializeField]
    private Scrollbar talkBar; // 对话框滚动条

    private Dictionary<int, HeroGrid> gridMap = new Dictionary<int, HeroGrid>();

    // Start is called before the first frame update
    void Start()
    {
        SelectEventUtil.refreshView = RefreshView;
        SelectEventUtil.selectHero = SelectHero;
        SelectEventUtil.readySelect = Selected;
        // 初始化英雄列表
        InitHeroList();
        this.WriteMessage(Protocol.TYPE_SELECT, 0, SelectProtocol.ENTER_CREQ, null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RefreshView(SelectRoomDTO room) {
        int team = room.GetTeam(GameData.user.id);

        if (team == 1) {
            for (int i = 0; i < room.teamOne.Length; i++) {
                left[i].Refresh(room.teamOne[i]);
            }
            for (int i = 0; i < room.teamTwo.Length; i++) {
                right[i].Refresh(room.teamTwo[i]);
            }
        } else {
            for (int i = 0; i < room.teamTwo.Length; i++) {
                left[i].Refresh(room.teamTwo[i]);
            }
            for (int i = 0; i < room.teamOne.Length; i++) {
                right[i].Refresh(room.teamOne[i]);
            }
        }
        RefreshHeroList(room);
    }

    private void InitHeroList() {
        if (GameData.user == null) {
            return;
        }

        for (int i = 0; i < GameData.user.heroList.Length; i++) {
            GameObject btn = Instantiate(heroGridPrefab);
            // 设置父物体
            btn.transform.parent = heroGridParent;
            // 设置位置
            float x = 70 * (i % 8) - 245;
            float y = - 70 * (i / 8) + 70;
            Vector3 localPosition = new Vector3(x, y);
            btn.transform.localPosition = localPosition;

            HeroGrid grid = btn.GetComponent<HeroGrid>();
            grid.Init(GameData.user.heroList[i]);
            gridMap.Add(GameData.user.heroList[i], grid);
        }
    }

    public void RefreshHeroList(SelectRoomDTO room) {
        int team = room.GetTeam(GameData.user.id);
        List<int> selected = new List<int>();
        if (team == 1) {
            foreach (var item in room.teamOne) {
                if (item.hero != -1) {
                    selected.Add(item.hero);
                }
            }
        } else {
            foreach (var item in room.teamTwo) {
                if (item.hero != -1) {
                    selected.Add(item.hero);
                }
            }
        }
        foreach (var item in gridMap.Keys) {
            if (selected.Contains(item) || !readyButton.interactable) {
                gridMap[item].DeActive();
            } else {
                gridMap[item].Active();
            }
        }
    }

    private void SelectHero(int heroId) {
        this.WriteMessage(Protocol.TYPE_SELECT, 0, SelectProtocol.SELECT_CREQ, heroId);
    }

    public void ReadySelect() {
        this.WriteMessage(Protocol.TYPE_SELECT, 0, SelectProtocol.READY_CREQ, null);
    }

    private void Selected() {
        readyButton.interactable = false;
    }

    public void SendClick() {
        if (talkInput.text == string.Empty) {
            return;
        }
        this.WriteMessage(Protocol.TYPE_SELECT, 0, SelectProtocol.TALK_CREQ, talkInput.text);
        talkInput.text = string.Empty;
    }

    private void TalkMessage(string value) {
        talkShow.text += "\n" + value;
        talkBar.value = 0;
    }
}
