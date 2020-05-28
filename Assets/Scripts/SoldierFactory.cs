using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject soldierPrefab;

    [SerializeField]
    private Transform birthplace;

    [SerializeField]
    private Transform soldierParent;

    [SerializeField]
    private Transform[] towers;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateSoldierGroup(0, 1, 5));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateSoldier(Transform birthplace, Transform[] towers) {
        GameObject soldier = Instantiate(soldierPrefab, birthplace.position, Quaternion.identity);
        soldier.transform.parent = soldierParent;

        SoldierMove soldierMove = soldier.GetComponent<SoldierMove>();
        soldierMove.towers = towers;
    }

    public IEnumerator CreateSoldierGroup(float startTime, float singleTime, float groupTime) {
        yield return new WaitForSeconds(startTime);

        while (true) {
            for (int i = 0; i < 3; i++)
            {
                CreateSoldier(birthplace, towers);

                yield return new WaitForSeconds(singleTime);
            }

            yield return new WaitForSeconds(groupTime);
        }
    }
}
