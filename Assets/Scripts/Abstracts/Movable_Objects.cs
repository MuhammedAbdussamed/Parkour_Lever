using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

// Ağırlık objeleri için genellikle bu script kullanılır
public class Movable_Objects : Interactable_Objects
{
    // Variables
    public bool isHolding;                          // Objenin "tutuluyor mu ?" sorusunu cevaplar
    private const int objectMovingSpeed = 10;

    // References
    private Rigidbody rb;
    private TextMeshPro massText;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        massText = transform.Find("Mass_Text").GetComponent<TextMeshPro>();
    }

    public override void InteractFunction()
    {
        HoldItem();
    }

    public void Update()
    {
        massText.text = rb.mass.ToString();
    }

    private void LateUpdate()
    {
        if (!isHolding) return;

        Transform cam = UnityEngine.Camera.main.transform;
        Vector3 targetPosition = cam.position + cam.forward * 2f;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            Time.deltaTime * objectMovingSpeed
            );
    }

    private void HoldItem()
    {
        isHolding = !isHolding;
        rb.isKinematic = isHolding;
    }
}

