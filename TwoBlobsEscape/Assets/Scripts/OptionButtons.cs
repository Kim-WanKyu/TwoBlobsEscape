using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionButtons : MonoBehaviour
{
    private SfxManager sfxManager;

    [SerializeField]
    private GameObject optionObject;        // 옵션 오브젝트 (옵션 최상위 오브젝트)

    private GameObject optionButtonObject;  // 옵션 버튼 오브젝트.
    private GameObject optionPanelObject;   // 옵션 패널 오브젝트.

    Button restartButton, homeButton, optionButton, optionExitButton;

    private void Awake()
    {
        sfxManager = GameManager.instance.gameObject.transform.GetChild(0).GetComponent<SfxManager>();

        if (optionObject != null)
        {
            optionButtonObject = optionObject.transform.GetChild(0).gameObject;

            optionButton = optionButtonObject.GetComponent<Button>();

            optionButton.onClick.AddListener(OpenOptionPanel);


            optionPanelObject = optionObject.transform.GetChild(1).gameObject;

            restartButton    = optionPanelObject.transform.GetChild(0).GetComponent<Button>();
            homeButton       = optionPanelObject.transform.GetChild(1).GetComponent<Button>();
            optionExitButton = optionPanelObject.transform.GetChild(2).GetComponent<Button>();

            optionExitButton.onClick.AddListener(CloseOptionPanel);
            restartButton.onClick.AddListener(Restart);
            homeButton.onClick.AddListener(GotoHome);

            if (optionPanelObject != null)
            {
                if (optionPanelObject.activeSelf == true)
                {
                    optionButtonObject.SetActive(true);

                    optionPanelObject.SetActive(false);
                    GameManager.instance.GameResume();
                }
            }
        }
    }

    public void OpenOptionPanel()
    {
        sfxManager.PlayUIButtonClickSound();
        sfxManager.PlayUIWindowOnSound();

        if (optionPanelObject != null)
        {
            if (optionPanelObject.activeSelf == false)
            {
                optionButtonObject.SetActive(false);

                optionPanelObject.SetActive(true);
                GameManager.instance.GamePause();
            }
        }
    }

    public void CloseOptionPanel()
    {
        sfxManager.PlayUIButtonClickSound();
        sfxManager.PlayUIWindowOffSound();

        if (optionPanelObject != null)
        {
            if (optionPanelObject.activeSelf == true)
            {
                optionButtonObject.SetActive(true);

                optionPanelObject.SetActive(false);
                GameManager.instance.GameResume();
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

