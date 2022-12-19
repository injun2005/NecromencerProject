using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public struct MonsterGroup
{
    public List<MonsterData> MosterDatas; //몬스터 데이터
};

[Serializable]
public struct MonsterData
{
    public ECharacterType characterType;
    public int minLevel;
    public int maxLevel;
}
[CreateAssetMenu(menuName = "SO/Stat/StageDataSO")]

public class StageDataSO : ScriptableObject
{
    //stage 1개의 리스트
    public List<MonsterGroup> MosterGroupData; //몬스터 그룹에 있는 데이터
    public int Level; //단계
    public int RandomLevelOffset; // 렌덤 레벨 수치
    public int Compensation; // 보상
    public int MpHealOffset; // 마나 회복 수치


}
