using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

// 유니티 2D 마우스 드래그 라인 만들기  https://blog.naver.com/dooya-log/221431250246 참고.
public class LineMatching : MonoBehaviour
{
    private GameObject originObj;   // PuzzlePiecePos.
    private GameObject targetObj;   // PuzzlePiece.

    private Vector2 originPos;      // originObj.position.
    private Vector2 targetPos;      // targetObj.position.

    private RectTransform myRectTransform;

    public int PieceNum { get; set; }

    private void Awake()
    {
        myRectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        PieceNum = int.Parse(gameObject.name.Substring(15));    // 퍼즐 번호를 오브젝트의 이름에 맞게 초기화.

        // originObj 및 originPos 초기화.
        originObj = transform.parent.gameObject;

        // targetObj 초기화.
        targetObj = transform.parent.GetChild(1).gameObject;

        // 현재 line의 RectTransform 의 기준점을 초기화.
        myRectTransform.anchoredPosition = Vector2.zero;

        // 연결선을 초기화해주기 위해 MatchLine() 수행.
        MatchLine();
    }

    public void MatchLine()
    {
        // 기준 오브젝트의 위치를 가져옴.
        originPos = originObj.GetComponent<RectTransform>().position;
        // 퍼즐 조각 오브젝트의 위치를 가져옴.
        targetPos = targetObj.GetComponent<RectTransform>().position;

        // 기준 오브젝트와 퍼즐 조각의 위치를 통해 두 사이의 거리를 계산.
        float distance = Vector2.Distance(originPos, targetPos);

        // 줄의 너비를 두 사이의 거리만큼 늘려주고,
        myRectTransform.sizeDelta = new Vector2(distance, myRectTransform.rect.height);
        // 기준 오브젝트와 퍼즐 조각의 각도만큼 회전시켜줌.
        myRectTransform.transform.localRotation = Quaternion.Euler(0, 0, AngleInDeg(originPos, targetPos));
        // => 두 오브젝트 사이에 선이 연결된 것처럼 보임. 
    }

    // 두 오브젝트 간의 각도(도) 를 구하는 메소드.
    private static float AngleInDeg(Vector2 vec1, Vector2 vec2)
    {
        return Mathf.Atan2(vec2.y - vec1.y, vec2.x - vec1.x) * 180 / Mathf.PI;
    }
}
