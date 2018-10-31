/* Copyright (c) 2018
 * Author: Programmer C# - Jose Luiz Santos - Brazil
 */

using UnityEngine;
using System.Collections;

public class MovimentoBasico : MonoBehaviour {

  private Rigidbody2D objetoCorpoRigido;
  public float velocidade;

  
	void OnEnable () {
	objetoCorpoRigido = transform.GetComponent<Rigidbody2D>();
	objetoCorpoRigido.velocity = transform.up * velocidade;
  }
}
