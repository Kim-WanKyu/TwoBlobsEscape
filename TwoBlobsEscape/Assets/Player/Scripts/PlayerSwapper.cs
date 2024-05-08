using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSwapper : MonoBehaviour
{
    [SerializeField] private GameObject playerBlue; //ÆÄ¶û.
    [SerializeField] private GameObject playerPink;
    private GameObject nowPlayer;
    public GameObject GetPlayerBlue() { return playerBlue; }
    public GameObject GetPlayerPink() { return playerPink; }
    public GameObject GetNowPlayer() { return nowPlayer; }

    private Color activatedColor;
    private Color deactivatedColor;

    // Start is called before the first frame update
    void Awake()
    {
        activatedColor = new Color(1, 1, 1);
        deactivatedColor = new Color(100 / 255f, 100 / 255f, 100 / 255f);

        playerBlue.GetComponent<SpriteRenderer>().color = activatedColor;
        playerPink.GetComponent<SpriteRenderer>().color = deactivatedColor;

        nowPlayer = playerBlue;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            // cancel walking animation to previous player.
            nowPlayer.GetComponent<Animator>().SetBool("isWalking", false);
            // change previous player's color to deactivated color.
            nowPlayer.GetComponent<SpriteRenderer>().color = deactivatedColor;

            // swap player.
            nowPlayer = nowPlayer == playerBlue ? playerPink : playerBlue;

            // change current player's color to activated color.
            nowPlayer.GetComponent<SpriteRenderer>().color = activatedColor;
        }
    }
}
