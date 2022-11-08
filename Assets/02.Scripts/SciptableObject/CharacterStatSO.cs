using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Stat/CharacterStatData")]
public class CharacterStatSO : ScriptableObject
{
    public int maxHP;
    public int maxMP;
    public int AD;
    public int speed;
    public int defense;
    public int level;
}
