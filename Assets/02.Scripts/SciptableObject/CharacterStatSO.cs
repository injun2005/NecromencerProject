using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Stat/CharacterStatData")]
public class CharacterStatSO : ScriptableObject
{
    public string characterName;
    public int maxHP;
    public int maxMP;
    public int AD;
    public int speed;
    public int defence;
    public int upHP;
    public int upMP;
    public int upSpeed;
    public int upDefence;
    public int upAD;
}