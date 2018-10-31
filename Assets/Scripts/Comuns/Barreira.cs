/* Copyright (c) 2018
 * Author: Programmer C# - Jose Luiz Santos - Brazil
 */

using UnityEngine;
using System.Collections;

public class Barreira : MonoBehaviour {

	public enum localizacaoBarreira { 
		ESQ, CIMA, DIR, BAIXO
	};
	public localizacaoBarreira direction;
	private BoxCollider2D barreira;
  	public float larguraBarreira = 0.8f;
	public float excesso = 1.0f; // Adicionado um excesso para assegurar que não haverá chances de perder o objeto depois que ele sair do colisor.
                                
	void Start () {


		// Pega as coordenadas dos cantos da viewport da camera.
		Vector3 superiorEsq = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, 0));
		Vector3 superiorDir = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0));
		Vector3 inferiorEsq = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
		Vector3 inferiorDir = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, 0));

		// Pega o collider 2D pra barreira.
		barreira = GetComponent<BoxCollider2D>();

		// Dependendo da direção atribuída da barreira, ajuste o tamanho e a posição baseados no viewport da camera.
		if (direction == localizacaoBarreira.CIMA) {
      barreira.size = new Vector2(Mathf.Abs(superiorEsq.x) + Mathf.Abs(superiorDir.x) + excesso, larguraBarreira);
      barreira.offset = new Vector2(0, larguraBarreira/2);
			transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight, 1)) ;
		}	
		if (direction == localizacaoBarreira.BAIXO) {
      barreira.size = new Vector2(Mathf.Abs(superiorEsq.x) + Mathf.Abs(superiorDir.x) + excesso, larguraBarreira);
      barreira.offset = new Vector2(0, -larguraBarreira/2);
			transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, 0, 1)) ;
		}
		if (direction == localizacaoBarreira.ESQ) {
      barreira.size = new Vector2(larguraBarreira, Mathf.Abs(inferiorEsq.y) + Mathf.Abs(inferiorDir.y) + excesso);
      barreira.offset = new Vector2(-larguraBarreira/2, 0);
			transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight / 2, 1)) ;
		}
		if (direction == localizacaoBarreira.DIR) {
      barreira.size = new Vector2(larguraBarreira, Mathf.Abs(inferiorEsq.y) + Mathf.Abs(inferiorDir.y) + excesso);
      barreira.offset = new Vector2(larguraBarreira/2, 0);
			transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight / 2, 1)) ;
		}
	}
}
