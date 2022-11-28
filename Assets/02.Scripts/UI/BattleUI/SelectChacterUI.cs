using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectChacterUI : MonoBehaviour
{
    [SerializeField]
    private Text characterNameText;
    [SerializeField]
    private Button selectBtn;
    private Character target;


    public void Init(Character character)
    {
        target = character;

        selectBtn.onClick.AddListener(SelectChacter);
    }
    
    public void SelectChacter()
    {
        //target.
    }
}
