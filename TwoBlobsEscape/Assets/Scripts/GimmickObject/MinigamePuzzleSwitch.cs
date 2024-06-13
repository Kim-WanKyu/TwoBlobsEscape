using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamePuzzleSwitch : SwitchObject
{
    [SerializeField] private GameObject minigame;       // 실행할 미니게임 오브젝트.

    private SpriteRenderer switchSpriteRenderer;        // 스위치 오브젝트의 스프라이트렌더러.
    [SerializeField] private Sprite onSwitchSprite;     // 켜진 스위치 스프라이트.
    [SerializeField] private Sprite offSwitchSprite;    // 꺼진 스위치 스프라이트.

    [SerializeField] private Door door;                 // 이 스위치가 열어야 할 문 오브젝트의 정보와 메소드 들어있음.

    WaitForSecondsRealtime waitSec;

    Puzzle puzzle;

    private bool isActivate;

    public void Awake()
    {
        isActivate = false;

        minigame.SetActive(false);
        puzzle = minigame.transform.GetChild(0).GetChild(0).GetComponent<Puzzle>();
        switchSpriteRenderer = GetComponent<SpriteRenderer>();

        switchSpriteRenderer.sprite = offSwitchSprite;

        waitSec = new WaitForSecondsRealtime(0.2f);
    }

    public override void ControlSwitch()
    {
        if (isActivate == false)
        {
            isActivate = true;

            switchSpriteRenderer.sprite = onSwitchSprite;
            // 스위치 조작 시 게임매니저에 미니게임 시작 신호 보냄.
            GameManager.instance.StartMiniGame();

            // 미니게임 오브젝트UI 켬.
            minigame.SetActive(true);

            StartCoroutine(OpenDoorCoroutine());
        }
    }

    private IEnumerator OpenDoorCoroutine()
    {
        while (true)
        {
            if (puzzle.IsClear)
            {
                door.OpenDoor();
                isActivate = false;
                transform.parent.gameObject.SetActive(false);

                // gameObject.SetActive(false);

                GameManager.instance.StopMiniGame();

                puzzle.SetClear(false);

                yield break;
            }

            yield return waitSec;
        }
    }
}
