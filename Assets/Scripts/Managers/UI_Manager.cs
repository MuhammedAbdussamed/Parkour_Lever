using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Interact interact;
    [SerializeField] private Player_Stats playerStats;

    [Header("Canvases")]
    [SerializeField] private Canvas playerUI;
    [SerializeField] private Canvas dynamicUI;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI interactText;

    [Header("Bars")]
    [SerializeField] private UnityEngine.UI.Slider staminaBar;

    void OnEnable()
    {
        interact.InteractTextEvent += TurnInteractText;
    }

    void OnDisable()
    {
        interact.InteractTextEvent -= TurnInteractText;
    }

    private void Start()
    {
        staminaBar.maxValue = 100f;
        staminaBar.minValue = 0f;
    }

    private void Update()
    {
        SetStaminaBar();
    }

    private void TurnInteractText(bool isOn)
    {
        interactText.gameObject.SetActive(isOn);
    }

    private void SetStaminaBar()
    {
        staminaBar.value = playerStats.stamina;

    }


}
