using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Clase específicar para las entradas.
/// </summary>
namespace SAT
{
public class ShipInput : MonoBehaviour
{
   

    [Range(-1, 1)]
    public float pitch;
    [Range(-1, 1)]
    public float yaw;
    [Range(-1, 1)]
    public float roll;
    [Range(-1, 1)]
    public float strafe;
    [Range(0, 1)]

    public float throttle;

    // Lo rápido que reacciona el acelerador a la entrada
    private const float THROTTLE_SPEED = 0.5f;

    // Mantener una referencia a Ship por si acaso.
    public Ship ship;

    private void Awake()
    {
        ship = GetComponent<Ship>();
    }

    private void Update()
    {

                strafe = Input.GetAxis("Horizontal");
                SetStickCommandsUsingMouse();
                UpdateKeyboardThrottle(KeyCode.W, KeyCode.S);
          

    }
    

    /// <summary>
    /// Simular un joystick virtusal usando un raton, cuando el ratón está en el centro de la pantalla es como un stick centrado
    /// </summary>
    public void SetStickCommandsUsingMouse()
    {
        Vector3 mousePos = Input.mousePosition;

        // Figurar la posición relativa más cercana al centro de la pantalla
        // (0, 0) es el centro, (-1, -1) es abajo izquierda, (1, 1) es arriba derecha.      
        pitch = (mousePos.y - (Screen.height * 0.5f)) / (Screen.height* 0.5f);
        yaw = (mousePos.x - (Screen.width * 0.5f)) / (Screen.width * 0.5f);

        // Asegurar con Clamp que no exceda los limites.
        pitch = -Mathf.Clamp(pitch, -1.0f, 1.0f);
        yaw = Mathf.Clamp(yaw, -1.0f, 1.0f);
    }

    /// <summary>
    /// Función para indicar con qué teclas aumentas o disminuyes la aceleración
    /// </summary>
    private void UpdateKeyboardThrottle(KeyCode increaseKey, KeyCode decreaseKey)
    {
        float target = throttle;

        if (Input.GetKey(increaseKey))
            target = 1.0f;
        else if (Input.GetKey(decreaseKey))
            target = 0.0f;

        throttle = Mathf.MoveTowards(throttle, target, Time.deltaTime * THROTTLE_SPEED);
    }

}
}
   
