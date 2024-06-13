using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IPuzzlePiece : IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public int PieceNum { get; set; }

    // 퍼즐 조각의 위치를 번호에 맞게 초기화 하는 메소드.
    public void InitPosPiece();

    // 퍼즐 조각을 맞출 때(스냅할 때), 해당 위치에 이미 조각이 있는지 확인해서 스냅하는 메소드.
    public bool CheckSnapPuzzle();

}
