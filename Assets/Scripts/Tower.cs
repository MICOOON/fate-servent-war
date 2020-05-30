using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int type;

    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.tag.Equals("BlueTower"))
        {
            type = (int) TowerType.BlueTower;
        }
        else if (this.gameObject.tag.Equals("RedTower"))
        {
            type = (int) TowerType.RedTower;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
