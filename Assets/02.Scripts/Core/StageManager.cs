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
    private List<StageDataSO> stages = new List<StageDataSO>();

    public List<StageDataSO>StageList { get { return stages; } }

    [SerializeField]
    private GameObject StagesPrefabs;
    [SerializeField]
    private List<StageMonsterPos> stageMonsterPos = new List<StageMonsterPos>();
    public GameObject Map;


    public int stage = 1;
    public GameObject[] nextStage;


    void Update()
    {
        Cheat();
    }

    public void NextStage()
    {
        if (stages[stage - 1].MosterGroupData.Count == 0)
        {
            stage++;
            if(stage > stages.Count)
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
        BattleSystem.Inst.SetStage(stageMonsterPos[stage - 1].MonsterPos, stages[stage - 1].MosterGroupData);
        BattleSystem.Inst.BattleStart();
    }


    public void Cheat()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            NextStage();
        }
    }
}
