using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject soldierPrefab;

    [SerializeField]
    private Transform soldierParent;

    [SerializeField]
    private Transform[] blueBirthplace;

    [SerializeField]
    private Transform[] redBirthplace;

    [SerializeField]
    private Transform[] blueTopTowers;

    [SerializeField]
    private Transform[] blueMiddleTowers;

    [SerializeField]
    private Transform[] blueBottomTowers;

    [SerializeField]
    private Transform[] redTopTowers;

    [SerializeField]
    private Transform[] redMiddleTowers;

    [SerializeField]
    private Transform[] redBottomTowers;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateSoldierGroup(0, 1, 5));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateSoldier(SoldierType type, Transform birthplace, Transform[] towers, int road) {
        GameObject soldier = Instantiate(soldierPrefab, birthplace.position, Quaternion.identity);
        soldier.transform.parent = soldierParent;

        SoldierBean soldierBean = soldier.GetComponent<SoldierBean>();
        soldierBean.towers = towers;
        soldierBean.SetAreaMask(road);
        soldierBean.type = (int) type;
    }

    public IEnumerator CreateSoldierGroup(float startTime, float singleTime, float groupTime) {
        yield return new WaitForSeconds(startTime);

        while (true) {
            for (int i = 0; i < 3; i++)
            {
                CreateSoldier(SoldierType.BlueSoldier, blueBirthplace[0], redTopTowers, 1 << 3);
                CreateSoldier(SoldierType.BlueSoldier, blueBirthplace[1], redMiddleTowers, 1 << 4);
                CreateSoldier(SoldierType.BlueSoldier, blueBirthplace[2], redBottomTowers, 1 << 5);
                CreateSoldier(SoldierType.RedSoldier, redBirthplace[0], blueTopTowers, 1 << 3);
                CreateSoldier(SoldierType.RedSoldier, redBirthplace[1], blueMiddleTowers, 1 << 4);
                CreateSoldier(SoldierType.RedSoldier, redBirthplace[2], blueBottomTowers, 1 << 5);

                yield return new WaitForSeconds(singleTime);
            }

            yield return new WaitForSeconds(groupTime);
        }
    }
}
