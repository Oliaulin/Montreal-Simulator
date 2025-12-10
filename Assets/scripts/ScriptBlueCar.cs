using System;
using UnityEngine;

public class ScriptBlueCar : MonoBehaviour
{
    public bool isActive;
    public float vitesseVoiture; // vitesse de la voiture settée dans l'inspecteur
    public GameObject voitureBleu;
    public Rigidbody rbVoitureBleu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isActive = false; // déterminer si la voiture est active ou non
        rbVoitureBleu.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activationVoiture() // Fonction activée seulement lorsque que les cones spéciaux dans le script ControleJoueur
                                    // ont étés ramassés, débloquant la rue.
    {
        //Debug.Log("arrivé");
        isActive = true;
        // Faire avancer la voiture
        rbVoitureBleu.linearVelocity = new Vector3(-vitesseVoiture, 0, 0);
        if (rbVoitureBleu.position.x <= -34)
        {
            rbVoitureBleu.position = new Vector3(57, rbVoitureBleu.position.y, rbVoitureBleu.position.z);
        }
    }
}
