using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 자신과 충돌한 조작가능한 오브젝트를 관리(저장)하는 클래스.
public class CollidedControllableObj : MonoBehaviour
{
    private LinkedList<GameObject> collidedObjects;  // 충돌한 오브젝트들 리스트.
    public LinkedList<GameObject> GetCollidedObjects() { return collidedObjects; }

    // 처음에는 충돌된 오브젝트 리스트를 초기화.
    private void Awake()
    {
        collidedObjects = new LinkedList<GameObject>();
    }

    // collider(trigger) 가 접촉되면,
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("OnTriggerEnter2D : " + collision.name);
        // 충돌한 오브젝트에 IControlObject를 상속받는 컴포넌트가 있는지 확인해서 있으면,
        if (collision.TryGetComponent<IControlObject>(out _))
        {
            // '충돌된 오브젝트들 리스트'에 충돌한 해당 오브젝트를 추가.
            collidedObjects.AddLast(collision.gameObject);
        }
    }

    // collider(trigger) 가 접촉이 해제되면,
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Debug.Log("OnTriggerExit2D : " + collision.name);
        // 충돌 해제된 오브젝트가 리스트에 있으면, 리스트에서 지움.
        if (collidedObjects.Contains(collision.gameObject))
        {
            collidedObjects.Remove(collision.gameObject);
        }
    }
}
