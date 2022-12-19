using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public struct MonsterGroup
{
    public List<Character> MosterData; //���� ������
};

[CreateAssetMenu(menuName = "SO/Stat/StageDataSO")]

public class StageDataSO : ScriptableObject
{
    //stage 1���� ����Ʈ
    public List<MonsterGroup> MosterGroupData; //���� �׷쿡 �ִ� ������
    public int Level; //�ܰ�
    public int RandomLevelOffset; // ���� ���� ��ġ
    public int Compensation; // ����
    public int MpHealOffset; // ���� ȸ�� ��ġ


}
