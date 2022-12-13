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
    List<StageDataSO> Stages = new List<StageDataSO>();
    private GameObject StagesPrefabs;
    //public List<StageArea> areaList;

    //private Dictionary<EStageArea, StageArea> E;


    void Awake()
    {

    }





}
