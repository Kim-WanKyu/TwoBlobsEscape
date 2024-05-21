using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// (조작가능한 오브젝트인 버튼)의 상위 추상 클래스. (이지만, IControlObject 상속 x => 상호작용을 통해 동작하는 것이 아니라, 플레이어와 충돌 시 동작하기 때문.)
public abstract class ButtonObject : MonoBehaviour
{
    private LinkedList<GameObject> collidedPlayers;  // 충돌한 플레이어들 리스트.

    // 처음에는 충돌한 플레이어들 리스트를 초기화.
    private void Awake()
    {
        collidedPlayers = new LinkedList<GameObject>();
    }

    // collider(trigger) 가 접촉되면,
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 오브젝트의 태그가 플레이어이면, => 플레이어와 충돌 시.
        if (collision.CompareTag("Player") == true)
        {
            // 충돌한 플레이어들 리스트에 충돌한 해당 플레이어 추가.
            collidedPlayers.AddLast(collision.gameObject);
        }

        // 충돌 중인 플레이어가 있다면, 버튼을 누름.
        if (collidedPlayers.Count > 0)
        {
            PushButton();   // 버튼 누름.
        }
    }

    // 플레이어 접촉 해제 시.
    private void OnTriggerExit2D(Collider2D collision)
    {
        // 충돌 해제된 오브젝트의 태그가 플레이어이면, => 플레이어와 충돌 해제 시.
        if (collision.CompareTag("Player") == true)
        {
            // 충돌 해제된 플레이어가 리스트에 있으면, 리스트에서 지움.
            if (collidedPlayers.Contains(collision.gameObject))
            {
                collidedPlayers.Remove(collision.gameObject);
            }

            // 충돌 중인 플레이어가 없다면, 버튼을 뗌.
            if (collidedPlayers.Count == 0)
            {
                CancelButton(); // 버튼 누름 해제.
            }
        }
    }

    // 버튼 누름.
    public abstract void PushButton();

    // 버튼 누름 해제.
    public abstract void CancelButton();

}
