using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearButtons : MonoBehaviour
{
    private SfxManager sfxManager;

    [SerializeField]
    private GameObject gameClearAreaObject;     // 게임 클리어 영역 오브젝트. (게임 클리어 최상위 오브젝트)

    private GameObject gameClearButtonsObject;  // 게임 클리어 버튼s 오브젝트. (버튼들을 하위로 갖는 오브젝트)

    // 게임 클리어 시 버튼들.
    private Button restartButton, homeButton, nextButton;

    void Awake()
    {
        sfxManager = GameManager.instance.gameObject.transform.GetChild(0).GetComponent<SfxManager>();

        if (gameClearAreaObject != null)
        {
            gameClearButtonsObject = gameClearAreaObject.transform.GetChild(2).gameObject;

            restartButton = gameClearButtonsObject.transform.GetChild(0).GetComponent<Button>();
            homeButton    = gameClearButtonsObject.transform.GetChild(1).GetComponent<Button>();
            nextButton    = gameClearButtonsObject.transform.GetChild(2).GetComponent<Button>();

            restartButton.onClick.AddListener(Restart);
            homeButton.onClick.AddListener(GotoHome);
            nextButton.onClick.AddListener(GotoNext);

            if (gameClearAreaObject.activeSelf == true)
            {
                gameClearAreaObject.SetActive(false);
            }
        }
    }
    public void Restart()
    {
        sfxManager.PlayUIButtonClickSound();
        sfxManager.PlayStageStartSound();
        SceneController.Restart();
    }

    public void GotoHome()
    {
        sfxManager.PlayUIButtonClickSound();
        sfxManager.PlayStageStartSound();
        SceneController.GotoHome();
    }

    public void GotoNext()
    {
        sfxManager.PlayUIButtonClickSound();
        sfxManager.PlayStageStartSound();
        SceneController.GotoNext();
    }
}
