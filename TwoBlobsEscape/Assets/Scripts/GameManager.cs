using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    // 게임의 상태에 대한 정보를 담는 변수들.
    /*프로퍼티 : get set 함수 없이 쉽게 접근할 수 있음*/
    // get; -> 읽을 수 있음. / private set; -> private 에 의해서 외부에서 쓸 수는 없음. (외부에서 set하려면, set 메소드 작성해야 함.)
    public bool IsGameover { get; private set; }    // 게임 오버 상태인지 (게임오버 시 true)
    public bool IsClear { get; private set; }       // 게임 클리어 상태인지 (게임클리어 시 true)
    public bool IsPause { get; private set; }       // 일시정지 상태인지 (일시정지 시 true)
    public bool IsPlayingMiniGame { get; private set; } // 미니게임 중인지 (미니게임 진행 중일 시 true)
    public int LevelCount { get; private set; }     // 스테이지의 개수.

    private void Awake()
    {
        Debug.Log("GameManager");
        if (instance == null)
        {
            instance = this;

            Init(); // 값 초기화.
        }
        else
        {
            // 같은 씬으로 다시 이동하면, Awake()문이 실행되며 오브젝트가 또 생성되므로, 오브젝트가 중복됨.
            Debug.LogWarning("씬에 두 개 이상의 게임매니저가 존재합니다!");
            Destroy(gameObject);    // 중복되므로 하나만 남도록 지워줌.
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);  // 이 오브젝트가 파괴되지 않도록 처리.

    }

    // 게임의 상태에 대한 정보 초기화.
    public void Init()
    {
        instance.IsGameover = false;
        instance.IsClear = false;
        instance.IsPause = false;
        LevelCount = 2; //임시로 2스테이지까지만,
    }

    // 게임 오버.
    public void GameOver()
    {
        IsGameover = true;
        GamePause();
    }

    // 게임 클리어.
    public void GameClear()
    {
        IsClear = true;
        GamePause();
    }

    // 게임 일시정지.
    public void GamePause()
    {
        IsPause = true;
        Time.timeScale = 0f;    // 시간 흐르는 속도 0으로. (멈춤)
    }

    // 게임 재개. (일시정지 해제).
    public void GameResume()
    {
        IsPause = false;
        Time.timeScale = 1f;    // 시간 흐르는 속도 1으로. (1배속)
    }

    // 미니게임 시작.
    public void StartMiniGame()
    {
        IsPlayingMiniGame = true;
        Time.timeScale = 0f;    // 시간 흐르는 속도 0으로. (멈춤)
    }

    // 미니게임 종료.
    public void StopMiniGame()
    {
        IsPlayingMiniGame = false;
        Time.timeScale = 1f;    // 시간 흐르는 속도 1으로. (1배속)
    }

    // 게임 종료. (프로그램 종료)
    public void GameQuit()
    {
        Debug.Log("모든 시간 기록 삭제");
        //PlayerPrefs에 저장된 모든 데이터 삭제.
        PlayerPrefs.DeleteAll();

        Debug.Log("게임종료");
        // 프로그램 종료.
        Application.Quit(); // 빌드 안하고 테스트 시에는 작동x.
    }

}
