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
        questionText.text = MakeQuestion(); // 문제는 만들어준 값으로 초기화하고,

        for (int i = 0; i < size; i++)  // 슬롯은 '0' 으로 초기화.
        {
            slotTexts[i].text = "0";
        }
    }

    // 문제 만드는 메소드.
    private string MakeQuestion()
    {
        int questionValue = UnityEngine.Random.Range(0, (int)Mathf.Pow(10, size));    // 정답이 될 0 ~ 10^size(4)-1(=9999)의 랜덤한 정수를 만들어

        answer = questionValue.ToString().PadLeft(size, '0');  // 부족한 부분은 0으로 채운 size의 길이만큼의 문자열을 만들어준 다음, 정답 변수에 넣어주고,

        int a = UnityEngine.Random.Range(0, questionValue);    // a와 b 두 부분으로 랜덤하게 나누어주어,
        int b = questionValue - a;                             // a + b = answer 가 되도록 해줌.

        return $"{a} + {b} = ?";        // "a + b = ?" 형식의 문자열을 만들어 리턴.
    }


    // 해당 슬롯의 숫자를 하나 올리는 메소드.
    public void UpNumber()
    {
        sfxManager = GameManager.instance.gameObject.transform.GetChild(0).GetComponent<SfxManager>();
        sfxManager.PlayButtonSound();

        GameObject go = EventSystem.current.currentSelectedGameObject;  // EventSystem.current.currentSelectedGameObject 마우스로 선택된 오브젝트(UI) 가져오기. // https://alpaca-code.tistory.com/139 참고.

        Text t = go.transform.parent.GetChild(2).GetChild(0).GetComponent<Text>();  // 현재 선택된 버튼이 조작하는 슬롯의 텍스트.
        int currentNum = int.Parse(t.text); // 텍스트의 정수문자를 정수로 변환. 
        currentNum = (currentNum + 1) % 10;    // 현재 정수값에 1을 더해주고 10으로 mod 연산. (9 -> 0 -> 1)

        t.text = currentNum.ToString(); // 1을 더해준 값을 다시 문자열로 변환하여 슬롯의 텍스트로 설정.

        CheckClear();   // 정답인지 확인.
    }

    // 해당 슬롯의 숫자를 하나 내리는 메소드.
    public void DownNumber()
    {
        sfxManager = GameManager.instance.gameObject.transform.GetChild(0).GetComponent<SfxManager>();
        sfxManager.PlayButtonSound();

        GameObject go = EventSystem.current.currentSelectedGameObject;  // EventSystem.current.currentSelectedGameObject 마우스로 선택된 오브젝트(UI) 가져오기.

        Text t = go.transform.parent.GetChild(2).GetChild(0).GetComponent<Text>();
        int currentNum = int.Parse(t.text);
        currentNum = (currentNum - 1 + 10) % 10;    // 현재 정수값에 1을 빼주고 10으로 mod 연산. (1 -> 0 -> 9)

        t.text = currentNum.ToString();

        CheckClear();
    }

    // 슬롯 값을 읽어와서 문자열로 만드는 메소드.
    public string GetSlotNumbers()
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < size; i++)
        {
            sb.Append(slotTexts[i].text); // StringBuilder 객체의 Append() 를 활용하여, 문자열을 연결해줌. string의 "+" 연산의 메모리 문제 방지.
        }
        
        return sb.ToString();   // 문자열로 변환하기 위해 ToString() 처리.
    }

    public override void InitPosSet()
    {
        // 조각이 없는 퍼즐이라 위치 초기화 필요 없음.
        // 따라서, overriding하여 내용을 구현하지는 않는다.
    }

    public override void CheckClear()
    {
        if (answer.CompareTo(GetSlotNumbers()) == 0)  // 질문의 답과 슬롯의 답 문자열이 같은지 비교하고, 같으면 정답 처리.
        {
            sfxManager.PlayPuzzleClearSound();

            SetClear(true);
        }
    }
}
