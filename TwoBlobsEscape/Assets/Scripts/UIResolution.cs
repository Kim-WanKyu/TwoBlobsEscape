using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 해상도를 1920x1080 으로 고정해주는 스크립트.
// '[Unity] 해상도에 따른 화면 비율 고정하기' https://giseung.tistory.com/19 참고.

// Main Camera에 넣음.
public class UIResolution : MonoBehaviour
{
    private int currentDeviceWidth;     // 현재 기기 너비.
    private int currentDeviceHeight;    // 현재 기기 높이.

    private void Start()
    {
        currentDeviceWidth = Screen.width; // 기기 너비 저장.
        currentDeviceHeight = Screen.height; // 기기 높이 저장.
        
        SetResolution(); // 초기에 게임 해상도 고정.
    }

    private void Update()
    {
        int deviceWidth = Screen.width; // 기기 너비 저장.
        int deviceHeight = Screen.height; // 기기 높이 저장.

        // 현재 기기의 너비나 높이가 변경되면,
        if (currentDeviceWidth != deviceWidth || currentDeviceHeight != deviceHeight)
        {
            // 변경된 너비/높이로 재설정하고,
            currentDeviceWidth = deviceWidth;
            currentDeviceHeight = deviceHeight;

            SetResolution(); // 창 크기 변경시에도 게임 해상도 고정.
        }
    }

    /* 해상도 설정하는 함수 */
    public void SetResolution()
    {
        int setWidth = 1920; // 사용자 설정 너비.
        int setHeight = 1080; // 사용자 설정 높이.

        Screen.SetResolution(setWidth, (int)(((float)currentDeviceHeight / currentDeviceWidth) * setWidth), true); // SetResolution 함수 제대로 사용하기.

        if ((float)setWidth / setHeight < (float)currentDeviceWidth / currentDeviceHeight) // 기기의 해상도 비가 더 큰 경우.
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)currentDeviceWidth / currentDeviceHeight); // 새로운 너비.
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // 새로운 Rect 적용.
        }
        else // 게임의 해상도 비가 더 큰 경우.
        {
            float newHeight = ((float)currentDeviceWidth / currentDeviceHeight) / ((float)setWidth / setHeight); // 새로운 높이.
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용.
        }
    }

    private void OnPreCull() => GL.Clear(true, true, Color.black);  // 원하는 부분만 rect를 이용해 비추고, 남은 부분을 검은색으로 채움.
}
