using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StageArea
{
    List<StageDataSO> EAreaData;
}


public class StageManager : MonoBehaviour
{
    public List<StageArea> areaList;

    private Dictionary<EStageArea, StageArea> E;
}
