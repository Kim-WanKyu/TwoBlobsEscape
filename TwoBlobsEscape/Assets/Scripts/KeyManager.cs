using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=ODB3IOFqmrE  [유니티] 키를 유저 맘대로 설정하게 만들어주자!!KeySetting! 참고.

// 키 동작의 enum.
public enum KeyAction {
    LEFT = 0,       // 왼쪽.
    RIGHT,          // 오른쪽.
    JUMP,           // 점프.
    SWAP,           // 플레이어 스왑.
    INTERACTION ,   // 상호작용.
    KEYCOUNT        // 키 개수.
}

// 키 세팅 static 클래스. 키보드 세팅 값을 각 키 동작에 맞게 저장하는 클래스.
public static class KeySetting
{
    // 원하는 동작과 입력값을 딕셔너리로.
    public static Dictionary<KeyAction, KeyCode>keys = new Dictionary<KeyAction, KeyCode>();
}

public class KeyManager : MonoBehaviour
{
    // 디폴트 키 값.
    KeyCode[] defaultKeys = new KeyCode[] { 
        KeyCode.LeftArrow,  // 왼쪽. (왼쪽화살표(←))
        KeyCode.RightArrow, // 오른쪽. (오른쪽화살표(→))
        KeyCode.Space,      // 점프. (스페이스바)
        KeyCode.C,          // 플레이어 스왑. (C)
        KeyCode.F           // 상호작용. (F)
    };


    private void Awake()
    {
        // 초기에 키세팅을 디폴트 키값으로 초기화.
        for (int i = 0; i < (int)KeyAction.KEYCOUNT; i++)
        {
            KeySetting.keys.Add((KeyAction)i, defaultKeys[i]);
        }
    }

    // GUI, 키 입력 등의 이벤트 발생 시 호출.
    private void OnGUI()
    {
        // Event클래스로 현재 실행되는 이벤트 가져오기.
        Event keyEvent = Event.current;
        // 해당 이벤트가 키보드 이벤트이면.
        if (keyEvent.isKey)
        {
            // 이벤트의 KeyCode로 현재 눌린 키보드의 값 가져와서 keys에 넣어주기.
            KeySetting.keys[(KeyAction)key] = keyEvent.keyCode;
            key = -1;
        }
    }

    int key = -1;
    public void ChangeKey(int num)
    {
        key = num;
    }
}
