using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Interact interact;
    [SerializeField] private PlayerStats playerStats;

    [Header("Canvases")]
    [SerializeField] private Canvas playerUI;
    [SerializeField] private Canvas dynamicUI;

    [Header("Images")]
    [SerializeField] private UnityEngine.UI.Image darkScreen;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI interactText;

    [Header("Bars")]
    [SerializeField] private UnityEngine.UI.Slider staminaBar;

    void OnEnable()
    {
        interact.InteractTextEvent += TurnOnInteractText;
    }

    void OnDisable()
    {
        interact.InteractTextEvent -= TurnOnInteractText;
    }

    private void Start()
    {
        staminaBar.maxValue = 100f;
        staminaBar.minValue = 0f;
    }

    private void Update()
    {
        SetStaminaBar();
        OpenScreen();
    }

    #region ----- Functions -----

    private void TurnOnInteractText(bool isOn)
    {
        interactText.gameObject.SetActive(isOn);
    }

    /*-----------------------------------*/

    private void SetStaminaBar()
    {
        staminaBar.value = playerStats.currentStamina;
    }

    /*-----------------------------------*/

    private void OpenScreen()
    {
        float transitionDuration = 20f;
        Color color = darkScreen.color;
        color.a = Mathf.Lerp(darkScreen.color.a, 0, 1f / transitionDuration);
        darkScreen.color = color;
    }


    #endregion


}
