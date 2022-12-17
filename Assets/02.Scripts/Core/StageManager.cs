using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[Serializable]
public class StageArea
{

}
[Serializable]
public struct StageMonsterPos
{
    public List<Transform> MonsterPos;
}

public class StageManager : MonoSingleton<StageManager>
{
    [SerializeField]
    List<StageDataSO> Stages = new List<StageDataSO>();
    public GameObject StagesPrefabs;
    [SerializeField]
    private List<StageMonsterPos> stageMonsterPos = new List<StageMonsterPos>();
    public GameObject Map;


    RandomStage st;

    //public List<StageArea> areaList;

    //private Dictionary<EStageArea, StageArea> E;


    public int stage = 1;
    public GameObject[] nextStage;


    void Update()
    {
        Cheat();
    }

    public void NextStage()
    {
        if (Stages[stage - 1].MosterGroupData.Count == 0)
        {
            stage++;
            if(stage > Stages.Count)
            {
                //GameManager.Inst.GameClear();
            }
            nextStage[stage - 1].SetActive(true);
        }
    }

    public void SceneStart()
    {
        //SceneManager.LoadScene("Main");
        StagesPrefabs.SetActive(true);
        Map.SetActive(false);
        //st.RandomMap();
        BattleSystem.Inst.SetStageTrm(stageMonsterPos[stage - 1].MonsterPos);
    }


    public void Cheat()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            NextStage();
        }
    }
}
