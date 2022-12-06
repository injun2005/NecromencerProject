using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Stat/StageDataSO")]

public class StageDataSO : ScriptableObject
{
    //stage 1개의 리스트
    public List<MonsterGroup> MosterGroupData; //몬스터 데이터
    public int Level; //단계
    public int RandomLevelOffset; // 렌덤 레벨 수치
    public int Compensation; // 보상
    public int MpHealOffset; // 마나 회복 수치

    [Serializable]
    public struct MonsterGroup
    {
        public List<Character> MosterData; //몬스터 데이터
    };
}
