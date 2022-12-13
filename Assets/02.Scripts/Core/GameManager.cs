using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Title,
    UI,
    FIGHT,
    STAGE,
}
public class GameManager : MonoSingleton<GameManager>
{
    private Player currentPlayer;
    public Player CurrentPlayer
    {
        get
        {
            return currentPlayer;
        }
    }

    public void AddNewPlayer(Player player)
    {
        currentPlayer = player;
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
    }
}
