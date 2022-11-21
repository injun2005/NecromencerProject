using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Stat/CharacterStatData")]

public class StageDataSO : ScriptableObject
{
    public int MosterData; //���� ������
    public int Level; //����
    public int RandomLevelOffset; // ���� ���� ��ġ
    public int Compensation; // ����
    public int MpHealOffset; // ���� ȸ�� ��ġ
}
