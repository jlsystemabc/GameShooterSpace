/* Copyright (c) 2018
 * Author: Programmer C# - Jose Luiz Santos - Brazil
 */

using UnityEngine;
using System.Collections;

public class AutoDestruirParticulas : MonoBehaviour {
	private ParticleSystem particulas;

	public void Start() {
		particulas = GetComponent<ParticleSystem>();
	}

	public void Update() {
		if (particulas) {
			if (!particulas.IsAlive()) {
				Destroy(gameObject);
			}
		}
	}
}