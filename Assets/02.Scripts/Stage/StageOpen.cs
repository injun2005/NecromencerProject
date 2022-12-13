using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageOpen : MonoBehaviour
{
    public GameObject StartImage;
    private StageManager stageManager;

    private void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        StartImage.SetActive(true);
    }

    public void OnTriggerExit(Collider other)
    {
        StartImage.SetActive(false);
        Debug.Log("DD");
    }
}
