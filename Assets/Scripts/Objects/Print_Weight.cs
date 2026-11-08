using TMPro;
using UnityEngine;

public class Print_Weight : MonoBehaviour
{
    public static void PrintMass(TextMeshPro massText, Rigidbody rb)
    {
        massText.text = rb.mass.ToString();
    }
}
