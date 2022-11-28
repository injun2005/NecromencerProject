using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int maxSlave = 4;

    private int currentSlave = 0;
    [SerializeField]
    private int playerMp;
    [HideInInspector]
    public List<Character> teamCharacters = new List<Character>();
    [SerializeField]
    public List<SelectChacterUI> selectCharacterUIList = new List<SelectChacterUI>();
    public bool isDead;

    private void Awake()
    {
        
    }

    public void Init()
    {
        BattleSystem.Inst.AddNewPlayer(this);
    }

    public void BattleSetting()
    {
        for(int i= 0; i< teamCharacters.Count; i++)
        {
            selectCharacterUIList[i].Init(teamCharacters[i]);
        }
    }


}
