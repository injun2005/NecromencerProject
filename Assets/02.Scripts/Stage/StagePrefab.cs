using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePrefab : MonoBehaviour
{
    public StageDataSO stage;

    public void PrintStageDataSO()
    {
        print(stage.MosterGroupData);
        print(stage.Level);
        print(stage.RandomLevelOffset);
        print(stage.Compensation);
        print(stage.MpHealOffset);
    }
}
