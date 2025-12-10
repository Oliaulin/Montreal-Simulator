using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControleJoueur : MonoBehaviour
{
    // Floats
    public float vitesse;
    private float forceMouvement;
    private float forceMouvementSide;
    public float vitesseTourne;
    public float hauteurSaut;
    private float forceSaut;
    public float conesSpeciauxAmmassees;
    public float conesAmmassees;
    public float SecretsTrouves;

    // Bools
    public bool auSol;

    // Rigidbodies
    public Rigidbody rbJoueur;

    // AudioSources
    public AudioSource audioJoueur;
    public AudioClip conePickup;
    public AudioClip sonSaut;
    public AudioClip manger;

    // Scripts
    public ScriptBlueCar ScriptBlueCar;
    public ScriptBlueCar2 ScriptBlueCar2;

    // GameObjects
    public GameObject hintBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbJoueur = GetComponent<Rigidbody>();

        // réinitialiser les scores
        conesSpeciauxAmmassees = 0;
        conesAmmassees = 0;
        SecretsTrouves = 0;

        Cursor.lockState = CursorLockMode.Locked; // désactivation du curseur lors du jeu
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(forceSaut);
        // Configuration des axes de mouvements
        float axeH = Input.GetAxisRaw("Horizontal");
        float axeV = Input.GetAxisRaw("Vertical");

        // configurations du mouvement du joueur
        Vector3 inputDirection = new Vector3(axeH, 0, axeV).normalized;
        Vector3 moveDirection = transform.TransformDirection(inputDirection);
        rbJoueur.linearVelocity = new Vector3(moveDirection.x * vitesse, rbJoueur.linearVelocity.y, moveDirection.z * vitesse);
        float valeurTourne = Input.GetAxis("Mouse X") * vitesseTourne;
        transform.Rotate(0, valeurTourne, 0);

        // Déterminer si le joueur touche le sol
        RaycastHit infoCollision;
        float maxDistance = 1.1f;
        auSol = Physics.SphereCast(transform.position + new Vector3(0, 1f, 0), 0.2f, Vector3.down, out infoCollision, maxDistance);

        // Si le joueur clique sur espace en étant au sol, il peut sauter
        if (Input.GetKeyDown(KeyCode.Space) && auSol)
        {
            forceSaut = hauteurSaut;
            GetComponent<Animator>().SetBool("isJumping", true);
            GetComponent<AudioSource>().PlayOneShot(sonSaut, 1);
        }

        // Si le joueur input du mouvement dans les axes, activer l'animation de course/marche
        if (axeH != 0 || axeV != 0)
        {
            GetComponent<Animator>().SetBool("isRunning", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("isRunning", false);
        }

        // Si les 5 cones spéciaux qui bloquent la rue sont ramassés, on active les deux voitures de cette rue.
        if (conesSpeciauxAmmassees == 5)
        {
            ScriptBlueCar.activationVoiture();
            ScriptBlueCar2.activationVoiture2();
        }

        // Lorsque tous les cones ont étés trouvés, la partie se termine
        if (conesAmmassees == 60)
        {
            Invoke("GameOver", 0);
        }
    }

    private void OnCollisionEnter(Collision autreObjet)
    {
        // Si le joueur touche un cone, il le ramasse, fait un son et le nb de cones ramassés est updaté
        if (autreObjet.gameObject.tag == "cone")
        {            
            GetComponent<AudioSource>().PlayOneShot(conePickup, 1);
            conesAmmassees += 1;
            ScriptConteurCones.Score += 1;
            Destroy(autreObjet.gameObject);
        }

        // Si le joueur touche un cone spécial, il le ramasse, fait un son, et le nb de cones ramassés
        // et le nb de cones spéciaux sont updatés
        if (autreObjet.gameObject.tag == "coneSpecial1")
        {            
            GetComponent<AudioSource>().PlayOneShot(conePickup, 1);
            conesSpeciauxAmmassees += 1;
            conesAmmassees += 1;
            ScriptConteurCones.Score += 1;
            Destroy(autreObjet.gameObject);
        }

        // Si le joueur touche une poutine, il la ramasse, fait un son,
        // et le nb de secrets trouvés est updaté
        if (autreObjet.gameObject.tag == "Poutine")
        {
            GetComponent<AudioSource>().PlayOneShot(manger, 1);
            ScriptConteurSecrets.ScoreSecrets += 1;
            Destroy(autreObjet.gameObject);
            //Debug.Log(ScriptConteurSecrets.ScoreSecrets + " poutine ramassées");
        }
    }

    private void OnTriggerStay(Collider autreObjet)
        // Utilisation de OnTriggerStay pour savoir si le joueur est sur les téléporteurs
    {
        // Si le joueur restes sur le téléporteur 1 puis clique sur shift, il se fera téléporter
        if (autreObjet.gameObject.tag == "teleporteur1")
        {
            if (Input.GetKey(KeyCode.RightShift))
            {
                // Debug.Log("actif");
                rbJoueur.position = new Vector3(475f, 2f, 838f);
            }
        }

        // Si le joueur restes sur le téléporteur 2 puis clique sur shift, il se fera téléporter
        if (autreObjet.gameObject.tag == "teleporteur2")
        {
            if (Input.GetKey(KeyCode.RightShift))
            {
                rbJoueur.position = new Vector3(10f, 111f, -20f);
            }
        }

        // Si le joueur restes sur le téléporteur 1 OU 2, l'instruction comment interagir apparaitra
        if (autreObjet.gameObject.tag == "teleporteur1" || autreObjet.gameObject.tag == "teleporteur2")
        {
            hintBox.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider autreObjet)
        // Utilisation de OnTriggerExit pour savoir quand est-ce que le joueur sort de la zone de téléportation,
        // puis pour enlever l'instruction
    {
        if (autreObjet.gameObject.tag == "teleporteur1" || autreObjet.gameObject.tag == "teleporteur2")
        {
            hintBox.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider autreObjet)
        // Utilisation de OnTriggerEnter pour savoir le moment ou le joueur touche un objet mortel
    {
        // Si le joueur touche un objet mortel, désactivation de ce script, activation de l'animation de mort,
        // puis changement de scène
        if (autreObjet.gameObject.tag == "mort")
        {
            GetComponent<Animator>().SetBool("isDead", true);
            enabled = false;
            Invoke("GameOver", 3f);
        }
    }

    private void FixedUpdate()
    {
        
        GetComponent<Rigidbody>().AddRelativeForce(forceMouvementSide, forceSaut, forceMouvement, ForceMode.VelocityChange);
        forceSaut = 0;
        GetComponent<Animator>().SetBool("isJumping", false);
    }

    private void GameOver()
    {
        // Changement de scène et réactivation du curseur
        SceneManager.LoadScene(3);
        Cursor.lockState = CursorLockMode.None;
    }
}

