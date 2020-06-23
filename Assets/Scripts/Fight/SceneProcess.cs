using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SceneProcess : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = Input.mousePosition;
        if (position.x < 10) {
            // 通知相机向左移动
            FightManager.instance.CameraHMove(-1);
        } else if (position.x > Screen.width - 10) {
            // 通知相机向右移动
            FightManager.instance.CameraHMove(1);
        } else {
            // 不移动
            FightManager.instance.CameraHMove(0);
        }
        if (position.y < 10) {
            // 通知相机向下移动
            FightManager.instance.CameraVMove(-1);
        } else if (position.y > Screen.height - 10) {
            // 通知相机向上移动
            FightManager.instance.CameraVMove(1);
        } else {
            // 不移动
            FightManager.instance.CameraVMove(0);
        }
        if (Input.GetKey(KeyCode.Space)) {

        }
    }

    // 鼠标点击玩家移动
    public void OnPointerClick(PointerEventData eventData) {
        switch (eventData.pointerId) {
            case PointerInputModule.kMouseRightId:
                FightManager.instance.RightClick(eventData.position);
                break;
        }
    }
}
