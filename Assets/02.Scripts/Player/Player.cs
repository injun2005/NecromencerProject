using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Action<Character> OnSelectTeam;
    public static Action<ESkillKeys> OnSelectSkill;
    public static Action<ECharacterAction> OnSelectAction;

    [SerializeField]
    private int maxSlave = 4;
    private int currentSlave = 0;

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
    public List<SelectChacterUI> selectTeamCharacterUIList = new List<SelectChacterUI>();
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

        Init();
    }
    private void Update()
    {
        if(BattleSystem.Inst.IsTurn)
        {
            if(teamCharacters.Count >= teamCharacters.Count)
            {
                EventManager.TriggerEvent(EEvent.EndTrun);
            }
        }
    }
    public void Init()
    {
        BattleSystem.Inst.AddNewPlayer(this);
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
    #endregion
    public void BattleSetting()
    {
        for(int i= 0; i< teamCharacters.Count; i++)
        {
            selectTeamCharacterUIList[i].Init(teamCharacters[i]);
        }
        StartTurn();
    }

    public void StartTurn()
    {
        completeSelectCnt = 0;
        ShowTeamPanel();
    }
    
    public void AddTeam(Character character) 
    {
        if(maxSlave < teamCharacters.Count)
        {
            return;
        }
        teamCharacters.Add(character);
        character.isTeam = true;

        character.Init(character.Level);
    }
    
    public void SelectTeamActionChar(Character team)
    {
        Debug.Log($"SelectTeam {team.characterName}");
        currentActionChar = team;
        ShowActionPanel();
    }

    public void SelectActionIdx(ECharacterAction actionIdx)
    {
        if (actionIdx == ECharacterAction.Skill)
        {
            ShowSkillPanel();
            return;
        }
        currentActionChar.SetActionIdx(actionIdx);
        ShowTeamPanel();
    }

    public void SelectSkillIdx(ESkillKeys skillKey) 
    {
        currentActionChar.SetSkillIdx(skillKey);
    }


}
