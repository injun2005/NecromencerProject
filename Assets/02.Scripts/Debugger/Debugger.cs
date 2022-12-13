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


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)){
            BattleSystem.Inst.BattleRelease();
            GameManager.Inst.CurrentPlayer.AddTeam(teamCharacter);
            BattleSystem.Inst.BattleStart();
        }       

    }
}
