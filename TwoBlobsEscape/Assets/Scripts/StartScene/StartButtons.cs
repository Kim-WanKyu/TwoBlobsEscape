using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class StartButtons : MonoBehaviour
{
    [SerializeField]
    private GameObject HowToPlayAreaPanelObject;    // 설명서 영역 오브젝트.

    // 씬 시작 시 초기화.
    private void Start()
    {
        // 설명서 영역은 비활성화(끄기)된 상태로 초기화.
        if (HowToPlayAreaPanelObject.activeSelf == true)
        {
            HowToPlayAreaPanelObject.gameObject.SetActive(false);
        }
    }

    // 버튼 스크립트들.

    // (GameStartButton 버튼).
    public void GameStartButton()
    {
        Debug.Log("게임시작");
        // 게임 씬(Game01)으로 이동.
        SceneManager.LoadScene("Game1");
    }

    // (HowToPlayButton 버튼).
    public void HowToPlayButton_OnOff()
    {
        Debug.Log("게임설명");
        // HowToPlay영역 켜고 끄기.
        if (HowToPlayAreaPanelObject != null)
        {
            // 꺼져있으면, 켜고.
            if (HowToPlayAreaPanelObject.gameObject.activeSelf == false)
            {
                Debug.Log("게임설명서 켜기");
                HowToPlayAreaPanelObject.gameObject.SetActive(true);
            }
            // 그렇지 않으면, 끈다.
            else
            {
                Debug.Log("게임설명서 끄기");
                HowToPlayAreaPanelObject.gameObject.SetActive(false);
            }
        }
    }

    // (HowToPlayExitButton 버튼).
    public void HowToPlayExitButton_Off()
    {
        Debug.Log("게임설명X");
        // HowToPlay영역 끄기.
        if (HowToPlayAreaPanelObject != null)
        {
            // 켜져있으면, 끈다.
            if (HowToPlayAreaPanelObject.gameObject.activeSelf == true)
            {
                Debug.Log("게임설명서 끄기");
                HowToPlayAreaPanelObject.gameObject.SetActive(false);
            }
        }
    }

    // (QuitButton 버튼).
    public void QuitButton()
    {
        Debug.Log("게임종료");
        // 프로그램 종료. 
        Application.Quit();
    }
}
