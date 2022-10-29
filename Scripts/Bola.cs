using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Bola : MonoBehaviour
{
    public float velocidad = 30.0f;

    public int golesDerecha = 0;
    public int golesIzquierda = 0;

    public Text contadorDerecha;
    public Text contadorIzquierda;

    AudioSource fuenteDeAudio;

    public AudioClip audioGol, audioRaqueta, audioRebote;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * velocidad;

        contadorIzquierda.text = golesIzquierda.ToString();
        contadorDerecha.text = golesDerecha.ToString();

        fuenteDeAudio = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D micolision) 
    {
        if(micolision.gameObject.tag == "ParedInvisible")
        {
            Physics2D.IgnoreCollision(micolision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        if(micolision.gameObject.name == "RaquetaIzquierda")
        {
            //Vectores de X , Y
            int x = 1;
            int y = direccionY(transform.position,micolision.transform.position);

            //calculo la direccion
            Vector2 direccion = new Vector2(x, y);
            GetComponent<Rigidbody2D>().velocity = direccion * velocidad;

            fuenteDeAudio.clip = audioRaqueta;
            fuenteDeAudio.Play();
        }

        if(micolision.gameObject.name == "RaquetaDerecha")
        {
            //Vectores de X , Y
            int x = 1;
            int y = direccionY(transform.position,micolision.transform.position);

            //calculo la direccion
            Vector2 direccion = new Vector2(x, y);
            GetComponent<Rigidbody2D>().velocity = direccion * velocidad;

            fuenteDeAudio.clip = audioRaqueta;
            fuenteDeAudio.Play();
        }

        if(micolision.gameObject.name == "Arriba" || micolision.gameObject.name == "Abajo")
        {
            fuenteDeAudio.clip = audioRebote;
            fuenteDeAudio.Play();
        }
    }

    int direccionY(Vector2 posicionBola, Vector2 posicionRaqueta)
    {
        if(posicionBola.y > posicionRaqueta.y)
            return 1;

        else if(posicionBola.y < posicionRaqueta.y)
            return -1;

        else
            return 0;
    }

    public void reiniciarBola(string direccion)
    {
        //posicion 0 de la bola
        transform.position = Vector2.zero; //Vector2.zero es lo mismo que new Vector2(0,0)

        //velocidad inicial de la bola
        velocidad = 30;
        //velocidad y direccion
        if(direccion == "Derecha")
        {
            golesIzquierda++;
            contadorIzquierda.text = golesIzquierda.ToString();
            //reinicio la bola
            GetComponent<Rigidbody2D>().velocity = Vector2.right * velocidad;
            if(golesIzquierda == 5)
            {
                SceneManager.LoadScene("Inicio");
            }
        }

        else if(direccion == "Izquierda")
        {
            golesDerecha++;
            contadorDerecha.text = golesDerecha.ToString();
            //reinicio la bola
            GetComponent<Rigidbody2D>().velocity = Vector2.left * velocidad;
            if(golesDerecha == 5)
            {
                SceneManager.LoadScene("Inicio");
            }
        }

        velocidad = velocidad + golesDerecha + golesIzquierda;
        fuenteDeAudio.clip = audioGol;
        fuenteDeAudio.Play();
    }
}