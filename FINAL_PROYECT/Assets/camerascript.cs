using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerascript : MonoBehaviour
{
    // Start is called before the first frame update
    // Referencia al objeto que la c�mara seguir�
    public Transform target;

    // Distancia relativa de la c�mara respecto al objeto
    public Vector3 offset;

    // Velocidad de seguimiento para suavizar el movimiento
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        // Si no hay un objetivo asignado, no hacer nada
        if (target == null) return;

        // Calcula la posici�n deseada de la c�mara
        Vector3 desiredPosition = target.position + offset;

        // Suaviza el movimiento de la c�mara hacia la posici�n deseada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        smoothedPosition.y=smoothedPosition.y+5f;
        smoothedPosition.z=smoothedPosition.z-5f;
        // Actualiza la posici�n de la c�mara
        transform.position = smoothedPosition;

        // Opcional: Mant�n la c�mara mirando al objetivo
        // transform.LookAt(target);
    }
}
