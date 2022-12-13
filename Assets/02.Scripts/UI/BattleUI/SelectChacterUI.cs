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

    public void Init(Character character)
    {
        target = character;
        characterNameText.text = character.characterName;
        selectBtn.onClick.AddListener(SelectChacter);

    }
    
    public void SelectChacter()
    {
        if (target.isTeam)
            Player.OnSelectTeam(target);
        else
            return;
    }

}
