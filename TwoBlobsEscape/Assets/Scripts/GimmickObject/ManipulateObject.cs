using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 조작가능한 오브젝트를 조작하는 클래스.
public class ManipulateObject : MonoBehaviour
{
    private PlayerSwapper playerSwapper;

    private bool isControlling; // 조작중이면 true.

    private void Awake()
    {
        playerSwapper = GetComponent<PlayerSwapper>();
        isControlling = false;
    }

    private void Update()
    {
        // 상호작용키를 누르면, && 조작 중이 아니면,
        if (Input.GetKeyDown(KeySetting.keys[KeyAction.INTERACTION]) && isControlling == false)
        {
            ManipulateObj();
        }
    }

    // 조작 가능한 오브젝트 조작.
    private void ManipulateObj()
    {
        StartCoroutine(ManipulateObjCoroutine());
    }

    // 조작 가능한 오브젝트 조작 코루틴.
    private IEnumerator ManipulateObjCoroutine()
    {
        isControlling = true;

        // 현재 플레이어의 CollidedControllableObj 스크립트의 충돌중인 조작가능한 오브젝트들 리스트를 가져옴.
        LinkedList<GameObject> objCollider = playerSwapper.GetNowPlayer().GetComponent<CollidedControllableObj>().GetCollidedObjects();
        // 만약 현재 플레이어가 조작가능한 오브젝트와 하나 이상 충돌중이라면, null이 아니거나 카운트가 0보다 큼.
        if (objCollider != null || objCollider.Count > 0)
        {
            // 충돌중인 조작가능한 오브젝트 하나하나 반복하며.
            foreach (GameObject obj in objCollider)
            {
                // 해당 오브젝트의 IControlObject를 상속하는 컴포넌트를 모두 가져옴. (하나의 오브젝트에 여러 기능이 있을 수 있으므로 GetComponent's' (배열)).
                foreach (IControlObject controllableObj in obj.GetComponents<IControlObject>())
                {
                    // 가져온 모든 컴포넌트들 각각의 ControlObject()를 실행.
                    controllableObj.ControlObject();  // 해당 오브젝트의 각 기능을 조작하는 메소드 실행.
                }
            }
        }

        isControlling = false;

        yield break;
    }

}
