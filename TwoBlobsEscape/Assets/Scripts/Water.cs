using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private GameObject playerPink;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 오브젝트의 태그가 플레이어이면, => 플레이어와 충돌 시.
        if (collision.CompareTag("Player") == true)
        {
            // 분홍이와 충돌 시.
            if (collision.name.CompareTo(playerPink.name) == 0)
            {
                Debug.Log("분홍이 물에 닿음 게임 종료");
                collision.transform.parent.GetComponent<PlayerController>().Die();
            }
        }
    }
}
