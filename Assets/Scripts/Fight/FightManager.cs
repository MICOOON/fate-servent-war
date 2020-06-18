using GameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    // 单例模式
    public static FightManager instance;

    // 主相机
    private Camera cameraMain;

    // 相机水平方向
    private int cameraH;

    // 相机垂直方向
    private int cameraV;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
         cameraMain = Camera.main;
        // 加载场景
        this.WriteMessage(Protocol.TYPE_FIGHT, 0, FightProtocol.ENTER_CREQ, null);
    }

    // Update is called once per frame
    void Update()
    {
        switch (cameraH) {
            case 1:
                if (cameraMain.transform.position.x <= 70) {
                    cameraMain.transform.position = new Vector3(cameraMain.transform.position.x + cameraH, cameraMain.transform.position.y, cameraMain.transform.position.z);
                }
                break;
            case -1:
                if (cameraMain.transform.position.x >= -60) {
                    cameraMain.transform.position = new Vector3(cameraMain.transform.position.x + cameraH, cameraMain.transform.position.y, cameraMain.transform.position.z);
                }
                break;
        }
        switch (cameraV) {
            case 1:
                if (cameraMain.transform.position.z <= 30) {
                    cameraMain.transform.position = new Vector3(cameraMain.transform.position.x, cameraMain.transform.position.y, cameraMain.transform.position.z + cameraV);
                }
                break;
            case -1:
                if (cameraMain.transform.position.z >= -110) {
                    cameraMain.transform.position = new Vector3(cameraMain.transform.position.x, cameraMain.transform.position.y, cameraMain.transform.position.z + cameraV);
                }
                break;
        }
    }

    // 相机横移, 向右传1, 向左传-1
    public void CameraHMove(int dir) {
        if (cameraH != dir) {
            cameraH = dir;
        }
    }

    // 相机纵移, 向上传1, 向下传-1
    public void CameraVMove(int dir) {
        if (cameraV != dir) {
            cameraV = dir;
        }
    }
}
