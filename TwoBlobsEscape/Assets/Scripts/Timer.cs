using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 수업시간 타이머 참고.
public class Timer : MonoBehaviour
{
    public static float time;

    private Text timeText;
    
    // Start is called before the first frame update
    void Start()
    {
        timeText = GetComponent<Text>();
        time = 0;
    }

    void Update()
    {
        // 일시정지 상태가 아닐 때에만, 시간을 더해주고 화면에 출력함. (시간이 멈추면 결과가 같기 때문에, 갱신할 필요가 없음.)
        if (!GameManager.instance.IsPause)
        {
            time += Time.deltaTime;

            int timeInt = Mathf.FloorToInt(time);
            
            timeText.text = $"Time : {timeInt}";
        }
    }
}
