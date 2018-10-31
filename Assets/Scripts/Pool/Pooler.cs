/* Copyright (c) 2018
 * Author: Programmer C# - Jose Luiz Santos - Brazil
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ItemObjetoDePool {
	
	public int quantidadeParaPool;
	public GameObject objetosParaPool;
	public bool deveExpandir;
}
	
public class Pooler : MonoBehaviour {

	public static Pooler InstanciaCompartilhada;
	public List<GameObject> objetosNoPool;
	public List<ItemObjetoDePool> itensParaPool;


	void Start () {
		
		// Lista com objetos no Pool
		objetosNoPool = new List<GameObject>();
		foreach (ItemObjetoDePool item in itensParaPool) {
			for (int i = 0; i < item.quantidadeParaPool; i++) {
				GameObject obj = (GameObject)Instantiate(item.objetosParaPool);
				obj.SetActive(false);
				objetosNoPool.Add(obj);
			}
		}
	}


	void Awake() {

		// Instancia para ser usada para nao sobrecarregar com GetComponets<>().
		InstanciaCompartilhada = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Função para reusar os mesmos gameobjects instaciados.
	public GameObject ObterObjetosNoPool(string tag) {
		
		for (int i = 0; i < objetosNoPool.Count; i++) {
			if (!objetosNoPool[i].activeInHierarchy && objetosNoPool[i].tag == tag) {
				return objetosNoPool[i];
			}
		}
		foreach (ItemObjetoDePool item in itensParaPool) {
			if (item.objetosParaPool.tag == tag) {
				if (item.deveExpandir) {
					GameObject obj = (GameObject)Instantiate(item.objetosParaPool);
					obj.SetActive(false);
					objetosNoPool.Add(obj);
					return obj;
				}
			}
		}
		return null;
	}
}
