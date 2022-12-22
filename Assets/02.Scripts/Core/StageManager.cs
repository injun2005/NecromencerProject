using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TMPro;

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

    public GameObject StagesPrefabs;
    [SerializeField]
    private List<StageMonsterPos> stageMonsterPos = new List<StageMonsterPos>();
    public GameObject Map;

    private bool isOnce = false;
    public int stage = 1;
    public GameObject[] nextStage;

    public TextMeshProUGUI text;


    void Update()
    {

    }

    private void Awake()
    {
        text.text = "���� :" + stage + " ��������";
    }

    public void NextStage()
    {
            stage++;
            text.text = "���� :" + stage + " ��������";
            //if(stage > stages.Count)
            //{
            //    //GameManager.Inst.GameClear();
            //}
            nextStage[stage - 1].SetActive(true); 
    }




    public void SceneStart()
    {
        //SceneManager.LoadScene("Main");
        StagesPrefabs.SetActive(true);
        Map.SetActive(false);
        //st.RandomMap();
        if (!isOnce)
        {
            isOnce = true;
            GameManager.Inst.CurrentPlayer.AddTeam(BattleSystem.Inst.Pop(ECharacterType.Slime, 5));
        }
        BattleSystem.Inst.SetStage(stageMonsterPos[stage - 1].MonsterPos, stages[stage - 1].MosterGroupData);
    }


    public void Cheat()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            NextStage();
        }
    }
}
