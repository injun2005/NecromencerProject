using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[Serializable]
public class StageArea
{
    List<StageDataSO> EAreaData;
}


public class StageManager : MonoBehaviour
{
    public int stage = 1;

    public List<StageArea> areaList;

    private Dictionary<EStageArea, StageArea> E;

    private void Update()
    {
        NextStage();
    }


    public void NextStage()
    {
        if(Input.GetKeyDown(KeyCode.Q))//Monster == 0))
        {
            stage++;
            if(stage == 2)
            {

            }
        }
    }

}
