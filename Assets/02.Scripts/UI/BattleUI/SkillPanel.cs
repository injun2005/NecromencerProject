using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SkillPanel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text skillName;
    [SerializeField]
    private TMP_Text limitMPText;
    [SerializeField]
    private TMP_Text skillInfoText;
    [SerializeField]
    private GameObject skillInfoPanel;
    [SerializeField]
    private Button skillBtn;

    private ESkillKeys currentSkillkey;
    public void Init(Skill skill)
    {
        skillName.text = skill.skillKey.ToString();
        limitMPText.text = "MP: " + skill.limitMP.ToString();
        skillInfoText.text = skill.skillInfo;
        currentSkillkey = skill.skillKey;
        skillBtn.onClick.AddListener(OnSelectSkill);
    }

    private void OnSelectSkill()
    {
        Player.OnSelectSkill.Invoke(currentSkillkey);
    }

    public void Release()
    {
        skillName.text = "";
        limitMPText.text = "";
        skillInfoText.text = "";
    }
}
