using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageOpen : MonoBehaviour
{
    public GameObject[] StartImage;
    public RandomStage Rs;

    private void Start()
    {
        Rs = GetComponent<RandomStage>();
    }

    public void start1()
    {
        StartImage[0].SetActive(true);
    }

    public void Exit1()
    {
        StartImage[0].SetActive(false);
    }

    public void start2()
    {
        StartImage[1].SetActive(true);
    }

    public void Exit2()
    {
        StartImage[1].SetActive(false);
    }

    public void start3()
    {
        StartImage[2].SetActive(true);
    }

    public void Exit3()
    {
        StartImage[2].SetActive(false);
    }


    public void start4()
    {
        StartImage[3].SetActive(true);
    }

    public void Exit4()
    {
        StartImage[3].SetActive(false);
    }


















    public void SceneStart()
    {
        SceneManager.LoadScene("Main");
    }
}
