using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PasswordGame : Puzzle
{
    private GameObject AnswerAreaObj;   // AnswerArea 오브젝트 (슬롯들의 상위 오브젝트, 슬롯 하나당 위 버튼, 아래 버튼, 숫자 텍스트 하나.)
    private Button[] upButtons;     // 위쪽 버튼들.
    private Button[] downButtons;   // 아래쪽 버튼들.
    private Text[] slotTexts;       // 숫자 텍스트들.

    private int size;               // 길이. (슬롯 개수)

    private Text questionText;      // 질문 텍스트.

    private string answer;          // 정답 문자열.

    private void Awake()
    {
        InitMatchingObj();

        InitButtonOnClick();

        SetClear(false);
    }

    private void Start()
    {
        SetClear(false);

        InitTexts();
    }


    // 오브젝트 연결 및 할당 메소드.
    private void InitMatchingObj()
    {
        questionText = transform.GetChild(0).GetChild(0).GetComponent<Text>();

        AnswerAreaObj = transform.GetChild(1).gameObject;
        
        size = AnswerAreaObj.transform.childCount;

        upButtons = new Button[size];
        downButtons = new Button[size];
        slotTexts = new Text[size];

        for (int i = 0; i < size; i++)
        {
            upButtons[i]   = AnswerAreaObj.transform.GetChild(i).GetChild(0).GetComponent<Button>();
            downButtons[i] = AnswerAreaObj.transform.GetChild(i).GetChild(1).GetComponent<Button>();
            slotTexts[i]   = AnswerAreaObj.transform.GetChild(i).GetChild(2).GetChild(0).GetComponent<Text>();
        }
    }

    // 버튼 onclick 메소드 연결 메소드.
    private void InitButtonOnClick()
    {
        for (int i = 0; i < size; i++)
        {
            upButtons[i].onClick.AddListener(UpNumber);
            downButtons[i].onClick.AddListener(DownNumber);
        }
    }

    // 텍스트 초기화 메소드.
    private void InitTexts()
    {
        questionText.text = MakeQuestion();

        for (int i = 0; i < size; i++)
        {
            slotTexts[i].text = "0";
        }
    }

    // 문제 만드는 메소드.
    private string MakeQuestion()
    {
        int questionValue = Random.Range(0, 10000); // 정답이 될 0~9999의 랜덤한 정수를 만들고,

        answer = questionValue.ToString();  // 정답 변수에 넣어주고,

        int a = Random.Range(0, questionValue);     // a와 b 두 부분으로 랜덤하게 나누어주어,
        int b = questionValue - a;


        return $"{a} + {b} = ?";                    // "a+b=?" 형식의 텍스트를 만들어 리턴.
    }


    // 해당 슬롯의 숫자를 하나 올리는 메소드.
    public void UpNumber()
    {
        sfxManager = GameManager.instance.gameObject.transform.GetChild(0).GetComponent<SfxManager>();
        sfxManager.PlayButtonSound();

        GameObject go = EventSystem.current.currentSelectedGameObject;  // EventSystem.current.currentSelectedGameObject 마우스로 선택된 오브젝트 반환.

        Text t = go.transform.parent.GetChild(2).GetChild(0).GetComponent<Text>();
        int currentNum = int.Parse(t.text);
        currentNum = (currentNum + 1) % 10;

        t.text = currentNum.ToString();

        CheckClear();
    }

    // 해당 슬롯의 숫자를 하나 내리는 메소드.
    public void DownNumber()
    {
        sfxManager = GameManager.instance.gameObject.transform.GetChild(0).GetComponent<SfxManager>();
        sfxManager.PlayButtonSound();

        GameObject go = EventSystem.current.currentSelectedGameObject;  // EventSystem.current.currentSelectedGameObject 마우스로 선택된 오브젝트 반환.

        Text t = go.transform.parent.GetChild(2).GetChild(0).GetComponent<Text>();
        int currentNum = int.Parse(t.text);
        currentNum = (currentNum - 1 + 10) % 10;

        t.text = currentNum.ToString();

        CheckClear();
    }

    // 슬롯 값을 읽어와서 문자열로 만드는 메소드.
    public string GetSlotNumbers()
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < size; i++)
        {
            sb.Append(slotTexts[i].text);
        }
        
        return sb.ToString();
    }

    public override void InitPosSet()
    {
        // 조각이 없는 퍼즐이라 위치 초기화 필요 없음.
        // 따라서, overriding하여 내용을 구현하지는 않는다.
    }

    public override void CheckClear()
    {
        if (answer.CompareTo(GetSlotNumbers()) == 0)  // 질문의 답과 슬롯의 답이 같으면, 정답.
        {
            sfxManager.PlayPuzzleClearSound();

            SetClear(true);
        }
    }
}
