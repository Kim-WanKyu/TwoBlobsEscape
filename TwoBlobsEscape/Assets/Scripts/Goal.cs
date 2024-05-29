using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private GameObject Player;

    private bool isGoal;
    public bool GetIsGoal() { return isGoal; }

    void Start()
    {
        isGoal = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.name.CompareTo(Player.name) == 0)
            {
                isGoal = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.name.CompareTo(Player.name) == 0)
            {
                isGoal = false;
            }
        }
    }
}
