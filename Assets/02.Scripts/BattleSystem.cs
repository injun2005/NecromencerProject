using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityRandom = UnityEngine.Random;
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
    [SerializeField]
    private AISystem aiSystem;

    private List<Character> poolCharacters = new List<Character>();
    private List<Character> teamCharacters = new List<Character>();
    public List<Character> TeamCharacters { get { return teamCharacters; } }
    private List<Character> enemyCharacters = new List<Character>();
    private Player currentPlayer { get { return GameManager.Inst.CurrentPlayer; } }
    private List<Transform> enemyTrms = new List<Transform>(); //�� ��ġ �����ֱ�
    private StageManager stageManager;

    private bool isTurn = false;
    public bool IsTurn { get { return isTurn; } }
    private bool isEndBattle = false;
    public bool IsEndBattle { get { return isEndBattle; } }
    private int teamDeathCount = 0;
    private int enemyDeathCount = 0;

    public GameObject LodingScene;

    public GameObject StagesPrefabs;
    public GameObject Map;

    #region ���� �׷�(�������� ��������)
    private List<MonsterGroup> CurrentMonsterGroup = new List<MonsterGroup>();
    private int battleIdx = 0;
    #endregion

    StageManager st;

    public void Awake()
    {
        CreatePool();

        EventManager.StartListening(EEvent.StartTurn, StartTurn);
        EventManager.StartListening(EEvent.EndTrun, EndTurn);

    }


    #region PoolSystem
    public Character Pop(ECharacterType characterName, int level) // InBattle
    {
        foreach (Character character in poolCharacters)
        {
            if (characterName == character.characterType)
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

    public void SetStage(List<Transform> transforms, List<MonsterGroup> mobGroupDatas)
    {
        battleIdx = 0;
        enemyTrms = transforms;
        CurrentMonsterGroup = mobGroupDatas;

        BattleStart();
    }


    public void BattleStart()
    {
        isEndBattle = false;
        Debug.Log("StartBattle");
        if(currentPlayer == null)
        {
            Debug.Log("Player is Null");
            return;
        }

        enemyCharacters.Clear();
        foreach (var monster in CurrentMonsterGroup[battleIdx].MosterDatas)
        {
            Character character = Pop(monster.characterType, UnityRandom.Range(monster.minLevel, monster.maxLevel + 1));
            enemyCharacters.Add(character);
        }
        for (int i = 0; i < enemyCharacters.Count; i++)
        {
            enemyCharacters[i].transform.position = enemyTrms[i].position;
            currentPlayer.selectTargetCharacterUIList[i].Init(enemyCharacters[i]);
            currentPlayer.changeToTeamPanelUIList[i].Init(enemyCharacters[i]);
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
            if (!character.isDead)
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
        foreach (Character character in enemyCharacters)
        {
            if (!character.isDead)
            {
                aiSystem.SetCurrentCharacter(character);
                Character target = teamCharacters[UnityRandom.Range(0, teamCharacters.Count)];
                aiSystem.SetTarget(target);
                aiSystem.RandomAction();
            }
        }
        StartCoroutine(EndTurnCorutine());
    }

    public void BattleCharacterDead(Character character)
    {
        if(character.isTeam)
        {
            teamDeathCount++;
            GameManager.Inst.CurrentPlayer.DeadTeamCharacter(character);
            if(teamCharacters.Count < teamDeathCount)
            {
                currentPlayer.isDead = true;
            }
        }
        else
        {
            enemyDeathCount++;
            foreach(var ui in currentPlayer.selectTargetCharacterUIList)
            {
                if (ui.Target == character)
                {
                    ui.Dead();
                }
            }
        }
        CheckEndBattle();
    }


    public void CheckEndBattle()
    {
        if (enemyDeathCount >= enemyCharacters.Count)
        {
            BattleRelease();
            WinBattle();

        }
        else if(GameManager.Inst.CurrentPlayer.isDead)
        {
            BattleRelease();
            GameOver();
        }
        else
        {
            return;
        }
    }

    public void GameOver()
    {
        GameManager.Inst.GameOver();
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

    public void StageClear()
    {
        StagesPrefabs.SetActive(false);
        Map.SetActive(true);
        Debug.Log("�������� Ŭ����");
        StageManager.Inst.NextStage();
    }


    public void NextBattle()
    {
        StartCoroutine(NextBattleCoroutine());
    }

    public IEnumerator NextBattleCoroutine()
    {
        Debug.Log("�Լ� ���� nextBattle");
        LodingScene?.SetActive(true);
        yield return new WaitForSeconds(2f);
        LodingScene?.SetActive(false);
        BattleStart();
    }

    public void WinBattle()
    {
        Debug.Log("�¸�");   
        isEndBattle = true;
        GameManager.Inst.CurrentPlayer.ShowTargetPanelEndBattle();
    }
    public void CheckNextBattle()
    {
        Debug.Log("Check");
        battleIdx++;
        if (battleIdx >= CurrentMonsterGroup.Count)
        {
            StageClear();
        }
        else
        {
            NextBattle();
        }
        foreach(var enemy in enemyCharacters)
        {
            enemy.gameObject.SetActive(false);
        }
    }
}
