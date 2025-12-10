using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScriptConteurSecrets : MonoBehaviour
{
    public static int ScoreSecrets;
    TextMeshProUGUI tmpScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Awake()
    {
        tmpScore = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // affichage du nombre de secrets trouvés
        tmpScore.text = "Secrets trouvés: " + ScoreSecrets + "/3";
    }
}
