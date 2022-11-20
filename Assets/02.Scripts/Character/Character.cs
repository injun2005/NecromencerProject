using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECharacterAction
{
    None = 0,
    Attack,
    Skill,
    Defence,
}
public class Character : MonoBehaviour
{
    #region Stat
    [SerializeField]
    private CharacterStatSO statData;
    [SerializeField]
    private int level;
    private int mp;
    private int hp;
    private int ad;
    private int speed;
    private int defence;

    public int MP { get { return mp; } }
    public int HP { get { return hp; } }
    public int AD { get { return ad; } }
    public int Speed { get { return speed; } }
    public int Defence { get { return defence; } }
    public int Level { get { return level; } }
    #endregion
    private int currentSkillIdx;
    private int currentActionIdx;
    //public List<Skill> skillList;
    public bool isTeam = false;

    public virtual void StatSetting()
    {
        //������ ���� �����ϴ� ������ �־����
        mp = statData.maxMP; 
        hp = statData.maxHP;
        ad = statData.AD;
        speed = statData.speed;
        defence = statData.defence;
    }

    public void CheckActionIdx(Character actionTarget)
    {
        switch(currentActionIdx) 
        {
            case 0:
                break;
            case 1:
                Attack(actionTarget, ad);
                break;
            case 2:
                PlaySkill();
                break;
        }
    }

    public virtual void Attack(Character target, int damage)
    {

    }

    public virtual void PlaySkill()
    {
        //currentSkillIdx ������� ������
    }

    public virtual void PlayDefecne() 
    { 
    
    }

}
