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
        // 일시정지 상태가 아닐 때에만, 플레이어 조작 가능. (게임오버나 게임 클리어 시도 일시정지가 적용됨).
        if (!GameManager.instance.IsPause)
        {
            if (Input.GetKey(KeySetting.keys[KeyAction.JUMP]))
            {
                Jump(); // 플레이어 점프.
            }
        }
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.IsPause)
        {
            Move(); // 플레이어 이동.
            LandingPlatform();  // 플레이어 착지.
        }
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

        GameObject nowPlayer = playerSwapper.GetNowPlayer();
        Rigidbody2D nowPlayerRigid = nowPlayer.GetComponent<Rigidbody2D>();

        // 현재 플레이어의 속도를 설정한 방향값에 이동속도로 설정하여, 현재 플레이어를 이동시키는 코드.
        nowPlayerRigid.velocity = new Vector2(direction.x * speed, nowPlayerRigid.velocity.y);

        // 걷기 애니메이션.
        // 방향값이 있으면 이동 중이므로, 걷기 애니메이션 실행.
        if (direction != Vector2.zero)
        {
            nowPlayer.GetComponent<SpriteRenderer>().flipX = (direction == Vector2.left);    // 스프라이트 방향 설정. 플레이어의 방향이 왼쪽이면 스프라이트를 flip함.
            nowPlayer.GetComponent<Animator>().SetBool("isWalking", true);
        }
        else
        {
            // 그렇지 않으면, 정지이므로, 걷기 애니메이션 끄기.
            nowPlayer.GetComponent<Animator>().SetBool("isWalking", false);
        }
    }

    // 플레이어 점프.
    private void Jump()
    {
        // 바닥을 밟으면,
        if (IsLanding())
        {
            StartCoroutine(JumpCoroutine(playerSwapper.GetNowPlayer()));
        }
    }

    // 점프 코루틴.
    private IEnumerator JumpCoroutine(GameObject nowPlayer)
    {
        // 점프 애니메이션 실행.
        nowPlayer.GetComponent<Animator>().SetBool("isJumping", true);

        Rigidbody2D nowPlayerRigid = nowPlayer.GetComponent<Rigidbody2D>();
        // 플레이어가 점프하기 직전에 y축의 속도를 0으로 설정.
        nowPlayerRigid.velocity = new Vector2(nowPlayerRigid.velocity.x, 0f);
        // 플레이어를 위의 방향으로 힘을 주어 점프 시킴.
        nowPlayerRigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

        yield break;
    }
    
    // 플레이어 착지.
    private void LandingPlatform()
    {
        GameObject nowPlayer = playerSwapper.GetNowPlayer();
        Animator nowPlayerAnimator = nowPlayer.GetComponent<Animator>();

        if (IsLanding())
        {
            // 바닥을 밟으면, 애니메이션의 "isJumping"값을 false로 설정.
            nowPlayerAnimator.SetBool("isJumping", false);
        }
        else
        {
            // 바닥을 밟지 않으면, -> 공중이면, 애니메이션의 "isJumping"값을 true로 설정. -> 점프가 아닌 추락일 때도 점프로 처리하기 위함. -> 추락 시에도 착지 애니메이션 실행됨.
            nowPlayerAnimator.SetBool("isJumping", true);
        }
    }

    private bool IsLanding()
    {
        GameObject nowPlayer = playerSwapper.GetNowPlayer();

        // 바닥 감지 영역을 위한 값들 초기화.
        float detectionWidth = 0.5f * nowPlayer.GetComponent<Collider2D>().bounds.size.x;  // 감지 영역 너비를 플레이어의 콜라이더의 x 크기의 0.5로 설정.
        Vector2 hitPos = nowPlayer.transform.position;          // 감지 영역 위치.
        Vector2 hitSize = new Vector2(detectionWidth, 0.1f);    // 감지 영역 크기.

        // BoxCast로 박스모양의 영역으로 raycasthit. (레이어가 Platform인 오브젝트만 감지)
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(hitPos, hitSize, 0f, Vector2.down, 0, LayerMask.GetMask("Platform"));

        // 바닥을 밟으면, collider가 null이 아님.
        if (raycastHit2D.collider != null)
        {
            return true;
        }
        return false;
    }

    // (Debug) 를 위한 해당 오브젝트 선택 시 기즈모를 그려주는 메소드.
    private void OnDrawGizmosSelected()
    {
        GameObject nowPlayer = playerSwapper.GetNowPlayer();

        // 바닥 감지 영역을 위한 값들 초기화.
        float detectionWidth = 0.5f * nowPlayer.GetComponent<Collider2D>().bounds.size.x;  // 감지 영역 너비를 플레이어의 콜라이더의 x 크기의 0.5로 설정.
        Vector2 hitPos = nowPlayer.transform.position;          // 감지 영역 위치.
        Vector2 hitSize = new Vector2(detectionWidth, 0.1f);    // 감지 영역 크기.

        // BoxCast로 박스모양의 영역으로 raycasthit. (레이어가 Platform인 오브젝트만 감지)
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(hitPos, hitSize, 0f, Vector2.down, 0, LayerMask.GetMask("Platform"));

        // 충돌 감지 시각화를 위한 DrawRay. 바닥을 밟으면 녹색, 아니면 빨간색으로 씬에서 볼 수 있음.
        Gizmos.color = (raycastHit2D.collider != null ? Color.green : Color.red);
        Gizmos.DrawCube(hitPos, hitSize);
    }

    // 플레이어 죽음.
    public void Die()
    {
        Debug.Log("플레이어 죽음");

        PlayerDeathAnim();

        GameManager.instance.GameOver();    // 게임매니저에 게임오버 알림.
    }

    // 플레이어 죽음 시 플레이어를 부드럽게 사라지도록 하는 메소드.
    private void PlayerDeathAnim()
    {
        StartCoroutine(PlayerDeathAnimCoroutine());
    }

    // 플레이어 죽음 시 플레이어를 부드럽게 사라지도록 하는 코루틴.
    private IEnumerator PlayerDeathAnimCoroutine()
    {
        Color fromColor = new Color(1f, 1f, 1f, 1f);    //흰색 불투명.
        Color toColor = new Color(0f, 0f, 0f, 0f);      //검은색 투명.

        GameObject playerBlue = playerSwapper.GetPlayerBlue();
        GameObject playerPink = playerSwapper.GetPlayerPink();

        SpriteRenderer blueSpriteRenderer = playerBlue.GetComponent<SpriteRenderer>();
        SpriteRenderer pinkSpriteRenderer = playerPink.GetComponent<SpriteRenderer>();

        float time = 0f;
        float duration = 1f;
        while (time < duration)
        {
            // Lerp 함수를 이용하여 부드러운 색 변경이 가능하게 함.
            blueSpriteRenderer.color = Color.Lerp(fromColor, toColor, time / duration);
            pinkSpriteRenderer.color = Color.Lerp(fromColor, toColor, time / duration);

            time += Time.unscaledDeltaTime;

            yield return null;
        }

        // while 문 종료시 확실하게 toColor로 변경되도록 함.
        blueSpriteRenderer.color = toColor;
        pinkSpriteRenderer.color = toColor;

        // 스프라이트 이미지 상으로 사라지게 한 다음 오브젝트를 비활성화.
        playerBlue.SetActive(false);
        playerPink.SetActive(false);
    }
}
