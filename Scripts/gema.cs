﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gema : MonoBehaviour
{
    public int scoreGive = 1000;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Game.obj.addScore(scoreGive);

            AudioManager.obj.playCoin();

            UIManager.obj.updateScore();

            FXManager.obj.showPop(transform.position);


            gameObject.SetActive(false);

            Game.obj.gameOver();
        }
    }

}
