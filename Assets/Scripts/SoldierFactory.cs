using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject soldier;

    [SerializeField]
    private Transform birthplace;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateSoldierGroup(0, 1, 5));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateSoldier() {
        Instantiate(soldier, birthplace.position, Quaternion.identity);
    }

    public IEnumerator CreateSoldierGroup(float startTime, float singleTime, float groupTime) {
        yield return new WaitForSeconds(startTime);

        while (true) {
            for (int i = 0; i < 3; i++)
            {
                CreateSoldier();

                yield return new WaitForSeconds(singleTime);
            }

            yield return new WaitForSeconds(groupTime);
        }
    }
}
