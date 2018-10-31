/* Copyright (c) 2018
 * Author: Programmer C# - Jose Luiz Santos - Brazil
 */

using UnityEngine;
using System.Collections;

public class MovimentoRandomico : MonoBehaviour {

	public float velocidadeMovimento = 5.0f;

	private float maxX;
	private float minX;
	private float maxY;
	private float minY;

	private float tMudanca = 0.0f; // força uma nova direção na primeira atualização update().
	private float randomicoX;
	private float randomicoY;

  void Start () {
    maxX = ControladorDeJogo.InstanciaCompartilhada.barreiraDir.transform.position.x;
    minX = ControladorDeJogo.InstanciaCompartilhada.barreiraEsq.transform.position.x;
    maxY = ControladorDeJogo.InstanciaCompartilhada.barreiraCima.transform.position.y;
    minY = ControladorDeJogo.InstanciaCompartilhada.barreiraBaixo.transform.position.y;
  }

	void Update() {

		// Muda para uma nova direção randomica em intervalos randomicos.
		if (Time.time >= tMudanca) {
			randomicoX = Random.Range(-2.0f, 2.0f);
			randomicoY = Random.Range(-2.0f, 2.0f); //  Entre -2.0 and 2.0 é retornado.

			// Define um intervalo randomico entre 0.5 e 1.5
			tMudanca = Time.time + Random.Range(0.5f,1.5f);
		}
		Vector3 novaPosicao = new Vector3(randomicoX, randomicoY, 0);
		transform.Translate(novaPosicao * velocidadeMovimento * Time.deltaTime);

		// Se alguma barreira é colidida, muda a direção.
		if (transform.position.x >= maxX || transform.position.x <= minX) {
			randomicoX = -randomicoX;
		}
		if (transform.position.y >= maxY || transform.position.y <= minY) {
			randomicoY = -randomicoY;
		}
		Vector3 posicaoFixada = transform.position;
		posicaoFixada.x = Mathf.Clamp(transform.position.x, minX, maxX);
		posicaoFixada.y = Mathf.Clamp(transform.position.y, minY, maxY);
		transform.position = posicaoFixada;
	}
}