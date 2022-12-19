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
    public Character Target { get { return target; } }
    private Player player { get { return GameManager.Inst.CurrentPlayer; } }
    private bool isDead = false;
    public void Init(Character character)
    {
        target = character;
        characterNameText.text = character.characterName;
        selectBtn.onClick?.AddListener(SelectChacter);
        isDead = false;
    }
    
    public void SelectChacter()
    {
        if (target.isTeam)
            player.OnSelectTeam?.Invoke(target);
        else
            player.OnSelectTarget?.Invoke(target);
    }

    public void Dead()
    {
        isDead = true;
        gameObject.SetActive(false);
    }
}
