using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ActionPanel : MonoBehaviour
{
    [SerializeField]
    private ECharacterAction actionType;

    private Button selectBtn;
    private Player player { get { return GameManager.Inst.CurrentPlayer; } }


    private void Awake()
    {
        selectBtn = GetComponent<Button>();
        selectBtn.onClick.AddListener(SelectActionType);
    }

    private void SelectActionType()
    {
        player.OnSelectAction?.Invoke(actionType);
    }
}
