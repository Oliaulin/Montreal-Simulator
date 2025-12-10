using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptBouton : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jouer(int numeroScene)
    {
        // fonction pour changer de scènes
        SceneManager.LoadScene(numeroScene);
    }

    public void ResetScore()
    {
        ScriptConteurSecrets.ScoreSecrets = 0;
    }
}
