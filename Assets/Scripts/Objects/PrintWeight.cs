using TMPro;
using UnityEngine;

public class PrintWeight : MonoBehaviour
{
    public static void PrintMass(TextMeshPro massText, float mass)
    {
        if (mass <= 0)
            massText.text = string.Empty;
        else
            massText.text = mass.ToString();
    }
}
