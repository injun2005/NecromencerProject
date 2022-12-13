using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelecetSkillPanel : MonoBehaviour
{
    [SerializeField]
    private List<SkillPanel> skillPanels = new List<SkillPanel>();

    private List<Skill> skills = new List<Skill>();

    private int skillPanelIdx = 0;
    public void SetSkillPanels(List<Skill> charSkillList)
    {
        foreach(SkillPanel panel in skillPanels)
        {
            panel.Release();
        } 

        skillPanelIdx = 0;
        foreach (Skill skill in charSkillList)
        {
            Debug.Log(skill.isActive + skill.skillKey.ToString());
            if(skill.isActive)
            {
                skillPanels[skillPanelIdx].Init(skill);
                skillPanelIdx++;
            }
        }
    }
}
