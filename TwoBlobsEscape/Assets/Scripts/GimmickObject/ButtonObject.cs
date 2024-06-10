using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// (조작가능한 오브젝트인 버튼)의 상위 추상 클래스. (이지만, IControlObject 상속 x => 상호작용을 통해 동작하는 것이 아니라, 플레이어와 충돌 시 동작하기 때문.)
public abstract class ButtonObject : MonoBehaviour
{
    private SfxManager sfxManager;

    private LinkedList<GameObject> collidedPlayers;  // 충돌한 플레이어들 리스트.
    private SpriteRenderer buttonSpriteRenderer;            // 버튼 오브젝트의 스프라이트렌더러.
    [SerializeField] private Sprite baseButtonSprite;       // 기본 버튼 스프라이트.
    [SerializeField] private Sprite pushedButtonSprite;     // 눌린 버튼 스프라이트.

    // 처음에는 충돌한 플레이어들 리스트를 초기화.
    private void Awake()
    {
        sfxManager = GameManager.instance.gameObject.transform.GetChild(0).GetComponent<SfxManager>();

        collidedPlayers = new LinkedList<GameObject>();

        // 버튼 오브젝트의 스프라이트렌더러 가져옴.
        buttonSpriteRenderer = GetComponent<SpriteRenderer>();
        buttonSpriteRenderer.sprite = baseButtonSprite; // 기본 버튼 스프라이트로 초기화.
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
            sfxManager.PlayButtonSound();

            buttonSpriteRenderer.sprite = pushedButtonSprite;
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
                buttonSpriteRenderer.sprite = baseButtonSprite;
                CancelButton(); // 버튼 누름 해제.
            }
        }
    }

    // 버튼 누름.
    public abstract void PushButton();

    // 버튼 누름 해제.
    public abstract void CancelButton();

}
