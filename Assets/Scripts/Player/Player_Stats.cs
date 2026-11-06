using System.Runtime.CompilerServices;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] internal float characterSpeed;
    [SerializeField] internal float jumpPower;
    [SerializeField] internal float stamina;

    internal float staminaDecreasingValue;
    internal float staminaIncreasingValue;
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
        stamina = Mathf.Clamp(stamina, 0, 100);
    }
}
