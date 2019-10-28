using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SAT{
public class EnemigoF : MonoBehaviour {

	private Transform target;
    public float moveSpeed=55f;
	Rigidbody rigidbody;

    public int vida = 200;

     public Gun[] guns;  

	void Start () {
		target = GameObject.Find("Nave").transform;
		rigidbody = target.GetComponent<Rigidbody>();
	}
	
	void Update () {
    
    //Código para mirar hacia el jugador
    this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
    Quaternion.LookRotation(target.transform.position - this.transform.position),3 * Time.deltaTime);

    //Comprobamos la distancia y segun esta aumentamos, disparamos o disminuimos velocidad

    // Menos de 600 de distancia para y dispara.

  if (Vector3.Distance(this.transform.position,target.transform.position) < 600){
    foreach (var gun in guns)
                {
                    gun.Fire(Vector3.zero);
                }

    // Menos de 800 de distancia dispara, reduce velocidad y rota a menor velocidad.

  }else if (Vector3.Distance(this.transform.position,target.transform.position) < 800){

    this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
    Quaternion.LookRotation(target.transform.position - this.transform.position),0.3f * Time.deltaTime);

    this.transform.position += this.transform.forward * (moveSpeed*2) * Time.deltaTime;

    foreach (var gun in guns)
                {
                    gun.Fire(Vector3.zero);
                }


    // Menos de 1500 de distancia dispara, aumenta la velocidad normal.

  }else if (Vector3.Distance(this.transform.position,target.transform.position) > 1100 && Vector3.Distance(this.transform.position,target.transform.position) < 1400){
  
  this.transform.position += this.transform.forward * (moveSpeed*18.5f) * Time.deltaTime;

   foreach (var gun in guns)
                {
                    gun.Fire(Vector3.zero);
                }

    // Mayor de 1400 de distancia mantiene velocidad constante reducida.

  }else if (Vector3.Distance(this.transform.position,target.transform.position) > 1400){

  this.transform.position += this.transform.forward * (moveSpeed*8) * Time.deltaTime;

  }else{
    // En caso de que no aplique ninguno velocidad constante normal.
 this.transform.position += this.transform.forward * (moveSpeed*10) * Time.deltaTime;
  }
	}



}
}