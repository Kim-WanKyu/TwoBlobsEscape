using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 문 클래스.
public class Door : MonoBehaviour
{
    private Collider2D doorCollider2D;                  // 문 오브젝트 콜라이더.
    private SpriteRenderer doorSpriteRenderer;          // 문 오브젝트의 스프라이트렌더러.
    [SerializeField] private Sprite openedDoorSprite;   // 열린 문 스프라이트.
    [SerializeField] private Sprite closedDoorSprite;   // 닫힌 문 스프라이트.

    [SerializeField] private bool isOpened; // 문이 열려있는지 저장하는 변수. 인스펙터에서 초기값 지정.

    private void Awake()
    {
        // 오브젝트의 콜라이더와 스프라이트렌더러 가져옴.
        doorCollider2D = GetComponent<Collider2D>();
        doorSpriteRenderer = GetComponent<SpriteRenderer>();

        // 변수 초기값에 따라 문의 상태를 설정.
        if (isOpened == true)   // 변수 상태가 true이면, 열어줌.
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    // 문을 여는 메소드.
    public void OpenDoor()
    {
        isOpened = true;
        doorCollider2D.enabled = false; // 지나갈 수 있도록 콜라이더 꺼주고,
        doorSpriteRenderer.sprite = openedDoorSprite;   // 스프라이트를 열린 문 이미지로.
    }
    
    // 문을 닫는 메소드.
    public void CloseDoor()
    {
        isOpened = false;
        doorCollider2D.enabled = true; // 지나갈 수 없도록 콜라이더 켜주고,
        doorSpriteRenderer.sprite = closedDoorSprite;   // 스프라이트를 닫힌 문 이미지로.
    }

    // 문이 열려있는지 확인하는 메소드.
    public bool IsOpenedDoor()
    {
        return (isOpened == true); // 열려있으면 true 반환.
    }

}
