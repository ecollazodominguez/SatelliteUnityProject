using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAT{
public class JefeSpawner : MonoBehaviour {
	private Colisiones col;
	public Transform prefab;

	private GameObject nave;

	private Vector3 spawnpos = Vector3.zero;

	public float range = 2;
	void Start () {
		nave = GameObject.Find("Nave");
		col = nave.GetComponent<Colisiones>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (col.visitados == 3){
			Transform t = Instantiate(prefab,spawnpos,Quaternion.identity);
			col.visitados = -1;
		}
	}
}
}
