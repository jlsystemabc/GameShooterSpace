/* Copyright (c) 2018
 * Author: Programmer C# - Jose Luiz Santos - Brazil
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControladorDeJogo: MonoBehaviour {

	public static ControladorDeJogo InstanciaCompartilhada;

	public Text textoLabel;
	private int atualScore = 0;
	public Text gameOverLabel;
	public Button botaoRestart;
	public Button botaoMainMenu;

	public GameObject inimigoTipo_1;
	public GameObject inimigoTipo_2;

	public GameObject subChefe;

	public float aguardarInicio = 1.0f;
	public float intervaloDeOnda = 2.0f;
	public float intervaloDeSpawn = 0.5f;
	public int inimigosPorOnda = 5;

	public GameObject barreiraEsq;                   
	public GameObject barreiraDir;                  // Referências aos limites da tela: Usado para garantir que o jogador não saia da tela
	public GameObject barreiraCima;                    
	public GameObject barreiraBaixo;                 

	void Awake () {
		InstanciaCompartilhada = this;
	}

	void Start () {
		StartCoroutine(SpawnarOndaInimiga());
	}

	IEnumerator SpawnarOndaInimiga () {
		yield return new WaitForSeconds (aguardarInicio);
		while (true) {
		  float tipoDeOnda = Random.Range(0.0f, 10.0f);
			for (int i = 0 ; i < inimigosPorOnda; i++) {
				Vector3 superiorEsq = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight + 2, 0));
				Vector3 superiorDir = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight + 2, 0));
				Vector3 posicaoDeSpawn = new Vector3 (Random.Range(superiorEsq.x, superiorDir.x), superiorEsq.y, 0);
				Quaternion rotacaoDeSpawn = Quaternion.Euler(0, 0, 180);

				if (tipoDeOnda >= 5.0f) {
					GameObject inimigo_1 = Pooler.InstanciaCompartilhada.ObterObjetosNoPool("Nave Inimiga 1"); 
					if (inimigo_1 != null) {
						inimigo_1.transform.position = posicaoDeSpawn;
						inimigo_1.transform.rotation = rotacaoDeSpawn;
						inimigo_1.SetActive(true);
					}

				} else {
					GameObject inimigo_2 = Pooler.InstanciaCompartilhada.ObterObjetosNoPool("Nave Inimiga 2"); 
					if (inimigo_2 != null) {
						inimigo_2.transform.position = posicaoDeSpawn;
						inimigo_2.transform.rotation = rotacaoDeSpawn;
						inimigo_2.SetActive(true);
					}

				} 
        yield return new WaitForSeconds (intervaloDeSpawn);
			} 
      yield return new WaitForSeconds (intervaloDeOnda);
		}
	}

	public void IncrementarScore(int incremento) {
		atualScore += incremento;
		textoLabel.text = "Score: " + atualScore;
	}

	public void ExibirGameOver() {
		gameOverLabel.rectTransform.anchoredPosition3D = new Vector3 (0, 0, 0);
		botaoRestart.GetComponent<RectTransform>().anchoredPosition3D = new Vector3 (0, -50f, 0);
		botaoMainMenu.GetComponent<RectTransform>().anchoredPosition3D = new Vector3 (0, -85f, 0);
	}

	public void RestaurarJogo() {
		SceneManager.LoadScene("Nivel_1");
	}
}
