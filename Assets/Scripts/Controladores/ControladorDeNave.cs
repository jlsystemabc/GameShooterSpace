/* Copyright (c) 2018
 * Author: Programmer C# - Jose Luiz Santos - Brazil
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControladorDeNave : MonoBehaviour {

	public float velocidadeDeMovimento = 10.0f;						        // Velocidade de movimento da nave
	public float tempoDeCarregamentoDaTorreta_360 = 2.0f;	// Tempo de recarregamento da torreta
	public GameObject armaInicial;						        // Torreta inicial do jogador
	public List<GameObject> torretaTripla;			  
	public List<GameObject> upgradeDeTorreta;			    // Referencia para upgrades da torreta
	public List<GameObject> torretas360;			 
	public List<GameObject> torretasDoJogadorAtiva;		  //
	public GameObject explosao;						          // Referencia para o prefab de explosão
	public GameObject balaDoJogador;						        
    public ParticleSystem turbinaDoJogador;             
	private Rigidbody2D corpoRigidoJogador;				      
	private Renderer renderizadorNaveJogador;					        
	private CircleCollider2D colisorDoJogador;			   

	[HideInInspector ]
	public bool ocultarUIMenu = false;

	public GameObject gerenciadorDeUIGO;
	private GerenciadorUI gerenciadorUI;
    public int atualizarEstado = 0;						          // A referencia para estados do jogador

    private GameObject barreiraEsq;                   
    private GameObject barreiraDir;                  // Limites da tela
    private GameObject barreiraCima;                  
    private GameObject barreiraBaixo;               

	private AudioSource somDeTiro;					     


	void Start () {
		barreiraEsq = ControladorDeJogo.InstanciaCompartilhada.barreiraEsq;
		barreiraDir = ControladorDeJogo.InstanciaCompartilhada.barreiraDir;
		barreiraCima = ControladorDeJogo.InstanciaCompartilhada.barreiraCima;
		barreiraBaixo = ControladorDeJogo.InstanciaCompartilhada.barreiraBaixo;

		colisorDoJogador = gameObject.GetComponent<CircleCollider2D>();
		renderizadorNaveJogador = gameObject.GetComponent<Renderer>();
		torretasDoJogadorAtiva = new List<GameObject>();
		torretasDoJogadorAtiva.Add(armaInicial);
		somDeTiro = gameObject.GetComponent<AudioSource>();
		corpoRigidoJogador = GetComponent<Rigidbody2D>();
		gerenciadorUI = gerenciadorDeUIGO.GetComponent<GerenciadorUI> ();
	}
	
	void Update () {
		if (Input.GetKeyDown("space")) {
      Atirar();
		}
		float xDir = Input.GetAxis("Horizontal");
		float yDir = Input.GetAxis("Vertical");
		corpoRigidoJogador.velocity = new Vector2(xDir * velocidadeDeMovimento, yDir * velocidadeDeMovimento);
		corpoRigidoJogador.position = new Vector2
			(
				Mathf.Clamp (corpoRigidoJogador.position.x, barreiraEsq.transform.position.x, barreiraDir.transform.position.x),
				Mathf.Clamp (corpoRigidoJogador.position.y, barreiraBaixo.transform.position.y, barreiraCima.transform.position.y)
			);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Powerup") {
			ColetorDePowerup scriptPowerup = other.gameObject.GetComponent<ColetorDePowerup>();
			scriptPowerup.PowerupColetado();
			AtualizarArmas();
		} 
    else if (other.gameObject.tag == "Nave Inimiga 1" || other.gameObject.tag == "Nave Inimiga 2" || other.gameObject.tag == "Laser Inimigo") {

			gerenciadorUI.vidas = gerenciadorUI.vidas - 1;
			gerenciadorUI.EsconderIconeDeVida ();

			if (gerenciadorUI.vidas == 0) {

				ocultarUIMenu = true;

				ControladorDeJogo.InstanciaCompartilhada.ExibirGameOver();  // Se o jogador for atingido por um laser inimigo ou uma nave inimiga GameOver. 
				renderizadorNaveJogador.enabled = false;       // Não podemos destruir o objeto de jogo do jogador imediatamente ou qualquer código deste ponto não será executado
				colisorDoJogador.enabled = false;       // Desativa o renderizador de jogadores para que o jogador não seja mais exibido e desligue o colisor de jogadores
				turbinaDoJogador.Stop();

				Instantiate(explosao, transform.position, transform.rotation);   // Instancia a explosão ... um no centro e alguns adicionais em torno da localização dos jogadores para um maior estrondo!

				for (int i = 0 ; i < 8; i++) {
					Vector3 deslocamentoRandomico = new Vector3 (transform.position.x + Random.Range(-0.6f, 0.6f), transform.position.y + Random.Range(-0.6f, 0.6f), 0.0f); 
					Instantiate(explosao, deslocamentoRandomico, transform.rotation);
				}
				Destroy(gameObject, 1.0f); // Segundo parâmetro em Destroy é um atraso para garantir o término da explosão antes de removermos o player da cena.
			}
		}
	}

  void Atirar() {
    foreach(GameObject torreta in torretasDoJogadorAtiva) {
			
			GameObject bala = Pooler.InstanciaCompartilhada.ObterObjetosNoPool("Bala do Jogador");

			if (bala != null) {
				bala.transform.position = torreta.transform.position;
				bala.transform.rotation = torreta.transform.rotation;
				bala.SetActive(true);
			}
    }
    somDeTiro.Play();
  }

	void AtualizarArmas() {     

		// Verifica o estado de atualização do player e adiciona os objetos de jogo torreta apropriados à Lista de torretas ativa.

		if (atualizarEstado == 0) {
			foreach(GameObject torreta in torretaTripla) {
				torretasDoJogadorAtiva.Add(torreta);
			}
		} 
    else if (atualizarEstado == 1) {
			foreach(GameObject torreta in upgradeDeTorreta) {
				torretasDoJogadorAtiva.Add(torreta);
			}
		} 
    else if (atualizarEstado == 2) {
			StartCoroutine("AtivarTorreta360");
    } 
    else {
      return;
    }
		atualizarEstado ++;
	}

    IEnumerator AtivarTorreta360() {

    // Torreta automática.
    // A corotina ativa a torreta 360

		while (true) {
			foreach(GameObject torreta in torretas360) {
				
				GameObject bala = Pooler.InstanciaCompartilhada.ObterObjetosNoPool("Bala do Jogador");

				if (bala != null) {
					bala.transform.position = torreta.transform.position;
					bala.transform.rotation = torreta.transform.rotation;
					bala.SetActive(true);
				}
			}
			somDeTiro.Play();
			yield return new WaitForSeconds(tempoDeCarregamentoDaTorreta_360);
		}
	}
}
