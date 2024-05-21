using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerSwapper playerSwapper;    // 현재 플레이어를 가져오기 위해 추가.

    private float speed;        // 이동속도.
    private float jumpPower;    //점프력.

    private void Awake()
    {
        playerSwapper = GetComponent<PlayerSwapper>();

        speed = 7f;
        jumpPower = 21.5f;
    }

    private void Update()
    {
        if (Input.GetKey(KeySetting.keys[KeyAction.JUMP]))
        {
            Jump(); // 플레이어 점프.
        }
    }

    private void FixedUpdate()
    {
        Move(); // 플레이어 이동.
        LandingPlatform();  // 플레이어 착지.
    }

    // 플레이어 이동.
    private void Move()
    {
        Vector2 direction = Vector2.zero;
        // 키보드 입력 받아서 방향값을 설정.
        if (Input.GetKey(KeySetting.keys[KeyAction.RIGHT]))
        {
            direction = Vector2.right;
        }
        else if (Input.GetKey(KeySetting.keys[KeyAction.LEFT]))
        {
            direction = Vector2.left;
        }

        // 현재 플레이어의 속도를 설정한 방향값에 이동속도로 설정하여, 현재 플레이어를 이동시키는 코드.
        playerSwapper.GetNowPlayer().GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * speed, playerSwapper.GetNowPlayer().GetComponent<Rigidbody2D>().velocity.y);

        // 걷기 애니메이션.
        // 방향값이 있으면 이동 중이므로, 걷기 애니메이션 실행.
        if (direction != Vector2.zero)
        {
            playerSwapper.GetNowPlayer().GetComponent<SpriteRenderer>().flipX = (direction == Vector2.left);    // 스프라이트 방향 설정. 플레이어의 방향이 왼쪽이면 스프라이트를 flip함.
            playerSwapper.GetNowPlayer().GetComponent<Animator>().SetBool("isWalking", true);
        }
        else
        {
            // 그렇지 않으면, 정지이므로, 걷기 애니메이션 끄기.
            playerSwapper.GetNowPlayer().GetComponent<Animator>().SetBool("isWalking", false);
        }
    }

    // 플레이어 점프.
    private void Jump()
    {
        // 애니메이션의 "isJumping"값이 false이면, -> 점프 중이 아님(공중에 있지 않음).
        if (playerSwapper.GetNowPlayer().GetComponent<Animator>().GetBool("isJumping") == false)
        {
            // 플레이어를 위의 방향으로 힘을 주어 점프 시킴.
            playerSwapper.GetNowPlayer().GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            // 점프 애니메이션 실행.
            playerSwapper.GetNowPlayer().GetComponent<Animator>().SetBool("isJumping", true);
        }
    }
    
    // 플레이어 착지.
    private void LandingPlatform()
    {
        // 바닥 감지 영역을 위한 값들 초기화.
        float playerSizeX = playerSwapper.GetNowPlayer().GetComponent<Collider2D>().bounds.size.x;  // 감지 영역 너비를 플레이어의 콜라이더의 x 크기로 설정.
        Vector2 hitPos = playerSwapper.GetNowPlayer().transform.position;   // 감지 영역 위치.
        Vector2 hitSize = new Vector2(playerSizeX, 0.2f);                   // 감지 영역 크기.

        // BoxCast로 박스모양의 영역으로 raycasthit. (레이어가 Platform인 오브젝트만 감지)
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(hitPos, hitSize, 0f, Vector2.right, 0, LayerMask.GetMask("Platform"));

        // 플레이어의 y속력이 0보다 작을 때 -> 플레이어가 떨어지고 있을 때에만 조건을 검사.
        if (playerSwapper.GetNowPlayer().GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            // 바닥을 밟으면, collider가 null이 아님.
            if (raycastHit2D.collider != null)
            {
                Debug.Log("standing platform is : " + raycastHit2D.collider.name);
                // 바닥을 밟으면, 애니메이션의 "isJumping"값을 false로 설정.
                playerSwapper.GetNowPlayer().GetComponent<Animator>().SetBool("isJumping", false);
                // 바닥을 밟으면, 플레이어의 속도를 0으로.
                playerSwapper.GetNowPlayer().GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
            else
            {
                // 바닥을 밟지 않으면, -> 공중이면, 애니메이션의 "isJumping"값을 true로 설정. -> 점프가 아닌 추락일 때도 점프로 처리하기 위함. -> 추락 시에도 착지 애니메이션 실행됨.
                playerSwapper.GetNowPlayer().GetComponent<Animator>().SetBool("isJumping", true);
            }
        }

        // (Debug) 충돌 감지 시각화를 위한 DrawRay. 바닥을 밟으면 녹색, 아니면 빨간색으로 씬에서 볼 수 있음.
        Color hitColor = (raycastHit2D.collider != null ? Color.green : Color.red);
        Debug.DrawRay(hitPos + new Vector2(-playerSizeX / 2f, 0.1f), Vector2.down * 0.2f, hitColor);
        Debug.DrawRay(hitPos + new Vector2(playerSizeX / 2f, 0.1f), Vector2.down * 0.2f, hitColor);
        Debug.DrawRay(hitPos + new Vector2(playerSizeX / 2f, 0.1f), Vector2.left * playerSizeX, hitColor);
        Debug.DrawRay(hitPos + new Vector2(playerSizeX / 2f, -0.1f), Vector2.left * playerSizeX, hitColor);
    }

}
