using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroGrid : MonoBehaviour
{
    [SerializeField]
    private Button btn;

    [SerializeField]
    private Image img;

    private int id = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(int id) {
        this.id = id;
        img.sprite = Resources.Load<Sprite>("HeroIcon/" + id);
    }

    public void Active() {
        btn.interactable = true;
    }

    public void DeActive() {
        btn.interactable = false;
    }

    public void Click() {
        if (id != -1) {
            // 处理点击事件
            SelectEventUtil.selectHero(id);
        }
    }
}
