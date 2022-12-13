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
    [SerializeField]
    private List<CharacterBattleUIPanel> teamBattleUIList = new List<CharacterBattleUIPanel>();
    [SerializeField]
    private List<CharacterBattleUIPanel> enemyBattleUIList = new List<CharacterBattleUIPanel>();

    private List<Character> poolCharacters = new List<Character>();
    private List<Character> teamCharacters = new List<Character>();
    public List<Character> TeamCharacters { get { return teamCharacters; } }
    private List<Character> enemyCharacters = new List<Character>();
    private Player currentPlayer { get { return GameManager.Inst.CurrentPlayer; } }
    private List<Transform> enemyTrms = new List<Transform>(); //적 위치 맞춰주기
    private Queue<Character> characterQueue = new Queue<Character>(); 



    private bool isTurn = false;
    public bool IsTurn { get { return isTurn; } }

    private int teamDeathCount = 0;
    private int enemyDeathCount = 0;

    public void Awake()
    {
        CreatePool();

        EventManager.StartListening(EEvent.StartTurn, StartTurn);
        EventManager.StartListening(EEvent.EndTrun, EndTurn);
    }


    #region PoolSystem
    public Character Pop(string characterName, int level) // InBattle
    {
        foreach (Character character in poolCharacters)
        {
            if (characterName == character.characterName)
            {
                character.Init(level);
                character.gameObject.SetActive(true);
                poolCharacters.Remove(character);
                return character;
            }
        }
        Debug.LogError("characterName is Null, check characterName string or poolSystem");
        return null;
    }

    public void Push(Character character)
    {
        if(character.isTeam) 
        {
            teamCharacters.Remove(character);
        }
        else
        {
            enemyCharacters.Remove(character);
        }
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
        //스테이지에서 현재 전투 할 적 정보를 읽어온다음 여기에 넣어줘야함

        enemyCharacters.Add(Pop("Slime", 3)); 

        for (int i = 0; i < enemyCharacters.Count; i++)
        {
            //enemyCharacters[i].transform.position = enemyTrms[i].position; 
            currentPlayer.selectTargetCharacterUIList[i].Init(enemyCharacters[i]);
            enemyBattleUIList[i].Init(enemyCharacters[i]);
            
        }
        currentPlayer.BattleSetting();
        for(int i = 0; i < teamCharacters.Count; i++)
        {
            teamBattleUIList[i].Init(teamCharacters[i]);
        }
    }

    public void StartTurn(object dummyParam) 
    {
        isTurn = true;
        if(!currentPlayer.isDead)
        {
            currentPlayer.StartTurn();
        }
    }

    public void SetEnemyList(List<Character> characters)
    {
        enemyCharacters.Clear();
        enemyCharacters.AddRange(characters);
    }


    private IEnumerator EndTurnCorutine()
    {
        var list = new List<Character>();
        list.AddRange(teamCharacters);
        list.AddRange(enemyCharacters);

        list = list.OrderByDescending(x => x.Speed).ToList();

        foreach (Character character in list)
        {
            if (character.gameObject.activeSelf)
            {
                character.DoBehaviour();
                yield return new WaitUntil(() => { return !character.isAction; });
            }
        }

        EventManager.TriggerEvent(EEvent.StartTurn);
    }


    private void EndTurn(object dummyParam)
    {
        isTurn = false;

        StartCoroutine(EndTurnCorutine());
    }

    public void BattleCharacterDead(Character character)
    {
        if(character.isTeam)
        {
            teamDeathCount++;
            if(teamCharacters.Count >= teamDeathCount)
            {
                currentPlayer.isDead = true;
            }
        }
        else
        {
            enemyDeathCount++;
        }
        CheckEndBattle();
    }


    public void CheckEndBattle()
    {
        if(enemyDeathCount >= enemyCharacters.Count)
        {
            WinBattle();
        }
        else if(teamDeathCount >= teamCharacters.Count)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        EndBattle();
        GameManager.Inst.GameOver();
    }

    private void EndBattle()
    {
        BattleRelease();
    }
    public void BattleRelease()
    {
        teamCharacters.Clear();
        teamDeathCount = 0;

        foreach (CharacterBattleUIPanel ui in enemyBattleUIList)
        {
            ui.Release();
        }

        foreach (CharacterBattleUIPanel ui in teamBattleUIList)
        {
            ui.Release();
        }
    }
    public void WinBattle()
    {
        EndBattle();
        Debug.Log("승리");
        //만약 몬스터 그룹카운트가 남지 않았다면 StageClear 그렇지 안다면 다음 배틀로 
    }

    
}
