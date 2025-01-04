using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerascript : MonoBehaviour
{
    // Start is called before the first frame update
    // Referencia al objeto que la cámara seguirá
    public Transform target;

    // Distancia relativa de la cámara respecto al objeto
    public Vector3 offset;

    // Velocidad de seguimiento para suavizar el movimiento
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        // Si no hay un objetivo asignado, no hacer nada
        if (target == null) return;

        // Calcula la posición deseada de la cámara
        Vector3 desiredPosition = target.position + offset;

        // Suaviza el movimiento de la cámara hacia la posición deseada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        smoothedPosition.y=smoothedPosition.y+5f;
        smoothedPosition.z=smoothedPosition.z-5f;
        // Actualiza la posición de la cámara
        transform.position = smoothedPosition;

        // Opcional: Mantén la cámara mirando al objetivo
        // transform.LookAt(target);
    }
}
