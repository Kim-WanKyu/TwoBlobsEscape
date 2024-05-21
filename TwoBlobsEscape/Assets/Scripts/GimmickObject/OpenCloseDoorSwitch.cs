using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 문을 열고닫는 스위치 클래스.
public class OpenCloseDoorSwitch : SwitchObject
{
    // 이 버튼이 열고 닫을 문 오브젝트들의 정보들과 메소드 들어있음.
    [SerializeField] private Door[] doors;

    public override void ControlSwitch()
    {
        OpenCloseDoor();
    }

    // 문을 열고 닫는 메소드.
    public void OpenCloseDoor()
    {
        if (doors != null)
        {
            // 스위치가 열고 닫을 모든 문들을 확인하며,
            foreach (Door door in doors)
            {
                // 해당 문이 열려있으면,
                if (door.IsOpenedDoor() == true)
                {
                    Debug.Log("문 닫음");
                    door.CloseDoor();   // 문을 닫음.
                }
                else
                {
                    Debug.Log("문 열음");
                    door.OpenDoor();    // 아니면, 문을 열음.
                }
            }
        }
    }

}
