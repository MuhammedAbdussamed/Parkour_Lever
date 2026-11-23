using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Movement Stats")]
    [SerializeField] internal float characterSpeed;
    [SerializeField] internal float jumpPower;
    [SerializeField] internal float maxSpeed;

    [Header("Stamina")]
    [SerializeField] internal float currentStamina;
    [SerializeField] internal float maxStamina;
    [SerializeField] internal float staminaDecreasingValue;
    [SerializeField] internal float staminaIncreasingValue;
    [SerializeField] internal float stayTiredDuration;
    internal float defaultCharacterSpeed;
    internal bool isTired;

    void Awake()
    {
        defaultCharacterSpeed = characterSpeed;
        staminaDecreasingValue = 50f;
        staminaIncreasingValue = 50f;
    }

    void Update()
    {
        currentStamina = Mathf.Clamp(currentStamina, 0, 100);
    }
}
