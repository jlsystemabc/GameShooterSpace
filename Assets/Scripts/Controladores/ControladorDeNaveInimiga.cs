/* Copyright (c) 2018
 * Author: Programmer C# - Jose Luiz Santos - Brazil
 */

using UnityEngine;
using System.Collections;

public class ControladorDeNaveInimiga : MonoBehaviour {

	public GameObject powerUp;
	public GameObject explosao;
	public GameObject balaInimiga;
	public float tempoRecarregarMin = 1.0f;
	public float tempoRecarregarMax = 2.0f;

	// Use this for initialization
	void Start () {
    StartCoroutine("Atirar");
	}

  IEnumerator Atirar() {
    yield return new WaitForSeconds((Random.Range(tempoRecarregarMin, tempoRecarregarMax)));
    while (true) {
		  Instantiate(balaInimiga, gameObject.transform.position, gameObject.transform.rotation);
      yield return new WaitForSeconds((Random.Range(tempoRecarregarMin, tempoRecarregarMax)));
    }
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Barreira" && other.gameObject.name != "Barreira Superior") {
			gameObject.SetActive(false);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Bala do Jogador") {
      ControladorDeJogo.InstanciaCompartilhada.IncrementarScore(5);
			float numeroRandomico = Random.Range(0.0f, 10.0f);
			if (numeroRandomico > 9.0f) {
				Instantiate(powerUp, gameObject.transform.position, gameObject.transform.rotation);
			}
			Instantiate(explosao, gameObject.transform.position, gameObject.transform.rotation);
			other.gameObject.SetActive(false);
			gameObject.SetActive(false);
		}
	}
}
