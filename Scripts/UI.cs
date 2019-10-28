using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace SAT{
public class UI : MonoBehaviour {

	public Text visitas;
   	public Text aceleracion;
	public Text vida;

	public Text WinLose;

	private Colisiones col;
	private ShipInput shipInput;
	private Ship ship;
	private GameObject nave;

	public GameObject restart;
	private int time;
	void Start () {
		nave = GameObject.Find("Nave");
		col = nave.GetComponent<Colisiones>();
		shipInput = nave.GetComponent<ShipInput>();
		ship = nave.GetComponent<Ship>();
		WinLose.text = "";
		restart.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		SetVisitas();
		SetAcel();
		SetVida();
		setWinLose();
	}

	//funcion para mostrar cuerpos visitados
	void SetVisitas ()
    {
		if (col.visitados >=0 && col.visitados <3 )
        visitas.text = "C.Celestes visitados: " + col.visitados + "/3";
        else if (col.visitados >= 3)
        {
            visitas.text = "Una nave grande ha aparecido, ¡Derrótala!";
			
        }
    }

	//funcion para mostrar aceleracion
	void SetAcel()
	{
		aceleracion.text = "Acel: "+ shipInput.throttle*100 +"%";
	}

	//funcion para mostrar vida
	void SetVida()
	{
		vida.text = "Vida: "+ ship.vida+"%";
	}

	//funcion para mostrar si has perdido o ganado segun las condiciones y boton de reseteado
	void setWinLose()
	{
		if (Bullet.jefeDestruido){
			WinLose.text = "¡Has ganado!";
			nave.SetActive(false);
			restart.SetActive(true);
		}else if (Bullet.naveDestruida){
			WinLose.text = "¡Has perdido!";
			restart.SetActive(true);
		}
	}
	         public void RestartGame() {
             SceneManager.LoadScene(SceneManager.GetActiveScene().name); // carga la escena actual
			 Bullet.naveDestruida = false;
			 Bullet.jefeDestruido = false;
         }
}
}
