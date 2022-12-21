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
    [SerializeField]
    private ESCPanel escPanel;
    [SerializeField]
    private GameObject gameOverPanel;
    public void AddNewPlayer(Player player)
    {
        currentPlayer = player;
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        gameOverPanel.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!escPanel.isOpen)
            {
                escPanel.OnPanel();
            }
            else
            {
                escPanel.OffPanel();
            }
        }
    }
}
