using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ECharacterAction
{
    None = 0,
    Attack,
    Skill,
    Defence,
    Count
}
public enum ECharacterType
{
    None,
    Slime,
    Bomb,
    Shell,
    TurtleShell,
    Dragon,
    Snake,
    Poison,
    Blossom,
    Mboss,
    Boss,
    Count
}
public class Character : MonoBehaviour
{
    #region Stat
    [SerializeField]
    private CharacterStatSO statData;

    private int level;
    private int mp;
    private int hp;
    private int ad;
    private int speed;
    private int defence;

    public int MP { get { return mp; } }
    public int maxMP;
    public int HP { get { return hp; } }
    public int maxHP;
    public int AD { get { return ad; } }
    public int Speed { get { return speed; } }
    public int Defence { get { return defence; } }
    public int Level { get { return level; } }
    public string characterName { get { return statData.characterName; } }
    #endregion
    private int currentSkillIdx;
    private ECharacterAction currentActionIdx;
    public ECharacterType characterType;
    private Character target;
    public List<Skill> skillList;
   
    [HideInInspector] public bool isTeam = false;
    [HideInInspector] public bool isDead = false;
    [HideInInspector] public bool isSelcectAction = false; // 캐릭터 동작을 정했는가
    [HideInInspector] public bool isAction = false; // 캐릭터가 동작중인가

    public UnityEvent<int> OnDamage;
    public UnityEvent OnDie;
    public UnityEvent<int> OnUssSkill;
    public virtual void Init(int level)
    {
        //레벨에 따라 증가하는 수식이 있어야함
        SettingStat(level);

        foreach(Skill skill in skillList)
        {
            skill.Init(this);
        }
    }

    public void SettingStat(int level)
    {
        this.level = level;
        isDead = false;
        mp = statData.maxMP + statData.upMP * level;
        maxMP = mp;
        hp = statData.maxHP + statData.upHP * level;
        maxHP = hp;
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
        maxHP = hp;
        maxMP = mp;
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
        currentActionIdx = idx;
    }

    public void SetTarget(Character target)
    {
        this.target = target;
    }
    public void SetSkillIdx(ESkillKeys skillKey)
    {   
        currentSkillIdx = (int)skillKey;
    }
    public virtual void DoBehaviour()
    {
        isAction = true;
        CheckActionIdx();
    }

    public virtual void Attack(int damage)
    {
        isSelcectAction = false;
        isAction = false;
        target.Damaged(damage);
    }

    public virtual void PlaySkill()
    {
        foreach(Skill skill in skillList)
        {
            if((int)skill.skillKey == currentSkillIdx)
            {
                mp -= skill.limitMP;
                OnUssSkill?.Invoke(mp);
                skill.UseSkill(target);
            }
        }
        isSelcectAction = false;
    }

    public virtual void PlayDefecne()
    {
        isSelcectAction = false;
        isAction = false;
    }

    public virtual void Damaged(int damage)
    {
        hp -= damage;
        OnDamage.Invoke(hp);

        if (hp <= 0)
        {
            Dead();
        }
    }

    public virtual void Dead()
    {
        isDead = true;
        BattleSystem.Inst.BattleCharacterDead(this);
    }

    public virtual void MakedTeam()
    {
        isTeam = true;
    }
}
