using TMPro;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Interact interact;

    [Header("Canvases")]
    [SerializeField] private Canvas playerUI;
    [SerializeField] private Canvas dynamicUI;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI interactText;

    void OnEnable()
    {
        interact.InteractTextEvent += TurnInteractText;
    }

    void OnDisable()
    {
        interact.InteractTextEvent -= TurnInteractText;
    }

    private void TurnInteractText(bool isOn)
    {
        interactText.gameObject.SetActive(isOn);
    }
}
