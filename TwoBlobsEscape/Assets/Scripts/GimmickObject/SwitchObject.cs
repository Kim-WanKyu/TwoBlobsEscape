using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// (조작가능한 오브젝트인 스위치)의 상위 추상 클래스.
public abstract class SwitchObject : MonoBehaviour, IControlObject
{
    public void ControlObject()
    {
        ControlSwitch();    // 스위치를 조작하는 메소드 실행.
    }

    abstract public void ControlSwitch();   // 스위치 조작 추상 메소드.
}
