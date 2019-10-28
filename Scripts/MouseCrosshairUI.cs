using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Actualiza la posición de este Objeto para reflejar la posición del ratónUpdates the position of this GameObject to reflect the position of the mouse
/// cuando el jugador usa el ratón, en caso contrario lo esconde.
/// </summary>
namespace SAT
{
public class MouseCrosshairUI : MonoBehaviour
{
    private Image crosshair;

    private void Awake()
    {
        crosshair = GetComponent<Image>();
    }

    private void Update()
    {
        if (crosshair != null && Ship.PlayerShip != null)

                crosshair.transform.position = Input.mousePosition;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Confined;
        }
    }
}

