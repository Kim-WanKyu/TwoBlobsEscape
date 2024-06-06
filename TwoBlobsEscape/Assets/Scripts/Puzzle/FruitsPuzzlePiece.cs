using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;

public class FruitsPuzzlePiece : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public float snapOffset;
    private FruitsPuzzle puzzle;
    private GameObject puzzlePosSet;
    private GameObject puzzlePieceSet;
    private GameObject puzzleMovingSet;
    public int PieceNum { get; set; }

    private void Awake()
    {
        puzzle = transform.parent.parent.GetComponent<FruitsPuzzle>();
    }
    private void Start()
    {
        puzzlePosSet = puzzle.PuzzlePosSet;
        puzzlePieceSet = puzzle.PuzzlePieceSet;
        puzzleMovingSet = puzzle.PuzzleMovingSet;

        // 영역과 커서의 거리가 너비의 절반 이내이면 스냅되도록 수치 조정.
        snapOffset = puzzlePosSet.transform.GetChild(0).GetComponent<RectTransform>().rect.width / 2;

        // 퍼즐 번호 초기화.
        PieceNum = int.Parse(gameObject.name.Substring(11));    // 퍼즐 번호를 오브젝트의 이름에 맞게 초기화.

        InitPosPiece();
    }

    // 퍼즐 조각의 위치를 번호에 맞게 초기화 하는 메소드.
    private void InitPosPiece()
    {
        // 퍼즐 조각 위치 초기화.
        int defaultPosValue = puzzle.DefaultPiecesPosSerial[PieceNum];
        int posX, posY;

        posX = (defaultPosValue - (puzzle.DefaultPiecesPosSerial.Length / 2) >= 0 ? 715 : -715);
        posY = defaultPosValue % (puzzle.DefaultPiecesPosSerial.Length / 2);
        posY = (posY * 260) - 390;

        GetComponent<RectTransform>().localPosition = new Vector2(posX, posY);
    }

    // 드래그 시작.
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 지금 드래그하는 퍼즐 조각이 가장 위로 올 수 있도록 처리.
        transform.SetParent(puzzleMovingSet.transform);
    }

    // 드래그 중.
    public void OnDrag(PointerEventData eventData)
    {
        // 마우스 커서 위치 따라다니도록 처리.
        transform.position = eventData.position;
    }

    // 드래그 종료.
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!CheckSnapPuzzle())
        {
            // 퍼즐 조각을 스냅시키려는 위치에 조각이 있거나 퍼즐을 맞추는 위치가 아니면, 스냅 해제. (퍼즐 셋으로)
            transform.SetParent(puzzlePieceSet.transform);
        }
        else
        {
            // 퍼즐 완성 확인.
            puzzle.CheckClear();
        }

    }

    // 퍼즐 조각을 맞출 때(스냅할 때), 해당 위치에 이미 조각이 있는지 확인해서 스냅하는 메소드.
    bool CheckSnapPuzzle()
    {
        for (int i = 0; i < puzzlePosSet.transform.childCount; i++)
        {
            //위치에 자식오브젝트가 있으면 이미 퍼즐조각이 놓여진 것입니다.
            if (puzzlePosSet.transform.GetChild(i).childCount != 0)
            {
                continue;
            } // 자식 오브젝트가 없는 퍼즐pos의 위치를 확인하며 스냅오프셋보다 적으면, 스냅. (중심으로부터 snap 거리 이내이면, 스냅)
            else if (Vector2.Distance(puzzlePosSet.transform.GetChild(i).position, transform.position) < snapOffset)
            {
                transform.SetParent(puzzlePosSet.transform.GetChild(i).transform);
                transform.localPosition = Vector3.zero;
                return true;
            }
        }
        return false;
    }
}
