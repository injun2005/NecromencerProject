using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageOpen : MonoBehaviour
{
    public GameObject StartImage;
    public int stage = 1;
    public GameObject nextStage;
    private List<StageDataSO> SD;
    public int stageIndex = 0;

    private void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(stage == 1)
        {
            StartImage.SetActive(true);
        }
        else if (stage == 2)
        {
            StartImage.SetActive(true);
        }
        else if (stage == 3)
        {
            StartImage.SetActive(true);
        }
        else if (stage == 4)
        {
            StartImage.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        StartImage.SetActive(false);
        Debug.Log("DD");
    }

    public void NextStage()
    {
        if (stage == 1)
        {
            stageIndex++;
            if (SD[stageIndex].MosterGroupData.Count == 0)
            {
                stage++;
                nextStage.SetActive(true);
            }
        }
        else if (stage == 2)
        {
            stageIndex++;
            if (SD[stageIndex].MosterGroupData.Count == 0)
            {
                stage++;
                nextStage.SetActive(true);
            }
        }
        else if (stage == 3)
        {
            stageIndex++;
            if (SD[stageIndex].MosterGroupData.Count == 0)
            {
                stage++;
                nextStage.SetActive(true);
            }
        }
        else if (stage == 4)
        {
            stageIndex++;
            if (SD[stageIndex].MosterGroupData.Count == 0)
            {
                stage++;
                nextStage.SetActive(true);
            }
        }
    }

    public void SceneStart()
    {
        SceneManager.LoadScene("Main");
    }
}
