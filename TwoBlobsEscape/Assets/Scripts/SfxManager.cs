using System.Collections.Generic;
using UnityEngine;

// 효과음 오디오소스(클립) 이름들 enum.
public enum ESfxName
{
    /*플레이어 관련*/
    Walk/*걷기*/,
    Jump/*점프*/,
    Death/*죽음*/,
    Swap/*스왑*/,

    /*오브젝트 관련*/
    Button/*버튼*/,
    Switch/*스위치*/,
    DoorOpen/*문열기*/,
    DoorClose/*문닫기*/,

    /*장면 관련*/
    StageStart/*스테이지 시작*/,
    StageClear/*스테이지 클리어*/,

    /*퍼즐 관련*/
    PuzzleClear/*퍼즐 클리어*/,
    PuzzlePieceDrag/*퍼즐 조각 드래그(집었을 때)*/,
    PuzzlePieceDrop/*퍼즐 조각 드랍(놓았을 때)*/,
    PuzzlePieceSnap/*퍼즐 조각 스냅(맞췄을 때)*/,

    /*UI 관련*/
    UIButtonClick/*UI 버튼 클릭*/,
    UIWindowOn/*UI 창 띄우기(켜기)*/, 
    UIWindowOff/*UI 창 접기(끄기)*/,
}

public class SfxManager : MonoBehaviour
{
    private AudioSource sfxAudioSource;     // 효과음 재생을 위한 오디오소스.(SfxManager)
    private AudioSource playerWalkAudioSource;  // 플레이어 걷기 효과음 재생을 위한 오디오소스.(SfxManager)

    public Dictionary<ESfxName, AudioClip> SfxDict { get; private set; }

    private ESfxName[] sfxNameList;
    private AudioClip[] sfxClipList;


    private int listLength;

    [Header("[플레이어 관련]")]
    [SerializeField] private AudioClip walkAudioClip;
    [SerializeField] private AudioClip jumpAudioClip;
    [SerializeField] private AudioClip deathAudioClip;
    [SerializeField] private AudioClip swapAudioClip;

    [Header("[오브젝트 관련]")]
    [SerializeField] private AudioClip buttonAudioClip;
    [SerializeField] private AudioClip switchAudioClip;
    [SerializeField] private AudioClip doorOpenAudioClip;
    [SerializeField] private AudioClip doorCloseAudioClip;

    [Header("[장면 관련]")]
    [SerializeField] private AudioClip stageStartAudioClip;
    [SerializeField] private AudioClip stageClearAudioClip;

    [Header("[퍼즐 관련]")]
    [SerializeField] private AudioClip puzzleClearAudioClip;
    [SerializeField] private AudioClip puzzlePieceDragAudioClip;
    [SerializeField] private AudioClip puzzlePieceDropAudioClip;
    [SerializeField] private AudioClip PuzzlePieceSnapAudioClip;

    [Header("[UI 관련]")]
    [SerializeField] private AudioClip uiButtonClickAudioClip;
    [SerializeField] private AudioClip uiWindowOnAudioClip;
    [SerializeField] private AudioClip uiWindowOffAudioClip;

    public void Awake()
    {
        sfxAudioSource = GetComponent<AudioSource>();
        playerWalkAudioSource = transform.GetChild(0).GetComponent<AudioSource>();

        // enum인 EAudioSourceName의 값 배열을 가져와 길이를 구함.
        listLength = ((ESfxName[])System.Enum.GetValues(typeof(ESfxName))).Length;

        SfxDict = new Dictionary<ESfxName, AudioClip>();
        sfxNameList = new ESfxName[listLength];
        sfxClipList = new AudioClip[listLength];

        Init();
    }

    // AudioNameList와 AudioDict 초기화 메소드.
    public void Init()
    {
        InitAudioClipList();

        // AudioNameList를 위에 선언한 EAudioSourceName 의 값들을 가져와서 저장.
        sfxNameList = (ESfxName[])System.Enum.GetValues(typeof(ESfxName));

        // for문을 돌면서, EAudioSourceName의 이름과 AudioClipList의 오디오 소스를 매칭시켜 audioDict로 저장.
        for (int i = 0; i < listLength; i++)
        {
            SfxDict[sfxNameList[i]] = sfxClipList[i]; // audioDict 초기화.
        }

        playerWalkAudioSource.clip = SfxDict[ESfxName.Walk];
    }

    public void InitAudioClipList()
    {
        sfxClipList[(int)ESfxName.Walk] = walkAudioClip;
        sfxClipList[(int)ESfxName.Jump] = jumpAudioClip;
        sfxClipList[(int)ESfxName.Death] = deathAudioClip;
        sfxClipList[(int)ESfxName.Swap] = swapAudioClip;

        sfxClipList[(int)ESfxName.Button] = buttonAudioClip;
        sfxClipList[(int)ESfxName.Switch] = switchAudioClip;
        sfxClipList[(int)ESfxName.DoorOpen] = doorOpenAudioClip;
        sfxClipList[(int)ESfxName.DoorClose] = doorCloseAudioClip;

        sfxClipList[(int)ESfxName.StageStart] = stageStartAudioClip;
        sfxClipList[(int)ESfxName.StageClear] = stageClearAudioClip;

        sfxClipList[(int)ESfxName.PuzzleClear] = puzzleClearAudioClip;
        sfxClipList[(int)ESfxName.PuzzlePieceDrag] = puzzlePieceDragAudioClip;
        sfxClipList[(int)ESfxName.PuzzlePieceDrop] = puzzlePieceDropAudioClip;
        sfxClipList[(int)ESfxName.PuzzlePieceSnap] = PuzzlePieceSnapAudioClip;

        sfxClipList[(int)ESfxName.UIButtonClick] = uiButtonClickAudioClip;
        sfxClipList[(int)ESfxName.UIWindowOn] = uiWindowOnAudioClip;
        sfxClipList[(int)ESfxName.UIWindowOff] = uiWindowOffAudioClip;
    }


    // 효과음 실행 메소드들.


    // 플레이어 관련

    // playerWalkAudioManager의 오디오 소스에 해당 사운드 실행하는 메소드.
    // 걷기 사운드 실행 메소드.
    public void PlayWalkSound()
    {
        if (!playerWalkAudioSource.isPlaying)
        {
            playerWalkAudioSource.Play();
        }
    }
    // 걷기 사운드 종료 메소드.
    public void StopWalkSound()
    {
        if (playerWalkAudioSource.isPlaying)
        {
            playerWalkAudioSource.Stop();
        }
    }

    // AudioManager의 오디오 소스에 해당 사운드 실행하는 메소드.
    // 점프 사운드 실행 메소드.
    public void PlayJumpSound()
    {
        sfxAudioSource.PlayOneShot(SfxDict[ESfxName.Jump]);
    }

    // 죽음 사운드 실행 메소드.
    public void PlayDeathSound()
    {
        sfxAudioSource.PlayOneShot(SfxDict[ESfxName.Death]);
    }

    // 스왑 사운드 실행 메소드.
    public void PlaySwapSound()
    {
        sfxAudioSource.PlayOneShot(SfxDict[ESfxName.Swap]);
    }


    // 오브젝트 관련
    public void PlayButtonSound()
    {
        sfxAudioSource.PlayOneShot(SfxDict[ESfxName.Button]);
    }
    public void PlaySwitchSound()
    {
        sfxAudioSource.PlayOneShot(SfxDict[ESfxName.Switch]);
    }
    public void PlayDoorOpenSound()
    {
        sfxAudioSource.PlayOneShot(SfxDict[ESfxName.DoorOpen]);
    }
    public void PlayDoorCloseSound()
    {
        sfxAudioSource.PlayOneShot(SfxDict[ESfxName.DoorClose]);
    }


    // 장면 관련

    // 스테이지시작 사운드 실행 메소드.
    public void PlayStageStartSound()
    {
        sfxAudioSource.PlayOneShot(SfxDict[ESfxName.StageStart]);
    }

    // 스테이지클리어 사운드 실행 메소드.
    public void PlayStageClearSound()
    {
        sfxAudioSource.PlayOneShot(SfxDict[ESfxName.StageClear]);
    }


    // 퍼즐 관련

    // 퍼즐클리어 사운드 실행 메소드.
    public void PlayPuzzleClearSound()
    {
        sfxAudioSource.PlayOneShot(SfxDict[ESfxName.PuzzleClear]);
    }

    // 퍼즐조각드래그(집었을 때) 사운드 실행 메소드.
    public void PlayPuzzlePieceDragSound()
    {
        sfxAudioSource.PlayOneShot(SfxDict[ESfxName.PuzzlePieceDrag]);
    }

    // 퍼즐조각드랍(놓았을 때) 사운드 실행 메소드.
    public void PlayPuzzlePieceDropSound()
    {
        sfxAudioSource.PlayOneShot(SfxDict[ESfxName.PuzzlePieceDrop]);
    }

    // 퍼즐조각스냅(맞췄을 때) 사운드 실행 메소드.
    public void PlayPuzzlePieceSnapSound()
    {
        sfxAudioSource.PlayOneShot(SfxDict[ESfxName.PuzzlePieceSnap]);
    }


    // UI 관련

    // UI버튼클릭 사운드 실행 메소드.
    public void PlayUIButtonClickSound()
    {
        sfxAudioSource.PlayOneShot(SfxDict[ESfxName.UIButtonClick]);
    }

    // UI 창 띄우기(켜기) 사운드 실행 메소드.
    public void PlayUIWindowOnSound()
    {
        sfxAudioSource.PlayOneShot(SfxDict[ESfxName.UIWindowOn]);
    }

    // UI 창 접기(끄기) 사운드 실행 메소드.
    public void PlayUIWindowOffSound()
    {
        sfxAudioSource.PlayOneShot(SfxDict[ESfxName.UIWindowOff]);
    }
}
