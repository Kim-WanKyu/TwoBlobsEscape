using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerSwapper playerSwapper;

    private GameObject playerBlue;
    private GameObject playerPink;

    private Rigidbody2D rigidBlue;
    private Rigidbody2D rigidPink;

    private SpriteRenderer spriteRendererBlue;
    private SpriteRenderer spriteRendererPink;

    private Animator animBlue;
    private Animator animPink;

    private float speed;
    private float jumpPower;

    private void Awake()
    {
        InitPlayerObject();

        speed = 7f;
        jumpPower = 20f;
    }

    private void InitPlayerObject()
    {
        playerSwapper = GetComponent<PlayerSwapper>();

        playerBlue = playerSwapper.GetPlayerBlue();
        playerPink = playerSwapper.GetPlayerPink();

        rigidBlue = playerBlue.GetComponent<Rigidbody2D>();
        rigidPink = playerPink.GetComponent<Rigidbody2D>();

        spriteRendererBlue = playerBlue.GetComponent<SpriteRenderer>();
        spriteRendererPink = playerPink.GetComponent<SpriteRenderer>();

        animBlue = playerBlue.GetComponent<Animator>();
        animPink = playerPink.GetComponent<Animator>();
    }

    private void Update()
    {
        Move();

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        // Direction Sprite
        if (Input.GetButton("Horizontal"))
        {
            if (playerSwapper.GetNowPlayer() == playerBlue)
            {
                spriteRendererBlue.flipX = Input.GetAxisRaw("Horizontal") == -1;
            }
            else if (playerSwapper.GetNowPlayer() == playerPink)
            {
                spriteRendererPink.flipX = Input.GetAxisRaw("Horizontal") == -1;
            }
        }

        // Animation walk
        if (Input.GetButton("Horizontal"))
        {
            if (playerSwapper.GetNowPlayer() == playerBlue)
            {
                animBlue.SetBool("isWalking", true);
            }
            else if (playerSwapper.GetNowPlayer() == playerPink)
            {
                animPink.SetBool("isWalking", true);
            }
        }
        else
        {
            if (playerSwapper.GetNowPlayer() == playerBlue)
            {
                animBlue.SetBool("isWalking", false);
            }
            else if (playerSwapper.GetNowPlayer() == playerPink)
            {
                animPink.SetBool("isWalking", false);
            }
        }
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        playerSwapper.GetNowPlayer().transform.Translate(new Vector2(h * speed, 0) * Time.deltaTime);
    }

    private void Jump()
    {
        playerSwapper.GetNowPlayer().GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }
}
