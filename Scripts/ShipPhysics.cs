using UnityEngine;

/// <summary>
/// Aplica fuerza linear y angular a la nave
/// This is based on the ship physics from https://github.com/brihernandez/UnityCommon/blob/master/Assets/ShipPhysics/ShipPhysics.cs
/// </summary>
public class ShipPhysics : MonoBehaviour
{
    [Tooltip("X: Propulsion lateral\nY: Propulsion vertical\nZ: Propulsion longitunidal")]
    public Vector3 linearForce = new Vector3(100.0f, 100.0f, 100.0f);

    [Tooltip("X: Inclinacion\nY: Yaw\nZ: Giro")]
    public Vector3 angularForce = new Vector3(100.0f, 100.0f, 100.0f);

    [Range(0.0f, 1.0f)]
    [Tooltip("Multiplicador de la propulsion longitunidal cuando se quiere frenar")]
    public float reverseMultiplier = 1.0f;

    [Tooltip("Multiplicador para todas las fuerzas, se puede usar para que los numeros de las fuerzas sean menores y  másentendibles")]
    public float forceMultiplier = 100.0f;

    public Rigidbody Rigidbody { get { return rbody; } }

    private Vector3 appliedLinearForce = Vector3.zero;
    private Vector3 appliedAngularForce = Vector3.zero;

    private Rigidbody rbody;

    public Vector3 velocity;
    void Awake()
    {
        rbody = GetComponent<Rigidbody>();
        if (rbody == null)
        {
            Debug.LogWarning(name + ": ShipPhysics no tiene rigidbody.");
        }
    }

    void FixedUpdate()
    {
        if (rbody != null)
        {
            velocity = appliedLinearForce * forceMultiplier;
            rbody.AddRelativeForce(appliedLinearForce * forceMultiplier, ForceMode.Force);
            rbody.AddRelativeTorque(appliedAngularForce * forceMultiplier, ForceMode.Force);
        }
    }

    /// <summary>
    /// Fija la cantidad de Fuerza linear y angular es aplicada a la nave.
    /// cada componente del vector está en un rango de -1 a 1 sin estar restringido.
    /// </summary>
    public void SetPhysicsInput(Vector3 linearInput, Vector3 angularInput)
    {
        appliedLinearForce = MultiplyByComponent(linearInput, linearForce);
        appliedAngularForce = MultiplyByComponent(angularInput, angularForce);
    }

    /// <summary>
    /// Devuelve un Vector3 donde cada componente del vector A es multiplciado por el equivalente del Vector B.
    /// </summary>
    private Vector3 MultiplyByComponent(Vector3 a, Vector3 b)
    {
        Vector3 ret;

        ret.x = a.x * b.x;
        ret.y = a.y * b.y;
        ret.z = a.z * b.z;

        return ret;
    }
}