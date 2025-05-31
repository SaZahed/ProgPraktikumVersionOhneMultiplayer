using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] private float thrustForce = 500f; // schubkraft motor
    [SerializeField] private float dragCoefficient = 0.1f; // widerstandskoeffizient
    [SerializeField] private float rotationSpeed = 50f; // drehgeschgeschwindigkeit
   
    [SerializeField] private float anchorDropForce = 100f; // Kraft beim Anker fallen lassen
    [SerializeField] private float anchorLiftForce = 50f; // Kraft beim Anker heben
    [SerializeField] private bool anchorDropped = false; // bool wert anfangs auf false 

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
        float thrustInput = Input.GetAxis("Vertical"); // w/s schubsteuerung
        float turnInput = Input.GetAxis("Horizontal"); // a/d drehsteuerung

        applyForces(thrustInput);
        appplyRoatation(turnInput);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            dropAnchor();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            liftAnchor();
        }
    }
    private void appplyRoatation(float turnInput)
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
        thrustForce = 0; //das schiff soll stoppen

       
    }
    private void liftAnchor()
    {
            anchorDropped = false;
            thrustForce = 500f; // Schubkraft wiederherstellen

    }
}

  
