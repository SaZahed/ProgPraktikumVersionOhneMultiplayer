using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] private float thrustForce = 500f; // Schubkraft Motor
    [SerializeField] private float dragCoefficient = 0.1f; // Widerstandskoeffizient
    [SerializeField] private float rotationSpeed = 50f; // Drehgeschwindigkeit
   
    //[SerializeField] private float anchorDropForce = 100f; // Kraft beim Anker fallen lassen
    //[SerializeField] private float anchorLiftForce = 50f; // Kraft beim Anker heben
    [SerializeField] private bool anchorDropped = false; // bool Wert anfangs auf false 

    private Rigidbody rb;   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     rb = GetComponent<Rigidbody>(); // Rigidbody-Referenz abrufen   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float thrustInput = Input.GetAxis("Vertical"); // w/s Schubsteuerung
        float turnInput = Input.GetAxis("Horizontal"); // a/d Drehsteuerung

        applyForces(thrustInput);
        applyRotation(turnInput);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            dropAnchor();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("L gedrückt");//debug nachricht (die nicht angezeigt wird)
            liftAnchor();
        }
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
        //Start();
        rb.linearDamping = 0.1f; // normaler Widerstand
    }
}

  
