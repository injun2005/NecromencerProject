using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    [SerializeField]
    private Character teamCharacter;

    private void Awake()
    {
        teamCharacter.Init(3);
    }


}
