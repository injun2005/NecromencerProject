using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Define
{
    private static Camera _mainCam;

    public static Camera MainCam
    {
        get
        {
            if (_mainCam == null)
            {
                _mainCam = Camera.main;
            }

            return _mainCam;
        }

    }

    private static Transform windowCanvasTrm;

    public static Transform WindowCanvasTrm
    {
        get
        {
            if (windowCanvasTrm == null)
            {
                windowCanvasTrm = GameObject.FindGameObjectWithTag("WindowCanvas").transform;
            }

            return windowCanvasTrm;
        }
    }

    public static void GameQuit()
    {
        Application.Quit();
    }
}