using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField] private GameObject playerBlue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 오브젝트의 태그가 플레이어이면, => 플레이어와 충돌 시.
        if (collision.CompareTag("Player") == true)
        {
            // 파랑이와 충돌 시.
            if (collision.name.CompareTo(playerBlue.name) == 0)
            {
                Debug.Log("파랑이 용암에 닿음 게임 종료");
                collision.transform.parent.GetComponent<PlayerController>().Die();
            }
        }
    }
}
