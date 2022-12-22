using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ChangeToTeamPanel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text characterNameText;
    [SerializeField]
    private Button selectBtn;
    [SerializeField]
    private TMP_Text levelText;
    [SerializeField]
    private Character target;

    private Player player { get { return GameManager.Inst.CurrentPlayer; } }

    public void Init(Character character)
    {
        target = character;
        characterNameText.text = character.characterName;
        levelText.text = $"Lv.{character.Level}";
        selectBtn.onClick?.AddListener(SelectChacter);
        gameObject.SetActive(true);
    }
    public void Release()
    {
        target = null;
        characterNameText.text = "";
        levelText.text = "";
        selectBtn.onClick?.RemoveListener(SelectChacter);
    }
    public void SelectChacter()
    {
        if(!target.isTeam)
        {
            player.SetTeamChacter(target);
        }
    }
}
