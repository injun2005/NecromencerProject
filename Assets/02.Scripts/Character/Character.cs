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
    private int currentActionIdx;
    private Character target;

    public List<Skill> skillList;
    public bool isTeam = false;
    public bool isDead = false;
    public virtual void Init(int level)
    {
        //레벨에 따라 증가하는 수식이 있어야함
        mp = statData.maxMP + statData.upMP  *level;
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
            case 1:
                Attack(ad);
                break;
            case 2:
                PlaySkill();
                break;
        }
    }

    public virtual void DoBehaviour()
    {
        CheckActionIdx();
    }

    public virtual void Attack(int damage)
    {

    }

    public virtual void PlaySkill()
    {
        //currentSkillIdx 기반으로 움직임
    }

    public virtual void PlayDefecne()
    {

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
