using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ActionPanel : MonoBehaviour
{
    [SerializeField]
    private ECharacterAction actionType;

    private Button selectBtn;

    private void Awake()
    {
        selectBtn = GetComponent<Button>();
        selectBtn.onClick.AddListener(SelectActionType);
    }

    private void SelectActionType()
    {
        Player.OnSelectAction.Invoke(actionType);
    }
}
