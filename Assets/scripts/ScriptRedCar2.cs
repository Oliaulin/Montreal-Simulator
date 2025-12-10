using System;
using UnityEngine;

public class ScriptRedCar2 : MonoBehaviour
{
    public float vitesseVoiture; // vitesse de la voiture settée dans l'inspecteur
    public GameObject voitureRouge;
    public Rigidbody rbVoitureRouge;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbVoitureRouge.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Setter le mouvement de la voiture (dans le sens inverse)
        rbVoitureRouge.linearVelocity = new Vector3(0, 0, -vitesseVoiture);
        //rbVoiture.transform.Translate(Vector3.forward * vitesseVoiture);

        if (rbVoitureRouge.position.z <= -35)
        {
            // Si la voiture arrive à la fin de la rue, elle réapparaitrera au début de la rue.
            // Une possibilitée qui aurait pu être possible aurait été de la faire réapparaitre sur une autre rue,
            // pour un effet d'aléatoire. (J'y ai renoncé)
            rbVoitureRouge.position = new Vector3(rbVoitureRouge.position.x, rbVoitureRouge.position.y, 55);
        }
    }
}
