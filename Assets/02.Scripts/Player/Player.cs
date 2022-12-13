using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public  Action<Character> OnSelectTeam;
    public  Action<ESkillKeys> OnSelectSkill;
    public  Action<ECharacterAction> OnSelectAction;
    public Action<Character> OnSelectTarget;
    [SerializeField]
    private int maxTeamCnt = 4;
    private int currentTeamCnt = 0;

    [SerializeField]
    private int playerMp;
    [SerializeField]
    private GameObject teamSelectPanel;
    [SerializeField]
    private GameObject targetSelectPanel;
    [SerializeField]
    private GameObject actionSelectPanel;
    [SerializeField]
    private SelecetSkillPanel skillSelectPanel;
    [HideInInspector]
    public List<Character> teamCharacters = new List<Character>();
    [SerializeField]
    private List<SelectChacterUI> selectTeamCharacterUIList = new List<SelectChacterUI>();
    public List<SelectChacterUI> selectTargetCharacterUIList = new List<SelectChacterUI>();
    [SerializeField]
    public List<Transform> teamUnitPos = new List<Transform>();

    public bool isDead;
    private int completeSelectCnt = 0;
    private Character currentActionChar;
    private Character currentActionTarget;
    private void Awake()
    {
        OnSelectTeam += SelectTeamActionChar;
        OnSelectSkill += SelectSkillIdx;
        OnSelectAction += SelectActionIdx;
        OnSelectTarget += SelectTarget;
        Init();
    }
    private void Update()
    {

    }
    public void Init()
    {
        GameManager.Inst.AddNewPlayer(this);
    }
    #region Panel Hide/Show
    public void HideAllSelectPanel()
    {
        teamSelectPanel.gameObject.SetActive(false);
        targetSelectPanel.gameObject.SetActive(false);
        actionSelectPanel.gameObject.SetActive(false);
        skillSelectPanel.gameObject.SetActive(false);
    }
    public void ShowTeamPanel()
    {
        HideAllSelectPanel();
        teamSelectPanel.gameObject.SetActive(true);
    }
    public void ShowActionPanel()
    {
        HideAllSelectPanel();
        actionSelectPanel.gameObject.SetActive(true);
    }
    public void ShowSkillPanel()
    {
        HideAllSelectPanel();
        skillSelectPanel.SetSkillPanels(currentActionChar.skillList);
        skillSelectPanel.gameObject.SetActive(true);
    }
    public void ShowTargetPaenl()
    {
        HideAllSelectPanel();
        targetSelectPanel.gameObject.SetActive(true);
    }
    #endregion
    public void BattleSetting()
    {
        for (int i= 0; i< teamCharacters.Count; i++)
        {
            BattleSystem.Inst.TeamCharacters.Add(teamCharacters[i]);
            teamCharacters[i].transform.position = teamUnitPos[i].position;
            selectTeamCharacterUIList[i].Init(teamCharacters[i]);
        }

        EventManager.TriggerEvent(EEvent.StartTurn);
    }

    public void StartTurn()
    {
        completeSelectCnt = 0;
        foreach(Character character in teamCharacters)
        {
            character.isSelcectAction = false;
            character.isAction = false;
        }
        ShowTeamPanel();
    }
    
    public void AddTeam(Character character) 
    {
        if(maxTeamCnt < teamCharacters.Count)
        {
            Debug.LogError("ÆÀ¿ø ¼ö°¡ ÃÊ°úµÊ");
            return;
        }
        teamCharacters.Add(character);
        character.isTeam = true;

        character.SettingStat(character.Level + 2);
    }
    
    public void SelectTeamActionChar(Character team)
    {
        Debug.Log($"SelectTeam {team.characterName}");
        currentActionChar = team;
        ShowActionPanel();
    }
    public void SelectTarget(Character target)
    {
        currentActionChar.SetTarget(target);
        if(!currentActionChar.isSelcectAction)
        {
            currentActionChar.isSelcectAction = true; 
            completeSelectCnt++;
        }

        CheckEndTrun();
    }
    public void SelectActionIdx(ECharacterAction actionIdx)
    {
        currentActionChar.SetActionIdx(actionIdx);
        if (actionIdx == ECharacterAction.Skill)
        {
            ShowSkillPanel();
            return;
        }
        if(actionIdx == ECharacterAction.Attack)
        {
            ShowTargetPaenl();
            return;
        }
        if (!currentActionChar.isSelcectAction)
        {
            currentActionChar.isSelcectAction = true;
            completeSelectCnt++;
        }
        CheckEndTrun();
    }

    public void SelectSkillIdx(ESkillKeys skillKey) 
    {
        currentActionChar.SetSkillIdx(skillKey);

        ShowTargetPaenl();
    }

    private void CheckEndTrun()
    {
        if (completeSelectCnt >= teamCharacters.Count)
        {
            EndTrun();
        }
        else
        {
            ShowTeamPanel();   
        }
    }

    private void EndTrun()
    {
        HideAllSelectPanel();
        EventManager.TriggerEvent(EEvent.EndTrun);
    }

    public void SetTeamChacter(Character character)
    {
        character.SettingStat(character.Level + 2);
        character.isTeam = true;
    } 
}
