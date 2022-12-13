using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SelectChacterUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text characterNameText;
    [SerializeField]
    private Button selectBtn;

    private Character target;
    private Player player { get { return GameManager.Inst.CurrentPlayer; } }
    
    public void Init(Character character)
    {
        target = character;
        characterNameText.text = character.characterName;
        selectBtn.onClick?.AddListener(SelectChacter);
    }
    
    public void SelectChacter()
    {
        if (target.isTeam)
            player.OnSelectTeam?.Invoke(target);
        else
            player.OnSelectTarget?.Invoke(target);
    }

}
