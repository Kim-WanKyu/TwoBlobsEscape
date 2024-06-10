using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtons : MonoBehaviour
{
    [SerializeField]
    private GameObject howToPlayAreaPanelObject;    // 설명서 영역 오브젝트.

    private SfxManager sfxManager;

    // 씬 시작 시 초기화.
    private void Start()
    {
        sfxManager = GameManager.instance.gameObject.transform.GetChild(0).GetComponent<SfxManager>();

        // 설명서 영역은 비활성화(끄기)된 상태로 초기화.
        if (howToPlayAreaPanelObject.activeSelf == true)
        {
            howToPlayAreaPanelObject.SetActive(false);
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
        GameManager.instance.GameQuit();
    }
}
