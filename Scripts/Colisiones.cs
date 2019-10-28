using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAT
{
public class Colisiones : MonoBehaviour {

	public int visitados=0;
	private bool sol = false;
	private bool ishtar = false;
	private bool lilith = false;
	private Rigidbody player;
	void Start () {
		player = GetComponent<Rigidbody>();
	}
	
	void Update () {
		
	}
	void OnTriggerEnter(Collider other)
	{		
		// Comprobar que la nave ha visitado los cuerpos celestes y aumentar el contador para luego mostrarse.
		if(other.gameObject.CompareTag ("Cuerpo")){
			if(other.gameObject.Equals(GameObject.Find("Sol")) && !sol){
			visitados += 1;
			sol = true;
			}
			if(other.gameObject.Equals(GameObject.Find("Ishtar"))&& !ishtar){
			visitados += 1;
			ishtar = true;
			}
			if(other.gameObject.Equals(GameObject.Find("Lilith"))&& !lilith){
			visitados += 1;
			lilith = true;
			}

		}
	}
}
}

