using UnityEngine;

/// <summary>
/// Conecta todos los componentes primarios de la nave jugador.
/// </summary>


namespace SAT
{
public class Ship : MonoBehaviour
{    

    private ShipInput input;
    private ShipPhysics physics;
    public Gun[] guns;    


    // Referencia estática para saber si es la nave jugador, devuelve la nave si es posible, si no null.
    public static Ship PlayerShip { get { return playerShip; } }
    private static Ship playerShip;

    // Getters para objetos externos.

    public Vector3 Velocity { get { return physics.Rigidbody.velocity; } }
    public float Throttle { get { return input.throttle; } }

    public int vida = 100;

	Animator animacion;
    private void Awake()
    {
        animacion = GetComponent<Animator>();
        input = GetComponent<ShipInput>();
        physics = GetComponent<ShipPhysics>();
    }

    private void Update()
    {
        // Pasa la entrada a las físicas para mover la  nave.
        physics.SetPhysicsInput(new Vector3(input.strafe, 0.0f, input.throttle), new Vector3(input.pitch, input.yaw, input.roll));

            playerShip = this;
            // si aprietas el click izquierdo, dispara
            if (Input.GetMouseButton(0) == true)
            {
                foreach (var gun in guns)
                {
                    gun.Fire(Vector3.zero);
                }
            }
        
            //Si la nave está dañada aplicar animación

            if (vida <= 30)
        {
            // cambiamos a true la variable del animator
            animacion.SetBool("isDañado", true);
        }

             // Condición salida de aplicación
             if (Input.GetKey("escape"))
                 Application.Quit();
         }

}
}
