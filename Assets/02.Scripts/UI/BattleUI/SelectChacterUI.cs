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
    [SerializeField]
    private TMP_Text levelText;
    private Character target;
    public Character Target { get { return target; } }
    private Player player { get { return GameManager.Inst.CurrentPlayer; } }
    private bool isDead = false;
    private Image backgroundImage;

    private void Awake()
    {
    }
    public void Init(Character character)
    {
        gameObject.SetActive(true);
        backgroundImage ??= GetComponent<Image>();
        target = character;
        characterNameText.text = character.characterName;
        selectBtn.onClick?.AddListener(SelectChacter);
        isDead = false;
        levelText.text = $"Lv.{character.Level}";
        backgroundImage.color = new Color(0.65f, 0.65f, 0.65f, 0.7f);
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
        characterNameText.text = "";
        levelText.text = "";
        selectBtn.onClick.RemoveListener(SelectChacter);
        backgroundImage.color = new Color(0.65f, 0.65f, 0.65f, 0.5f);
    }

}
