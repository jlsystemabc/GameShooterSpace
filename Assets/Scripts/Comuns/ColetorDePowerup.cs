/* Copyright (c) 2018
 * Author: Programmer C# - Jose Luiz Santos - Brazil
 */

using UnityEngine;
using System.Collections;

public class ColetorDePowerup : MonoBehaviour {

	private AudioSource audioPowerup;
	private CircleCollider2D colisorPowerup;
	private Renderer renderizadorPowerup;


	void Start () {
		audioPowerup = gameObject.GetComponent<AudioSource>();
		colisorPowerup = gameObject.GetComponent<CircleCollider2D>();
		renderizadorPowerup = gameObject.GetComponent<Renderer>();
	}
	
	public void PowerupColetado() {
		colisorPowerup.enabled = false;
		renderizadorPowerup.enabled = false;
		audioPowerup.Play();
		Destroy(gameObject, audioPowerup.clip.length);
	}
}
