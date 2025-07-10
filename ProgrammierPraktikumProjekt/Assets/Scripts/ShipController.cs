using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;


/// <summary>
/// Steuert das Verhalten des Schiffs, einschließlich Bewegung und Gechwindigkeit, Ankermechanik und UI-Anzeige
/// </summary>

public class ShipController : MonoBehaviour
{
    [SerializeField] private GameObject shipAgent;//Agent
    [SerializeField] private GameObject startPosition;//Agent
    [SerializeField] private float thrustForce = 500f; // Schubkraft Motor
    [SerializeField] private float dragCoefficient = 0.1f; // Widerstandskoeffizient
    [SerializeField] private float rotationSpeed = 50f; // Drehgeschwindigkeit
    [SerializeField] private bool anchorDropped = false; // bool Wert anfangs auf false 
    [SerializeField] private UIDocument uiDocument; 
    private Label speedLabel;
    private Label ankerText;
    private Rigidbody rb;
    private NavMeshAgent agent;
    private Vector3 endPosition;
    
    
    //damit beim Anker setzen das Schiff langsamer stoppt
    private float targetDamping = 0.1f;
    private float dampingLerpSpeed = 1.5f; // wie schnell sich das anpasst

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (uiDocument != null)
        {
            var root = uiDocument.rootVisualElement;
            speedLabel = root.Q<Label>("Speed");
            ankerText = root.Q<Label>("Anker");
        }

        agent = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// Methode die Nutzereingaben ueber Taste steuert
    /// </summary>

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            
            rb.linearDamping = 0.0f; //den Wiederstand auf 0, damit es schneller wird
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            dropAnchor();
            ShowPanel(ankerText);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("L gedrückt");
            liftAnchor();
            ankerText.style.display = DisplayStyle.None;
        }

        // Geschwindigkeit berechnen & anzeigen
        float speed = rb.linearVelocity.magnitude;
        speedLabel.text = $"Geschwindigkeit: {speed:0.0} m/s";

        if (Input.GetKeyDown(KeyCode.M))
        {
            SpawnAndSetAgent();
        }
    }

    private void FixedUpdate()
    {
        float thrustInput = Input.GetAxis("Vertical"); // w/s Schubsteuerung
        float turnInput = Input.GetAxis("Horizontal"); // a/d Drehsteuerung

        applyForces(thrustInput);
        applyRotation(turnInput);

        rb.linearDamping = Mathf.Lerp(rb.linearDamping, targetDamping, Time.fixedDeltaTime * dampingLerpSpeed); //fuer sanfteres anhalten
    }

    /// <summary>
    /// zustaendig fuer Rotierung des schiffs
    /// </summary>
    /// <param> name=turnInput ueber benutzerangaben </param>
    private void applyRotation(float turnInput)
    {
     if(Mathf.Abs(turnInput)> 0.1f)
        {
            float adjustTurnSpeed = rotationSpeed * (rb.linearVelocity.magnitude / 10f);
            float turnTorque = turnInput * adjustTurnSpeed;

            rb.AddTorque(Vector3.up * turnTorque, ForceMode.Acceleration);
        }  
    }

    /// <summary>
    /// Berechnung des Antriebs
    /// </summary>
    /// <param> name=thrustInput ueber benutzereingabe </param>
    private void applyForces(float thrustInput)
    {
        if (anchorDropped)
            return;

        // Kräfte berechnen
        float thrust = thrustInput * thrustForce;
        float drag = dragCoefficient * rb.linearVelocity.magnitude * rb.linearVelocity.magnitude;

        // Nettokraft berechnen
        float netForce = thrust - drag;
        Vector3 acceleration = (netForce / rb.mass) * transform.forward;

        // Kraft auf das Rigidbody anwenden
        rb.AddForce(acceleration, ForceMode.Acceleration);
    }
    private void dropAnchor()
    {
        anchorDropped = true;
        rb.linearDamping = 10f; // starker Widerstand, damit das Schiff schnell stoppt
        targetDamping = 10f;
    }

    /// <summary>
    /// Widerstand anpassen, damit sas Schiff nach dem heben des Ankers losfaehrt
    /// </summary>
    private void liftAnchor()
    {
        anchorDropped = false;
        rb.linearDamping = 0.1f; // normaler Widerstand zum losfahren
        targetDamping = 0.1f;
    }

    /// <summary>
    /// Sichtbarkeit UI Document
    /// </summary>
    /// <param> name=targetPanel das entsprechende panel </param>
    private void ShowPanel(Label targetPanel)
    {
        ankerText.style.display = DisplayStyle.None;
        
        targetPanel.style.display = DisplayStyle.Flex;
    }

    /// <summary>
    /// Methode zustaendig um Kollisionen zu simulieren
    /// </summary>
    /// <param> name=other : das kollidierende Objekt </param>

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ufer"))
        {
            Debug.Log("Schiff gestoppt durch Trigger: " + other.name);
            dropAnchor();
        }
        if (other.CompareTag("Agent"))
        {
            Debug.Log("Schiff gestoppt durch Trigger: " + other.name);
            dropAnchor();
        }
    }

    /// <summary>
    /// Spawnt Agenten an der definierten Startposition
    /// </summary>
    private void SpawnAndSetAgent()
    {
        Quaternion prefabRotation = shipAgent.transform.rotation;
        GameObject newAgent = Instantiate(shipAgent, startPosition.transform.position, prefabRotation);
    }
}


