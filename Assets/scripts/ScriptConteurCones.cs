using UnityEngine;
using TMPro;

public class ScriptConteurCones : MonoBehaviour
{
    public static int Score;
    TextMeshProUGUI tmpScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Awake()
    {
        tmpScore = GetComponent<TextMeshProUGUI>();
        Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // affichage du nombre actuel de cones ramassés
        tmpScore.text = "Cones ramassés: " + Score + "/60";
    }
}
