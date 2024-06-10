using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameResultManager : MonoBehaviour
{
    private SfxManager sfxManager;

    [SerializeField] private Goal[] Goals;

    private GameObject gameResultUI;
    private GameObject gameOverUI;
    private GameObject gameClearUI;

    private GameObject gameClearTimeTexts;

    private Text gameClearResultTime;
    private Text gameClearBestTime;

    private WaitForSecondsRealtime waitSec; // 코루틴에 쓸 WaitForSecondsRealtime에 사용할 변수.
                                            // WaitForSecondsRealtime은 WaitForSeconds과 달리 unscaled time 을 사용해서,
                                            // 일시정지할 때, timescale을 0으로 하더라도 상관없이 동일한 간격으로 탐지 가능.

    private void Awake()
    {
        sfxManager = GameManager.instance.gameObject.transform.GetChild(0).GetComponent<SfxManager>();

        waitSec = new WaitForSecondsRealtime(0.2f);

        gameResultUI = transform.GetChild(1).gameObject;
        gameOverUI = gameResultUI.transform.GetChild(0).gameObject;
        gameClearUI = gameResultUI.transform.GetChild(1).gameObject;

        gameClearTimeTexts = gameClearUI.transform.GetChild(1).gameObject;

        gameClearResultTime = gameClearTimeTexts.transform.GetChild(0).GetComponent<Text>();
        gameClearBestTime = gameClearTimeTexts.transform.GetChild(1).GetComponent<Text>();
    }

    private void Start()
    {
        // 결과 창을 꺼줌.
        gameResultUI.SetActive(false);
        gameOverUI.SetActive(false);
        gameClearUI.SetActive(false);
        gameClearTimeTexts.SetActive(false);

        // 게임이 끝났는 지 감지하는 메소드. (코루틴) 실행.
        StartCoroutine(DetectGameEndCoroutine());
    }

    // 게임이 끝났는 지 감지하는 메소드. (코루틴)
    private IEnumerator DetectGameEndCoroutine()
    {
        while(true)
        {
            // 일시정시가 아닐 때, (게임 오버/클리어 시 일시정지 적용됨 + 일시정지에는 여부 판별 필요 x).
            if (!GameManager.instance.IsPause)
            {
                if (!GameManager.instance.IsClear)  // 게임 클리어되지 않았을 때,
                {
                    DetectGameClear();
                }
            }

            // 게임이 끝났으면, (클리어/오버) 결과 창을 띄우고, 코루틴 종료.
            if (GameManager.instance.IsClear)
            {
                sfxManager.StopWalkSound();
                sfxManager.PlayStageClearSound();
                RecordTime();
                gameClearTimeTexts.SetActive(true);
                gameClearUI.SetActive(true);
                gameResultUI.SetActive(true);
                Debug.Log("Game End(Clear)!!");
                yield break;
            }
            else if (GameManager.instance.IsGameover)
            {
                gameOverUI.SetActive(true);
                gameResultUI.SetActive(true);
                Debug.Log("Game End(Over)!!");
                yield break;
            }

            yield return waitSec;
        }
    }

    // 게임 클리어를 판단하는 메소드. 게임 클리어인지 확인하고 게임 클리어이면, 게임매니저에 게임클리어 알림.
    private void DetectGameClear()
    {
        // 모든 목표지점에 해당 플레이어가 도달했는지 확인.
        for (int i = 0; i < Goals.Length; i++)
        {
            // 만약, 해당 플레이어가 해당 목표지점에 도달하지 않았으면, break;
            if (!Goals[i].GetIsGoal())
            {
                break;
            }

            // 모든 목표지점에 모든 해당 플레이어가 도달한 경우, 클리어.
            if (i == Goals.Length - 1)
            {
                GameManager.instance.GameClear();   // 게임매니저에 게임클리어 알림.
            }
        }
    }

    // 시간을 기록하는 메소드. 수업시간 코드 참고.
    private void RecordTime()
    {
        int result = Mathf.FloorToInt(Timer.time);  // 현재 결과.
        int highScore;  // 최고기록을 저장하는 변수.

        // 현재 씬의 최고기록을 위한 키 문자열.
        string stageHighScore = $"HighScore_{SceneManager.GetActiveScene().name}";

        // PlayerPrefs에 해당 키가 있는지.
        if (PlayerPrefs.HasKey(stageHighScore))
        {
            highScore = PlayerPrefs.GetInt(stageHighScore); // 있으면, 해당 키의 값을 가져옴.
        }
        else
        {
            highScore = 86400;  // 없으면, 86400 가져옴. (1일을 초로 환산하면, 86400초).
        }

        // 만약, 현재 결과가 최고기록보다 작으면 (더 빨리 클리어하면) 현재결과를 저장.
        if (highScore > result)
        {
            PlayerPrefs.SetInt(stageHighScore, result);
        }

        gameClearResultTime.text = $"ResultTime: {result}";
        gameClearBestTime.text = $"BestTime: {highScore}";
    }
}
