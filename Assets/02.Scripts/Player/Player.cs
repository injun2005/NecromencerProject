using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField]
    private GameObject changeToTeamPanel;
    [HideInInspector]
    public List<Character> teamCharacters = new List<Character>();
    [SerializeField]
    private List<SelectChacterUI> selectTeamCharacterUIList = new List<SelectChacterUI>();
    public List<SelectChacterUI> selectTargetCharacterUIList = new List<SelectChacterUI>();
    public List<ChangeToTeamPanel> changeToTeamPanelUIList = new List<ChangeToTeamPanel>();
    [SerializeField]
    public List<Transform> teamUnitPos = new List<Transform>();
    [SerializeField]
    private Button changeToTeamNextBtn;
    public bool isDead;
    private int completeSelectCnt = 0;
    private Character currentActionChar;
    private Character currentActionTarget;
    [Header("ChangeToTeam")]
    [SerializeField]
    private int useMP;

    private void Awake()
    {
        OnSelectTeam += SelectTeamActionChar;
        OnSelectSkill += SelectSkillIdx;
        OnSelectAction += SelectActionIdx;
        OnSelectTarget += SelectTarget;
        changeToTeamNextBtn.onClick.AddListener(PassTeamCharacter);
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
        Debug.Log("Hide");
        teamSelectPanel.gameObject.SetActive(false);
        targetSelectPanel.gameObject.SetActive(false);
        actionSelectPanel.gameObject.SetActive(false);
        skillSelectPanel.gameObject.SetActive(false);
        changeToTeamPanel.gameObject.SetActive(false);
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
    public void DeadTeamCharacter(Character character)
    {
        teamCharacters.Remove(character);
    }
    public void ShowTargetPaenl()
    {
        HideAllSelectPanel();
        targetSelectPanel.gameObject.SetActive(true);
    }
    public void ShowTargetPanelEndBattle()
    {
        HideAllSelectPanel();
        Debug.Log("ShowPanel");
        changeToTeamPanel.gameObject.SetActive(true);
    }
    #endregion
    public void BattleSetting()
    {
        for (int i= 0; i< teamCharacters.Count; i++)
        {
            BattleSystem.Inst.TeamCharacters.Add(teamCharacters[i]);
            teamCharacters[i].transform.position = teamUnitPos[i].position;
            teamCharacters[i].transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
            selectTeamCharacterUIList[i].Init(teamCharacters[i]);
        }
        ShowTeamPanel();
        EventManager.TriggerEvent(EEvent.StartTurn);
    }

    public void StartTurn()
    {
        if (BattleSystem.Inst.IsEndBattle) return;
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
            Debug.LogError("���� ���� �ʰ���");
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
            if(!BattleSystem.Inst.IsEndBattle)
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
        if(playerMp - useMP >= 0)
        {
            playerMp -= useMP;
            character.SettingStat(character.Level + 2);
            character.isTeam = true;
            AddTeam(character);
            HideAllSelectPanel();
            BattleSystem.Inst.CheckNextBattle();
        }
    }
    public void PassTeamCharacter()
    {
        HideAllSelectPanel();
        BattleSystem.Inst.CheckNextBattle();

    }
}
