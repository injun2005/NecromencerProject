using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISystem : MonoBehaviour
{
    private Character currentCharacter;

    private int actionIdx;
    private int skillIdx;
    private Character target;

    public void SetCurrentCharacter(Character character)
    {
        currentCharacter = character;
    }
    public void SetTarget(Character character)
    {
        target = character;
        currentCharacter.SetTarget(target);
    }
    public void RandomAction()
    {
        actionIdx = Random.Range(1, 3);
        if (actionIdx == (int)ECharacterAction.Skill)
        {
            RandomSkill();
        }
        if (actionIdx == 1)
            currentCharacter.SetActionIdx(ECharacterAction.Attack);
        if (actionIdx == 2)
            currentCharacter.SetActionIdx(ECharacterAction.Skill);

    }

    public void RandomSkill()
    {
        int cnt = currentCharacter.skillList.Count;


        skillIdx = Random.Range(0, currentCharacter.skillList.Count);
        if (!currentCharacter.skillList[skillIdx].isCanUse || !currentCharacter.skillList[skillIdx].isActive)
        {
            currentCharacter.SetActionIdx(ECharacterAction.Attack);
            return;
        }
        currentCharacter.SetSkillIdx(currentCharacter.skillList[skillIdx].skillKey);
    }
}
