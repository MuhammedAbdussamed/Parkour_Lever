using TMPro;
using UnityEngine;

public class Print_Weight : MonoBehaviour
{
    public static void PrintMass(TextMeshPro massText, float mass)
    {
        massText.text = mass.ToString();
    }
}
