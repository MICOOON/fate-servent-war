using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    // 防御塔类型
    public int type;
    // 将被防御塔攻击的英雄队列
    public List<GameObject> beAttackedHeros;
    // 将被防御塔攻击的小兵队列
    public List<GameObject> beAttackedSoldiers;
    // 子弹预制体
    [SerializeField]
    private GameObject bulletPrefab;
    // 子弹生成位置
    [SerializeField]
    private Transform bulletBirthplace;
    // 子弹父物体
    [SerializeField]
    private Transform bulletParent;

    // Start is called before the first frame update
    void Start() {
        if (this.gameObject.tag.Equals(GameConsts.BLUE_TOWER))
        {
            type = (int) TowerType.BlueTower;
        }
        else if (this.gameObject.tag.Equals(GameConsts.RED_TOWER))
        {
            type = (int) TowerType.RedTower;
        }
        InvokeRepeating("CreateBullet", 0.1F, 1F);
    }

    // Update is called once per frame
    void Update() {
        
    }

    void CreateBullet() {
        if (beAttackedHeros.Count == 0 && beAttackedSoldiers.Count == 0) {
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, bulletBirthplace.position, Quaternion.identity);
        bullet.transform.parent = bulletParent;

        BulletBean bulletBean = bullet.GetComponent<BulletBean>();
        bulletBean.SetTarget(GetBulletTarget());
    }

    GameObject GetBulletTarget() {
        return beAttackedSoldiers.Count > 0 ? beAttackedSoldiers[0] : beAttackedHeros[0];
    }

    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag.Equals(GameConsts.PLAYER)) {
            beAttackedHeros.Add(collider.gameObject);
        } else {
            SoldierBean soldierBean = collider.GetComponent<SoldierBean>();
            if (soldierBean && this.type != soldierBean.type) {
                beAttackedSoldiers.Add(collider.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider collider) {
        if (collider.gameObject.tag.Equals(GameConsts.PLAYER)) {
            beAttackedHeros.Remove(collider.gameObject);
        } else {
            SoldierBean soldierBean = collider.GetComponent<SoldierBean>();
            if (this.type != soldierBean.type) {
                beAttackedSoldiers.Remove(collider.gameObject);
            }
        }
    }
}
