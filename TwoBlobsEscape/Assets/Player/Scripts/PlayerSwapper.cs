using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSwapper : MonoBehaviour
{
    [SerializeField] private GameObject playerBlue; // 파랑이.
    [SerializeField] private GameObject playerPink; // 분홍이.

    private GameObject nowPlayer;   // 현재 플레이어.
    public GameObject GetNowPlayer() { return nowPlayer; }

    // 플레이어의 활성화/비활성화 된 색
    private Color activatedColor;
    private Color deactivatedColor;

    void Awake()
    {
        // 플레이어의 활성화/비활성화 된 색 초기화.
        activatedColor = new Color(1, 1, 1);    // 컬러는 0~1이므로, (1,1,1)는 흰색.
        deactivatedColor = new Color(100 / 255f, 100 / 255f, 100 / 255f); // 0~255를 255로 나누어 0~1로 정규화. (회색).

        // 각 플레이어의 색을 초기화. (파랑이를 활성화 / 분홍이를 비활성화 된 색으로).
        playerBlue.GetComponent<SpriteRenderer>().color = activatedColor;
        playerPink.GetComponent<SpriteRenderer>().color = deactivatedColor;

        // 현재 플레이어를 파랑이로 설정하고, 값들을 초기화.
        nowPlayer = playerBlue;
        nowPlayer.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }

    void Update()
    {
        // C키(플레이어 변경 키) 누르면,
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.SWAP]))
        {
            SwapPlayer();
        }
    }

    // 플레이어 스왑(파랑이 <-> 분홍이).
    void SwapPlayer()
    {
        // 이전 플레이어의 속도를 0으로 설정.
        nowPlayer.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        // 이전 플레이어의 애니메이션들 종료.
        nowPlayer.GetComponent<Animator>().SetBool("isWalking", false); // 걷기.
        nowPlayer.GetComponent<Animator>().SetBool("isJumping", false); // 점프.
        // 이전 플레이어의 색을 비활성화된 색으로 변경.
        nowPlayer.GetComponent<SpriteRenderer>().color = deactivatedColor;
        // 이전 플레이어의 Order in Layer를 0으로 변경(이전 플레이어가 현재 플레이어의 뒤로 가도록).
        nowPlayer.GetComponent<SpriteRenderer>().sortingOrder = 0;

        // 플레이어 변경.
        nowPlayer = ((nowPlayer == playerBlue) ? playerPink : playerBlue);

        // 현재 플레이어의 색을 활성화된 색으로 변경.
        nowPlayer.GetComponent<SpriteRenderer>().color = activatedColor;
        // 현재 플레이어의 Order in Layer를 1으로 변경(현재 플레이어가 이전 플레이어의 앞으로 가도록).
        nowPlayer.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }
}
