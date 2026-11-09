using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

// Ağırlık objeleri için genellikle bu script kullanılır
public class Movable_Objects : Interactable_Objects
{
    public bool isHolding;      // Objenin "tutuluyor mu ?" sorusunu cevaplar
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
            Time.deltaTime * 10
            );
    }

    private void HoldItem()
    {
        isHolding = !isHolding;
        rb.isKinematic = isHolding;
    }
}

    /*
    private void MoveObject()
    {
        if (isHolding)
        {
            rb.Sleep();
            Vector3 cameraPosition = Camera.cameraPosition;

            transform.position = Vector3.Lerp(
                transform.position,
                cameraPosition,
                Time.deltaTime * 10
                );
        }
        else
        {
            rb.WakeUp();
        }
    }
    */

