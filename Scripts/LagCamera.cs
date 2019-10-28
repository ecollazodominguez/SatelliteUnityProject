using UnityEngine;

/// <summary>
/// Añade un ligero lag a la rotación de la cámara para hacer la tercera persona un poco más intersante.
/// Es necesario que siga un objeto para que lo siga correctamente.
/// </summary>
[RequireComponent(typeof(Camera))]
public class LagCamera : MonoBehaviour
{    
    [Tooltip("Velocidad a la que la cámara rota. (Camera usa Slerp para rotation.)")]
    public float rotateSpeed = 90.0f;

    [Tooltip("Si el objeto en el que está asignado usa FixedUpdate para el movimiento, poner en true para un movimiento más suave.")]
    public bool usedFixedUpdate = true;

    private Transform target;
    private Vector3 startOffset;

    private void Start()
    {
        target = transform.parent;

        if (target == null)
            Debug.LogWarning(name + ":  Lag de cámara no funcionará correctamente sin un target.");
        if (transform.parent == null)
            Debug.LogWarning(name + ": Lag de cámara no funcionará si no está asignada a un objeto como hijo.");

        startOffset = transform.localPosition;
        transform.SetParent(null);
    }

    private void Update()
    {
        if (!usedFixedUpdate)
            UpdateCamera();
    }

    private void FixedUpdate()
    {
        if (usedFixedUpdate)
            UpdateCamera();
    }

    private void UpdateCamera()
    {
        if (target != null)
        {
            transform.position = target.TransformPoint(startOffset);
            transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, rotateSpeed * Time.deltaTime);
        }
    }
}
