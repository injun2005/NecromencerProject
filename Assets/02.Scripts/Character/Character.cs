using System;
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
    public string characterName { get { return statData.characterName; } }
    #endregion
    private int currentSkillIdx;
    private ECharacterAction currentActionIdx;
    private Character target;

    public List<Skill> skillList;
   
    [HideInInspector]public bool isTeam = false;
    [HideInInspector] public bool isDead = false;
    [HideInInspector] public bool isAction = false;

    public virtual void Init(int level)
    {
        //레벨에 따라 증가하는 수식이 있어야함

        SettingStat(level);

        foreach(Skill skill in skillList)
        {
            skill.Init(this);
        }
    }

    private void SettingStat(int level)
    {
        mp = statData.maxMP + statData.upMP * level;
        hp = statData.maxHP + statData.upHP * level;
        ad = statData.AD + statData.upAD * level;
        speed = statData.speed * statData.upSpeed * level;
        defence = statData.defence * statData.upDefence * level;
    }

    public virtual void Release()
    {
        mp = statData.maxMP;
        hp = statData.maxHP;
        ad = statData.AD;
        speed = statData.speed;
        defence = statData.defence;
    }
    private void CheckActionIdx()
    {
        switch (currentActionIdx)
        {
            case 0:
                break;
            case ECharacterAction.Attack:
                Attack(ad);
                break;
            case ECharacterAction.Skill:
                PlaySkill();
                break;
            case ECharacterAction.Defence:
                break;
        }
    }

    public void SetActionIdx(ECharacterAction idx)
    {
        isAction = true;
        currentActionIdx = idx;
    }

    public void SetSkillIdx(ESkillKeys skillKey)
    {
        isAction = true;
        currentSkillIdx = (int)skillKey;
    }
    public virtual void DoBehaviour()
    {
        CheckActionIdx();
    }

    public virtual void Attack(int damage)
    {
        isAction = false;
    }

    public virtual void PlaySkill()
    {
        foreach(Skill skill in skillList)
        {
            if((int)skill.skillKey == currentSkillIdx)
            {
                skill.UseSkill(target);
            }
        }
        isAction = false;
    }

    public virtual void PlayDefecne()
    {
        isAction = false;
    }

    public virtual void Damaged(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Dead();
        }
    }

    public virtual void Dead()
    {
        isDead = true;
    }

    public virtual void MakedTeam()
    {
        isTeam = true;
    }
}
