using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtons : MonoBehaviour
{
    private SfxManager sfxManager;

    [SerializeField] private GameObject howToPlayAreaPanelObject;    // 설명서 영역 오브젝트.
    private GameObject howToPlayDetailObject;       // 설명서 설명 부분 오브젝트.

    private int pageNum;    // 페이지 번호.
    private int pageLen;    // 페이지 길이.

    private void Awake()
    {
        howToPlayDetailObject = howToPlayAreaPanelObject.transform.GetChild(0).GetChild(1).gameObject;

        pageNum = 0;
        pageLen = howToPlayDetailObject.transform.childCount;
    }

    // 씬 시작 시 초기화.
    private void Start()
    {
        sfxManager = GameManager.instance.gameObject.transform.GetChild(0).GetComponent<SfxManager>();

        // 설명서 영역은 비활성화(끄기)된 상태로 초기화.
        if (howToPlayAreaPanelObject.activeSelf == true)
        {
            howToPlayAreaPanelObject.SetActive(false);
        }

        // 설명서 설명 부분 오브젝트는 활성화된 상태로 초기화.
        if (howToPlayDetailObject.activeSelf == false)
        {
            howToPlayDetailObject.SetActive(true);

        }

        // 설명서 설명 부분의 0페이지는 활성화된 상태로 초기화.
        howToPlayDetailObject.transform.GetChild(0).gameObject.SetActive(true);
        for (int i = 1; i < 4; i++)
        {
            // 설명서 설명 부분의 나머지 1~3페이지는 비활성화된 상태로 초기화.
            howToPlayDetailObject.transform.GetChild(i).gameObject.SetActive(false);
        }

    }

    // 버튼 스크립트들.

    // (GameStartButton 버튼).
    public void GameStartButton()
    {
        sfxManager.PlayUIButtonClickSound();
        sfxManager.PlayStageStartSound();

        SceneManager.LoadScene("Game1");
    }

    // (HowToPlayButton 버튼).
    public void HowToPlayButton_OnOff()
    {
        sfxManager.PlayUIButtonClickSound();
        Debug.Log("게임설명");
        // HowToPlay영역 켜고 끄기.
        if (howToPlayAreaPanelObject != null)
        {
            // 꺼져있으면, 켜고.
            if (howToPlayAreaPanelObject.activeSelf == false)
            {
                Debug.Log("게임설명서 켜기");
                sfxManager.PlayUIWindowOnSound();
                howToPlayAreaPanelObject.SetActive(true);
            }
            // 그렇지 않으면, 끈다.
            else
            {
                Debug.Log("게임설명서 끄기");
                sfxManager.PlayUIWindowOffSound();
                howToPlayAreaPanelObject.SetActive(false);
            }
        }
    }

    // (HowToPlayExitButton 버튼).
    public void HowToPlayExitButton_Off()
    {
        sfxManager.PlayUIButtonClickSound();
        Debug.Log("게임설명X");
        // HowToPlay영역 끄기.
        if (howToPlayAreaPanelObject != null)
        {
            // 켜져있으면, 끈다.
            if (howToPlayAreaPanelObject.activeSelf == true)
            {
                Debug.Log("게임설명서 끄기");
                sfxManager.PlayUIWindowOffSound();
                howToPlayAreaPanelObject.SetActive(false);
            }
        }
    }

    // (QuitButton 버튼).
    public void QuitButton()
    {
        sfxManager.PlayUIButtonClickSound();
        sfxManager.PlayUIWindowOffSound();
        // 프로그램 종료.
        GameManager.instance.GameQuit();
    }

    // (NextPageButton 버튼).
    public void NextPageButton()
    {
        sfxManager.PlayUIButtonClickSound();

        // 현재 페이지를 비활성화하고,
        howToPlayDetailObject.transform.GetChild(pageNum).gameObject.SetActive(false);
        // 페이지를 증가시킨 다음,
        pageNum = (pageNum + 1) % pageLen; // (0->1->2->3->0)
        // 다음 페이지를 활성화.
        howToPlayDetailObject.transform.GetChild(pageNum).gameObject.SetActive(true);
    }

    // (PrevPageButton 버튼).
    public void PrevPageButton()
    {
        sfxManager.PlayUIButtonClickSound();

        // 현재 페이지를 비활성화하고,
        howToPlayDetailObject.transform.GetChild(pageNum).gameObject.SetActive(false);
        // 페이지를 감소시킨 다음,
        pageNum = (pageNum - 1 + pageLen) % pageLen; // (0->3->2->1->0)
        // 이전 페이지를 활성화.
        howToPlayDetailObject.transform.GetChild(pageNum).gameObject.SetActive(true);
    }
}
