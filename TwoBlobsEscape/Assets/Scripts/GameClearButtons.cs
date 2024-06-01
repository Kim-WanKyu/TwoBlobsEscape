using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearButtons : MonoBehaviour
{
    [SerializeField]
    private GameObject gameClearAreaObject;     // 게임 클리어 영역 오브젝트. (게임 클리어 최상위 오브젝트)

    private GameObject gameClearButtonsObject;  // 게임 클리어 버튼s 오브젝트. (버튼들을 하위로 갖는 오브젝트)

    // 게임 클리어 시 버튼들.
    private Button restartButton, homeButton, nextButton;

    void Awake()
    {
        if (gameClearAreaObject != null)
        {
            gameClearButtonsObject = gameClearAreaObject.transform.GetChild(2).gameObject;

            restartButton = gameClearButtonsObject.transform.GetChild(0).GetComponent<Button>();
            homeButton    = gameClearButtonsObject.transform.GetChild(1).GetComponent<Button>();
            nextButton    = gameClearButtonsObject.transform.GetChild(2).GetComponent<Button>();

            restartButton.onClick.AddListener(SceneController.Restart);
            homeButton.onClick.AddListener(SceneController.GotoHome);
            nextButton.onClick.AddListener(SceneController.GotoNext);

            if (gameClearAreaObject.activeSelf == true)
            {
                gameClearAreaObject.SetActive(false);
            }
        }
    }
}
