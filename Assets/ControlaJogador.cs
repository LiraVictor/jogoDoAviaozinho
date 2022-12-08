using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlaJogador : MonoBehaviour
{

    bool comecou;
    bool acabou;
    Rigidbody2D corpoJogador;
    Vector2 forcaImpulso = new Vector2(0, 500);

    public GameObject particulaPena;
    public Text textoScore;

    GameObject gameengine;

    int pontuacao;

    // Use this for initialization
    void Start()
    {
        gameengine = GameObject.FindGameObjectWithTag("MainCamera");
        corpoJogador = GetComponent<Rigidbody2D>();

        textoScore.transform.position = new Vector2(Screen.width / 2, Screen.height - 150);
        textoScore.text = "Toque para iniciar";
        textoScore.fontSize = 35;
    }

    // Update is called once per frame
    void Update()
    {
        if (!acabou)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (!comecou)
                {
                    comecou = true;
                    corpoJogador.isKinematic = false;

                    textoScore.text = pontuacao.ToString();
                    textoScore.fontSize = 100;

                    gameengine.SendMessage("Comecou");
                }

                corpoJogador.velocity = new Vector2(0, 0);
                corpoJogador.AddForce(forcaImpulso);

                GameObject peninhas = Instantiate(particulaPena);
                Vector3 posicaoFelpudo = this.transform.position + new Vector3(0, 1, 0);
                peninhas.transform.position = this.transform.position;

            }

            float alturaFelpudoEmPixels = Camera.main.WorldToScreenPoint(transform.position).y;

            if (alturaFelpudoEmPixels > Screen.height || alturaFelpudoEmPixels < 0)
            {
                //Destroy(this.gameObject);
                GetComponent<Collider2D>().enabled = false;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-300, 0));
                GetComponent<Rigidbody2D>().AddTorque(300f);
                GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.5f, 0.5f);
                FimDeJogo();
            }

            transform.rotation = Quaternion.Euler(0, 0, corpoJogador.velocity.y * 3);
        }
    }

    void OnCollisionEnter2D()
    {

        if (!acabou)
        {
            acabou = true;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-300, 0));
            GetComponent<Rigidbody2D>().AddTorque(300f);
            GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.75f, 0.75f);
            FimDeJogo();
        }
    }

    void MarcaPonto()
    {
        pontuacao++;
        textoScore.text = pontuacao.ToString();
    }

    void FimDeJogo()
    {
        gameengine.SendMessage("Acabou");
        Invoke("RecarregarCena", 2);
    }
    void RecarregarCena()
    {
        Application.LoadLevel("MinhaCena");
    }
}    
