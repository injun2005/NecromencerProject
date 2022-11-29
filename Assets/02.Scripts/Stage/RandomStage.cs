using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStage : MonoBehaviour
{
    public List<GameObject> mapList;
    int ramdomInt;

    private void Start()
    {
        RandomMap();
    }

    void RandomMap()
    {
        ramdomInt = Random.Range(0, mapList.Count);

        Instantiate(mapList[ramdomInt]);
        mapList[ramdomInt].transform.position = new Vector3(0, 0, 0); 
    }
}
