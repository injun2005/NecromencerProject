using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class SkillPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

    private Player player;
    private Skill skill;

    private ESkillKeys currentSkillkey;
    
    private void Awake()
    {
        skillBtn.onClick.AddListener(OnSelectSkill);
    }
    public void Init(Skill skill)
    {
        skillName.text = skill.skillKey.ToString();
        limitMPText.text = "MP: " + skill.limitMP.ToString();
        skillInfoText.text = skill.skillInfo;
        currentSkillkey = skill.skillKey;
        this.skill = skill;
        player ??= GameManager.Inst.CurrentPlayer;
    }

    private void OnSelectSkill()
    {
        if (skill == null) return;
        if (skill.isCanUse)
        {
            player.OnSelectSkill.Invoke(currentSkillkey);
        }
    }

    public void Release()
    {
        skillName.text = "";
        limitMPText.text = "";
        skillInfoText.text = "";
        skill = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        skillInfoPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        skillInfoPanel.SetActive(false);
    }
}
