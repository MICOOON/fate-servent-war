using GameProtocol.dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectGrid : MonoBehaviour
{
    [SerializeField]
    private Text nameText;

    [SerializeField]
    private Image head;

    [SerializeField]
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Refresh(SelectModel model) {
        nameText.text = model.name;

        if (model.enter) {
            if (model.hero == -1) {
                head.sprite = Resources.Load<Sprite>("HeroIcon/notenter");
            } else {
                head.sprite = Resources.Load<Sprite>("HeroIcon/" + model.hero);
            }
        } else {
            head.sprite = Resources.Load<Sprite>("HeroIcon/notenter");
        }

        if (model.ready) {
            image.color = Color.red;
        } else {
            image.color = Color.white;
        }
    }
}
