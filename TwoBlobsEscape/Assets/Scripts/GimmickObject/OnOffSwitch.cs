using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnOffSwitch : SwitchObject
{
    private SpriteRenderer switchSpriteRenderer;        // 스위치 오브젝트의 스프라이트렌더러.
    [SerializeField] private Sprite onSwitchSprite;     // 켜진 스위치 스프라이트.
    [SerializeField] private Sprite offSwitchSprite;    // 꺼진 스위치 스프라이트.

    private void Awake()
    {
        // 스위치 오브젝트의 스프라이트렌더러 가져옴.
        switchSpriteRenderer = GetComponent<SpriteRenderer>();
        switchSpriteRenderer.sprite = offSwitchSprite;  // 꺼진 스위치 스프라이트로 초기화.
    }

    public override void ControlSwitch()
    {
        // 스위치 스프라이트가 꺼져있으면 켜고, 
        if (switchSpriteRenderer.sprite == offSwitchSprite)
        {
            switchSpriteRenderer.sprite = onSwitchSprite;
        }
        else
        {
            // 스위치 스프라이트가 켜져있으면 끔. 
            switchSpriteRenderer.sprite = offSwitchSprite;
        }

        ControlOnOffSwitch();
    }

    abstract public void ControlOnOffSwitch();  // 스위치 켜고 끄는 추상 메소드.
}
