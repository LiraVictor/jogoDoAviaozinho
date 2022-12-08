using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour {

	public GameObject inimigo;

	// Use this for initialization
	void Comecou () {
		InvokeRepeating("CriaInimigo", 0.0f, 1.5f);
	}

    void Acabou()
    {
        CancelInvoke("CriaInimigo");
    }

    void CriaInimigo()
	{
		float alturaAleatoria = 10.0f * Random.value -4;
		GameObject novoInimigo = Instantiate(inimigo);
		novoInimigo.transform.position = new Vector2(15.0f, alturaAleatoria);
	}
}
