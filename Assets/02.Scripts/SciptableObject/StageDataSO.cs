using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Stat/CharacterStatData")]

public class StageDataSO : ScriptableObject
{
    public int MosterData; //몬스터 데이터
    public int Level; //레벨
    public int RandomLevelOffset; // 렌덤 레벨 수치
    public int Compensation; // 보상
    public int MpHealOffset; // 마나 회복 수치
}
