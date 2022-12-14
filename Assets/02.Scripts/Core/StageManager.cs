using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[Serializable]
public class StageArea
{

}


public class StageManager : MonoSingleton<StageManager>
{
    [SerializeField]
    private List<StageDataSO> stages = new List<StageDataSO>();

    public List<StageDataSO>StageList { get { return stages; } }

      
    private GameObject StagesPrefabs;

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
    }

    public void Cheat()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            NextStage();
        }
    }
}
