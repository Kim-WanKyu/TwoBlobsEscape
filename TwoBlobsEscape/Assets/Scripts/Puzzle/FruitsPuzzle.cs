using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsPuzzle : MonoBehaviour
{
    public bool IsClear { get; private set; }
    public void SetClear(bool value) { IsClear = value; }

    public GameObject PuzzlePosSet { get; private set; }
    public GameObject PuzzlePieceSet { get; private set; }
    public GameObject PuzzleMovingSet { get; private set; }

    public int[] DefaultPiecesPosSerial { get; private set; }
    public int[] DefaultPosSerial { get; private set; }

    private void Awake()
    {
        PuzzlePosSet = transform.GetChild(0).gameObject;
        PuzzlePieceSet = transform.GetChild(1).gameObject;
        PuzzleMovingSet = transform.GetChild(2).gameObject;

        DefaultPiecesPosSerial = new int[8];
        DefaultPosSerial = new int[8];

        RandomizeSerial(DefaultPiecesPosSerial);
        RandomizeSerial(DefaultPosSerial);

        // debug.
        string debugStrPieces = "";
        string debugStrPos = "";
        for (int i = 0; i < 8; i++)
        {
            debugStrPieces += (DefaultPiecesPosSerial[i] + ",");
            debugStrPos += (DefaultPosSerial[i] + ",");
        }
        Debug.Log($"debugStrPieces = {debugStrPieces}");
        Debug.Log($"debugStrPos = {debugStrPos}");

        InitPosSet();
    }

    // 주어진 배열의 길이만큼 0~n-1의 번호를 부여하고 랜덤으로 섞어주는 메소드.
    private void RandomizeSerial(int[] serial)
    {
        int len = serial.Length;
        for (int i = 0; i < len; i++)
        {
            serial[i] = i;
        }

        for (int i = 0; i < len; i++)
        {
            // 무작위 a, b 값을 가져옴. [0 ~ len-1]
            int a = UnityEngine.Random.Range(0, len);
            int b = UnityEngine.Random.Range(0, len);

            // 튜플을 이용하여 값 교환.
            (serial[b], serial[a]) = (serial[a], serial[b]);
        }
    }


    // 퍼즐 조각을 맞추는 영역의 위치를 번호에 맞게 초기화 하는 메소드.
    private void InitPosSet()
    {
        Vector2[] defaultPuzzlePos = { new Vector2(-300, 300), new Vector2(0, 300), new Vector2(300, 300), new Vector2(300, 0), new Vector2(300, -300), new Vector2(0, -300), new Vector2(-300, -300), new Vector2(-300, 0) };

        // 퍼즐 조각 위치 초기화.
        for (int i = 0; i < PuzzlePosSet.transform.childCount; i++)
        {
            PuzzlePosSet.transform.GetChild(i).GetComponent<RectTransform>().localPosition = defaultPuzzlePos[DefaultPosSerial[i]];
        }
    }


    // 퍼즐이 완성되었는 지 확인하는 메소드.
    public void CheckClear()
    {
        for (int i = 0; i < PuzzlePosSet.transform.childCount; i++)
        {
            //퍼즐위치의 자식이 없으면 모든 퍼즐조각이 놓여지지 않은것입니다.
            if (PuzzlePosSet.transform.GetChild(i).childCount == 0)
            {
                return;
            }
            //퍼즐조각의 번호와 퍼즐 위치 번호가 일치하지 않으면 퍼즐은 완성되지 않은것입니다.
            if (PuzzlePosSet.transform.GetChild(i).GetChild(0).GetComponent<FruitsPuzzlePiece>().PieceNum != i)
            {
                return;
            }
        }

        IsClear = true;
    }
}
