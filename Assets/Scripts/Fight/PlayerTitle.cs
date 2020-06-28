using GameProtocol.constans;
using GameProtocol.dto.fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTitle : MonoBehaviour
{
    [SerializeField]
    private TextMesh nameText;

    [SerializeField]
    private HpScript hp;

    [SerializeField]
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(FightPlayerModel model, bool friend) {
        nameText.text = model.name;
        hp.HpValue = model.hp / model.maxHp;
        if (friend) {
            sr.color = new Color(255, 255, 255, 255);
        } else {
            sr.color = new Color(255, 255, 255, 10);
        }
    }

    public void HpChange(float value) {
        hp.HpValue = value;
    }
}
