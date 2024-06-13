using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Puzzle : MonoBehaviour
{
    protected SfxManager sfxManager;

    public bool IsClear { get; private set; }
    public void SetClear(bool value) { IsClear = value; }

    public GameObject PuzzlePosSet { get; protected set; }
    public GameObject PuzzlePieceSet { get; protected set; }
    public GameObject PuzzleMovingSet { get; protected set; }

    public int[] DefaultPiecesPosSerial { get; protected set; }
    public int[] DefaultPosSerial { get; protected set; }

    private void Start()
    {
        sfxManager = GameManager.instance.gameObject.transform.GetChild(0).GetComponent<SfxManager>();
    }

    // 주어진 배열의 길이만큼 0~n-1의 번호를 부여하고 랜덤으로 섞어주는 메소드.
    public void RandomizeSerial(int[] serial)
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


    // 퍼즐 조각을 맞추는 영역의 위치를 번호에 맞게 초기화 하는 메소드 (추상).
    public abstract void InitPosSet();


    // 퍼즐이 완성되었는 지 확인하는 메소드.
    public abstract void CheckClear();

}
