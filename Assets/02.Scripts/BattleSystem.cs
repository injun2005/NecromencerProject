using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[Serializable]
public struct monsterPoolData
{
    public Character prefab;
    public int cnt;
}

public class BattleSystem : MonoSingleton<BattleSystem>
{
    private int id = 0;

    [SerializeField]
    private List<monsterPoolData> monsterPools = new List<monsterPoolData>();
    [SerializeField]
    private Transform poolParentTrm;
    private List<Character> poolCharacters = new List<Character>();
    private List<Character> allActiveCharacters = new List<Character>();

    private Player currentPlayer;
    private Queue<Character> characterQueue = new Queue<Character>(); 
    private bool isTurn = false;
    public bool IsTurn { get { return isTurn; } }
    public void Awake()
    {
        CreatePool();

        EventManager.StartListening(EEvent.StartTurn, StartTurn);
        EventManager.StartListening(EEvent.EndTrun, EndTurn);
    }
    private void Start()
    {
        BattleStart();
    }

    public void AddNewPlayer(Player player)
    {
        currentPlayer = player;
    }
    #region PoolSystem
    public void Pop(string characterName, int level) // InBattle
    {
        foreach (Character character in poolCharacters)
        {
            if (characterName == character.characterName)
            {
                allActiveCharacters.Add(character);
                character.Init(level);
                poolCharacters.Remove(character);
                return;
            }
        }
        Debug.Log("characterName is Null, check characterName string or poolSystem");
    }

    public void Push(Character character)
    {
        allActiveCharacters.Remove(character);
        poolCharacters.Add(character);
    }

    private void CreatePool()
    {
        for(int i = 0; i < monsterPools.Count; i++)
        {
            for (int j = 0; j < monsterPools[i].cnt; j++)
            {
                Character character = Instantiate(monsterPools[i].prefab, poolParentTrm);
                character.gameObject.SetActive(false);
                poolCharacters.Add(character);
            }
        }
    }
    #endregion

    public void BattleStart()
    {
        Debug.Log("StartBattle");
        if(currentPlayer == null)
        {
            Debug.Log("Player is Null");
            return;
        }
        
        foreach(Character character in currentPlayer.teamCharacters)
        {
            Debug.Log($"{character.characterName} is add activeChar");
            allActiveCharacters.Add(character);
        }

        //스테이지에서 적정보를 읽어온다음 여기에 넣어줘야함


        EventManager.TriggerEvent(EEvent.StartTurn);



    }

    public void StartTurn(object dummyParam) 
    {
        isTurn = true;
        if(currentPlayer.isDead)
        {
            currentPlayer.StartTurn();
        }


    }

    public void EndTurn(object dummyParam)
    {
        isTurn = false;

        var list = allActiveCharacters.OrderByDescending(x => x.Speed).ToList();

        Character characterLast = null;
        foreach (Character character in list)
        {
            if (character.gameObject.activeSelf)
            {
                character.DoBehaviour();
                characterLast = character;
            }
        }
        //턴의 마지막 몬스터라는것을 알려줘야함
    }

    public void EndBattle()
    {
        
    }

    
}
