using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 문을 열고닫는 버튼 클래스.
public class OpenCloseDoorButton : ButtonObject
{
    // 이 버튼이 열고 닫을 문 오브젝트들의 정보들과 메소드 들어있음.
    [SerializeField] private Door[] doors;

    private bool isPushed;  // 버튼이 눌렸는지.

    private void Start()
    {
        // 버튼 상태를 안 눌린 상태로 초기화.
        isPushed = false;
    }

    // 버튼 누름.
    public override void PushButton()
    {
        // 이 버튼이 눌리지 않았으면,
        if (isPushed == false)
        {
            Debug.Log("버튼을 누름");
            // 버튼을 누름.
            isPushed = true;
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

    // 버튼 누름 해제.
    public override void CancelButton()
    {
        // 이 버튼이 눌리고 있으면,
        if (isPushed == true)
        {
            Debug.Log("버튼을 똄");
            // 버튼을 뗌.
            isPushed = false;
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
