/* Copyright (c) 2018
 * Author: Programmer C# - Jose Luiz Santos - Brazil
 */

using UnityEngine;
using System.Collections;

public class DestruirNaBarreira : MonoBehaviour {

  // Assegurar que o objeto deixou a tela antes de ser removido.
  void OnTriggerExit2D(Collider2D other) {
		
		if (other.gameObject.tag == "Barreira") {
			
			if (gameObject.tag == "Bala do Jogador") {
				gameObject.SetActive(false);
			} else {
				Destroy(gameObject);
			}
		}
  }
}
