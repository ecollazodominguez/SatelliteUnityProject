using UnityEngine;

public enum RandomSpawnerShape
{
    Box,
    Sphere,
}

public class RandomAreaSpawner : MonoBehaviour
{
    [Header("Opciones generales:")]

    [Tooltip("Prefab a instanciar.")]
    public Transform prefab;

    [Tooltip("Forma geométrica en la que instanciar el prefab. Enum te permite elegir entre varias.")]
    public RandomSpawnerShape spawnShape = RandomSpawnerShape.Sphere;

    [Tooltip("Multiplicador para la aparición de la forma en cada eje.")]
    public Vector3 shapeModifiers = Vector3.one;

    [Tooltip("Cuántos prefabs a instanciar.")]
    public int asteroidCount = 1000;

    [Tooltip("Distancia desde el centro del objeto donde los prefabs aparecerán")]
    public float range = 8000.0f;

    [Tooltip("Si debería el profab tener una rotación aplicada.")]
    public bool randomRotation = true;

    [Tooltip("Mínimo/máximo de escala aleatoria a aplicar.")]
    public Vector2 scaleRange = new Vector2(1.0f, 3.0f);

    [Header("Propiedades Rigidbody:")]

    [Tooltip("Aplicar una velocidad desde 0 a una dirección aleatoria.")]
    public float velocity = 15f;

    [Tooltip("Aplicar una velocidad angular (grados/s) desde 0 a una dirección aleatoria.")]
    public float angularVelocity = 15f;

    [Tooltip("Si verdadero, aumenta la masa de este objeto basada en su escala.")]
    public bool scaleMass = true;


    void Start()
    {
        if (prefab != null)
        {
            for (int i = 0; i < asteroidCount; i++)
                CreateAsteroid();
        }
    }

    private void CreateAsteroid()
    {
        Vector3 spawnPos = Vector3.zero;
         
        // Crea una posición aleatoria basándose en la forma geométrica y el rango.
        if (spawnShape == RandomSpawnerShape.Box)
        {
            spawnPos.x = Random.Range(-range, range) * shapeModifiers.x;
            spawnPos.y = Random.Range(-range, range) * shapeModifiers.y;
            spawnPos.z = Random.Range(-range, range) * shapeModifiers.z;
        }
        else if (spawnShape == RandomSpawnerShape.Sphere)
        {
            spawnPos = Random.insideUnitSphere * range;
            spawnPos.x *= shapeModifiers.x;
            spawnPos.y *= shapeModifiers.y;
            spawnPos.z *= shapeModifiers.z;
        }

        // Offset position para igualar la posición al Objeto padre.
        spawnPos += transform.position;

        // Aplica una rotation alearoria si es necesario.
        Quaternion spawnRot = (randomRotation) ? Random.rotation : Quaternion.identity;

        // Crea el objeto y asigna el (padre) a este objeto para organización de escena.
        Transform t = Instantiate(prefab, spawnPos, spawnRot) as Transform;
        t.SetParent(transform);

        // Aplica escala.
        float scale = Random.Range(scaleRange.x, scaleRange.y);
        t.localScale = Vector3.one * scale;

        // aplica valores Rigidbody
        Rigidbody r = t.GetComponent<Rigidbody>();
        if (r)
        {
            if (scaleMass)
                r.mass *= scale * scale * scale;

            r.AddRelativeForce(Random.insideUnitSphere * velocity, ForceMode.VelocityChange);
            r.AddRelativeTorque(Random.insideUnitSphere * angularVelocity * Mathf.Deg2Rad, ForceMode.VelocityChange);
        }
    }

    public void CreateNewAstroid()
    {
        CreateAsteroid();
    }
}
