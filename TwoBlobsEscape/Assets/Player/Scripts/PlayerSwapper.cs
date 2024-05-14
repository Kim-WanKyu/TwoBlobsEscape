using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSwapper : MonoBehaviour
{
    [SerializeField] private GameObject playerBlue; // 파랑.
    [SerializeField] private GameObject playerPink; // 분홍.
    private GameObject nowPlayer;   // 현재 플레이어.

    public GameObject GetPlayerBlue() { return playerBlue; }
    public GameObject GetPlayerPink() { return playerPink; }
    public GameObject GetNowPlayer() { return nowPlayer; }

    // 플레이어의 활성화/비활성화 된 색
    private Color activatedColor;
    private Color deactivatedColor;


    void Awake()
    {
        // 플레이어의 활성화/비활성화 된 색 초기화.
        activatedColor = new Color(1, 1, 1);
        deactivatedColor = new Color(100 / 255f, 100 / 255f, 100 / 255f);

        // 각 플레이어의 색을 초기화. (파랑이를 활성화, 분홍이를 비활성화로).
        playerBlue.GetComponent<SpriteRenderer>().color = activatedColor;
        playerPink.GetComponent<SpriteRenderer>().color = deactivatedColor;

        // 현재 플레이어를 파랑이로 설정.
        nowPlayer = playerBlue;
    }

    void Update()
    {
        // C키(플레이어 변경 키) 누르면,
        if (Input.GetKeyDown(KeyCode.C))
        {
            // 이전 플레이어의 걷기 애니메이션 종료.
            nowPlayer.GetComponent<Animator>().SetBool("isWalking", false);
            // 이전 플레이어의 색을 비활성화된 색으로 변경.
            nowPlayer.GetComponent<SpriteRenderer>().color = deactivatedColor;

            // 플레이어 변경.
            nowPlayer = nowPlayer == playerBlue ? playerPink : playerBlue;

            // 현재 플레이어의 색을 활성화된 색으로 변경.
            nowPlayer.GetComponent<SpriteRenderer>().color = activatedColor;
        }
    }
}
