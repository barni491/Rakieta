using UnityEngine;
using System.Collections;

public class Rakieta : MonoBehaviour
{

    public bool PrzelacznikKamery = false; 
    public Camera[] Kamery; // kamery
    public int licznik = 0; // zwiekszenie liczby o 1 powoduje zmiane kamery
    public GameObject PostacGracza; // polozenie gracza po wyjsciu z rakiety

    public Material skyBox;

    void Start()
    {
        // aktywowana kamera tylko gracza
        Kamery[0].enabled = false;
        Kamery[1].enabled = false;
    }
    // gdy gracz wejdzie 
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")

        {
            PrzelacznikKamery = true; // aktywowanie przelacznika gracza
            PostacGracza = col.gameObject; // przypisanie gracza do Collider

         //   RenderSettings.skybox = skyBox;

        }
    }
    // gdy gracz wyjdzie
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            PrzelacznikKamery = false;           
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) // przelaczanie kamer pod klawiszem E
        {
            if(PrzelacznikKamery)
            {
                licznik++;
                switch(licznik)
                {
                    case 1:
                        {
                            Kamery[0].enabled = true;
                            Kamery[1].enabled = false;
                            PostacGracza.SetActive(false);
                        }; break;
                    case 2:
                        {
                            Kamery[0].enabled = false;
                            Kamery[1].enabled = true;                      
                        }; break;
                    case 3:
                        {
                            Kamery[0].enabled = false;
                            Kamery[1].enabled = false;
                            PostacGracza.SetActive(true);
                            licznik = 0;
                        }; break;
                }
            }
        }
       
    }
}
