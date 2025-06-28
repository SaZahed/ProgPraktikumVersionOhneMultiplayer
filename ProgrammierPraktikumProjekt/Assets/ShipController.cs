using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     rb = GetComponent<Rigidbody>(); // Rigidbody-Referenz abrufen   
    if (uiDocument != null)
    {
        var root = uiDocument.rootVisualElement;
        speedLabel = root.Q<Label>("Speed");
        ankerText = root.Q<Label>("Anker");
    }
        agent = GetComponent<NavMeshAgent>();
        endPosition = GameObject.FindGameObjectWithTag("End").transform.position;
        agent.SetDestination(endPosition);
    }

    // Update is called once per frame
    void Update()
    {
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
        if (Input.GetKeyDown("m"))
        {
            Instantiate(shipAgent, startPosition.transform.position, Quaternion.identity);
        }
    }

    private void FixedUpdate()
    {
        float thrustInput = Input.GetAxis("Vertical"); // w/s Schubsteuerung
        float turnInput = Input.GetAxis("Horizontal"); // a/d Drehsteuerung

        applyForces(thrustInput);
        applyRotation(turnInput);
    }
    private void applyRotation(float turnInput)
    {
     if(Mathf.Abs(turnInput)> 0.1f)
        {
            float adjustTurnSpeed = rotationSpeed * (rb.linearVelocity.magnitude / 10f);
            float turnTorque = turnInput * adjustTurnSpeed;

            rb.AddTorque(Vector3.up * turnTorque, ForceMode.Acceleration);
        }  
    }
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
    }
    private void liftAnchor()
    {
        anchorDropped = false;
        rb.linearDamping = 0.1f; // normaler Widerstand
    }

    private void ShowPanel(Label targetPanel)
    {
        ankerText.style.display = DisplayStyle.None;
        
        targetPanel.style.display = DisplayStyle.Flex;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ufer"))
        {
            anchorDropped = true;
            rb.linearDamping = 10f;
            Debug.Log("Schiff gestoppt durch Trigger: " + other.name);
        }
    }
}


