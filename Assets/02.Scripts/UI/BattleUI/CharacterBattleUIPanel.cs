using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBattleUIPanel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text characterNameText;
    [SerializeField]
    private TMP_Text characterLevelText;
    [SerializeField]
    private Image hpBarImage;
    [SerializeField]
    private Image mpBarImage;

    private int maxHP;
    private int maxMP;
    private Character currentCharacter;

    public void Init(Character character)
    {
        gameObject.SetActive(true);
        currentCharacter = character;
        maxHP = currentCharacter.maxHP;
        maxMP = currentCharacter.maxMP;
        characterLevelText.text = $"Lv.{character.Level}";
        characterNameText.text = $"{character.characterName}";
        HPBarValueChange(currentCharacter.HP);
        MPBarValueChange(currentCharacter.MP);
        currentCharacter.OnDamage.AddListener(HPBarValueChange);
        currentCharacter.OnUssSkill.AddListener(MPBarValueChange);
    }

    private void HPBarValueChange(int hp)
    {
        float value = (float)hp / maxHP;
        Debug.Log($"HPBarValue {hp} {value}");
        if(hp < 0.0f)
        {
            hp = 0;
        }
        hpBarImage.rectTransform.localScale = new Vector3(value, 1, 1);
    }

    private void MPBarValueChange(int mp)
    {
        float value = (float)mp / maxMP;
        if (mp < 0.0f)
        {
            mp = 0;
        }
        mpBarImage.rectTransform.localScale = new Vector3(value, 1, 1);
    }

    public void Release()
    {
        currentCharacter?.OnDamage.RemoveListener(HPBarValueChange);
        currentCharacter?.OnUssSkill.RemoveListener(MPBarValueChange);
        mpBarImage.rectTransform.localScale = Vector3.one;
        hpBarImage.rectTransform.localScale = Vector3.one;
        characterNameText.text = "";
        characterLevelText.text = "";
        maxMP = 0;
        maxHP = 0;
        currentCharacter = null;
        gameObject.SetActive(false);
    }
}
