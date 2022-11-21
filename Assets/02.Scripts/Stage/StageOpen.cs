using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageOpen : MonoBehaviour
{
    public GameObject[] StartImage;
    public int[] MapIdx;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void start1()
    {
        StartImage[0].SetActive(true);
    }

    public void Exit1()
    {
        StartImage[0].SetActive(false);
    }

    public void SceneStart()
    {
        MapIdx[0] = 0;
        SceneManager.LoadScene("Game");
    }
}
