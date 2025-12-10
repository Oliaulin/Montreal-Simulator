 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuiviFluide : MonoBehaviour
{
    // GameObjects
    public GameObject Cible;

    // Vector3
    public Vector3 Distance;
    public Vector3 AjustementFocus;

    // Floats
    public float Amortissement;

    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // placement de la caméra selon le joueur
        Vector3 PositionFinale = Cible.transform.TransformPoint(Distance);
        transform.position = Vector3.Lerp(transform.position, PositionFinale, Amortissement);

        // Visée de la caméra vers le joueur
        transform.LookAt(Cible.transform.position + AjustementFocus);
    }
}
