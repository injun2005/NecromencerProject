using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Stat/StageDataSO")]

public class StageDataSO : ScriptableObject
{
    //stage 1���� ����Ʈ
    public List<MonsterGroup> MosterGroupData; //���� ������
    public int Level; //�ܰ�
    public int RandomLevelOffset; // ���� ���� ��ġ
    public int Compensation; // ����
    public int MpHealOffset; // ���� ȸ�� ��ġ

    [Serializable]
    public struct MonsterGroup
    {
        public List<Character> MosterData; //���� ������
    };
}
