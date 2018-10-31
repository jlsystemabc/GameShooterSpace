/* Copyright (c) 2018
 * Author: Programmer C# - Jose Luiz Santos - Brazil
 */

using UnityEngine;
using System.Collections;

public class GerenciadorUI : MonoBehaviour {

	public GameObject[] objetosDoPause;
	public GameObject controladorDeNaveGO;
	private ControladorDeNave controladorDeNave;
	public GameObject vida_0;
	public GameObject vida_1;
	public GameObject vida_2;

	[HideInInspector]
	public int vidas = 3;


	// Use this for initialization
	void Start () {
		
		Time.timeScale = 1;
		controladorDeNave = controladorDeNaveGO.GetComponent<ControladorDeNave>();
		objetosDoPause = GameObject.FindGameObjectsWithTag("mostrarNoPause");

		EsconderPause();
	}

	// Update is called once per frame
	void Update () {

		// Usa o botão p para pausar e interromper o jogo;
		if(Input.GetKeyDown(KeyCode.P) && controladorDeNave.ocultarUIMenu == false)
		{
			if(Time.timeScale == 1)
			{
				Time.timeScale = 0;
				MostrarPause();
			} else if (Time.timeScale == 0){
				
				Time.timeScale = 1;
				EsconderPause();
			}
		}
	}


	// Recarrega a cena.
	public void RecarregarCena(){
		Application.LoadLevel(Application.loadedLevel);
	}

	// Controla o pause da cena.
	public void ControleDePause(){
		if(Time.timeScale == 1)
		{
			Time.timeScale = 0;
			MostrarPause();
		} else if (Time.timeScale == 0){
			Time.timeScale = 1;
			EsconderPause();
		}
	}
		
	public void MostrarPause(){
		foreach(GameObject g in objetosDoPause){
			g.SetActive(true);
		}
	}
		
	public void EsconderPause(){
		foreach(GameObject g in objetosDoPause){
			g.SetActive(false);
		}
	}

	public void EsconderIconeDeVida() {

				if (vidas == 2) {
					vida_0.SetActive (false);
				}

				else if (vidas == 1) {
					vida_1.SetActive (false);
				}

				else if (vidas == 0) {
					vida_2.SetActive (false);
				}
	}
		
	public void CarregarCena(string level){
		Application.LoadLevel(level);
	}
}
