using UnityEngine;
using UnityEngine.SceneManagement;

// 씬 이동 관련 클래스.
public class SceneController
{
    // 현재 씬을 다시 시작하는 메소드.
    public static void Restart()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.instance.GameResume();
        GameManager.instance.Init();
    }

    // 시작 화면으로 가는 메소드.
    public static void GotoHome()
    {
        Debug.Log("GotoHome");
        SceneManager.LoadScene("Start");
        GameManager.instance.GameResume();
        GameManager.instance.Init();
    }

    // 다음 스테이지 레벨 씬으로 가는 메소드. (Game1 -> Game2)
    public static void GotoNext()
    {
        Debug.Log("GotoNext");
        string sceneName = SceneManager.GetActiveScene().name;
        string nStr = sceneName.Substring(sceneName.Length - 1);
        int n = int.Parse(nStr);
        // 최대 스테이지 이전 스테이지면, 다음 스테이지로 이동.
        if (1 <= n && n < GameManager.instance.LevelCount)
        {
            string nextName = $"Game{n + 1}";
            SceneManager.LoadScene(nextName);
            GameManager.instance.GameResume();
            GameManager.instance.Init();
        }
        else    // 만약 마지막 스테이지이면, 시작 화면으로 이동.
        {
            GotoHome();
        }

    }
}
