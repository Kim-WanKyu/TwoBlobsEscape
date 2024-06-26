using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FruitsPuzzle : Puzzle
{
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

    // 퍼즐 조각을 맞추는 영역의 위치를 번호에 맞게 초기화 하는 메소드.
    public override void InitPosSet()
    {
        Vector2[] defaultPuzzlePos = { new Vector2(-300, 300), new Vector2(0, 300), new Vector2(300, 300), new Vector2(300, 0), new Vector2(300, -300), new Vector2(0, -300), new Vector2(-300, -300), new Vector2(-300, 0) };

        // 퍼즐 조각 위치 초기화.
        for (int i = 0; i < PuzzlePosSet.transform.childCount; i++)
        {
            PuzzlePosSet.transform.GetChild(i).GetComponent<RectTransform>().localPosition = defaultPuzzlePos[DefaultPosSerial[i]];
        }
    }

    public override void CheckClear()
    {
        for (int i = 0; i < PuzzlePosSet.transform.childCount; i++)
        {
            //퍼즐위치의 자식이 없으면 모든 퍼즐조각이 놓여지지 않은것입니다.
            if (PuzzlePosSet.transform.GetChild(i).childCount == 0)
            {
                return;
            }
            //퍼즐조각의 번호와 퍼즐 위치 번호가 일치하지 않으면 퍼즐은 완성되지 않은것입니다.
            if (PuzzlePosSet.transform.GetChild(i).GetChild(0).GetComponent<IPuzzlePiece>().PieceNum != i)
            {
                return;
            }
        }

        sfxManager.PlayPuzzleClearSound();

        SetClear(true);
    }
}
