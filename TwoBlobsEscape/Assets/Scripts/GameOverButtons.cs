using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverButtons : MonoBehaviour
{
    private SfxManager sfxManager;

    [SerializeField]
    private GameObject gameOverAreaObject;      // 게임 오버 영역 오브젝트. (게임 오버 최상위 오브젝트)

    private GameObject gameOverButtonsObject;   // 게임 오버 버튼s 오브젝트. (버튼들을 하위로 갖는 오브젝트)

    // 게임 오버 시 버튼들.
    private Button restartButton, homeButton;

    void Awake()
    {
        sfxManager = GameManager.instance.gameObject.transform.GetChild(0).GetComponent<SfxManager>();

        if (gameOverAreaObject != null)
        {
            gameOverButtonsObject = gameOverAreaObject.transform.GetChild(1).gameObject;

            restartButton = gameOverButtonsObject.transform.GetChild(0).GetComponent<Button>();
            homeButton    = gameOverButtonsObject.transform.GetChild(1).GetComponent<Button>();

            restartButton.onClick.AddListener(Restart);
            homeButton.onClick.AddListener(GotoHome);

            if (gameOverAreaObject.activeSelf == true)
            {
                gameOverAreaObject.SetActive(false);
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
}
