using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonPanelObject, instructionPanelObject;

    [SerializeField]
    private Button btnStart, btnInstruction, btnQuit, btnInsExit;

    // 씬 시작 시 초기화.
    public void Start()
    {
        // 버튼에 이벤트 추가.
        btnStart.onClick.AddListener(GotoGameScene);
        btnInstruction.onClick.AddListener(OpenInstuction);
        btnQuit.onClick.AddListener(ExitScene);
        btnInsExit.onClick.AddListener(CloseInstuction);

        // 설명서 영역은 비활성화된 상태로 초기화.
        if (instructionPanelObject.activeSelf == true)
        {
            instructionPanelObject.gameObject.SetActive(false);
        }
    }

    // HowToPlay영역 켜기.
    public void OpenInstuction()
    {
        if (instructionPanelObject != null)
        {
            if (instructionPanelObject.gameObject.activeSelf == false)
            {
                instructionPanelObject.gameObject.SetActive(true);
            }
        }
    }

    // HowToPlay영역 끄기.
    public void CloseInstuction()
    {
        if (instructionPanelObject != null)
        {
            if (instructionPanelObject.gameObject.activeSelf == true)
            {
                instructionPanelObject.gameObject.SetActive(false);
            }
        }
    }

    // 게임 씬(Game01)으로 이동.
    public void GotoGameScene()
    {
        SceneManager.LoadScene("Game1");
    }

    // 프로그램 종료.
    public void ExitScene()
    {
        Application.Quit();
    }
}
