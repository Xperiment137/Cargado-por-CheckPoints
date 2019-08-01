using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading;
public class Menu : MonoBehaviour
{
    public Button boton, boton1, boton2;
    public GameObject aux,aux2,aux3,aux4,aux5;
    private bool activo, activo2, activo4;
    private string mensaje;
    private Gusrdadoch car;
    void Awake()
    {
        Cursor.visible = true;
        Time.timeScale = 1;
        aux = GameObject.Find("Menudeescena");
        aux2 = GameObject.Find("OpcionesPanel");
        aux3 = GameObject.Find("CargadorPanel");
        aux4 =GameObject.Find("CheckPoint");
        aux5 = GameObject.Find("PantallaCarga");
       
        car = aux4.GetComponent<Gusrdadoch>();
    }
    void Start()
    {
        aux.SetActive(false);
        aux2.SetActive(false);
        aux3.SetActive(false);
        aux5.SetActive(false);
        activo4 = false;
    }


    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (aux.activeSelf)
            {
                ContinueGame();
}
            else
            {

                PauseGame();
                boton.onClick.AddListener(() => Reiniciar(false));
                boton1.onClick.AddListener(() => Cargar());
             
               

            }

            if (aux2.activeSelf)
            {

                aux2.SetActive(false);


            }
            if (aux3.activeSelf)
            {

                aux3.SetActive(false);


            }

        }
            }


    private void Reiniciar(bool seguro)
    {
        if (seguro)
        {
             string escena = PlayerPrefs.GetString("Escena");
            SceneManager.LoadScene(escena, LoadSceneMode.Single);
        }
        else
        {
            activo = true;
        }

    }
    private void Cargar()
    {
        aux3.SetActive(true);
        activo2 = true;
        if (aux4)
        {
            aux3.SetActive(false);
            aux5.SetActive(true);
            mensaje = "Cargando";
            activo4 = true;

        }
            if(car.CargarJugador())
            {
               
                Thread.Sleep(25);
                mensaje = "Listo";
                activo4 =false;
                aux5.SetActive(false);
                aux3.SetActive(true);

            }
        
           
           
        }
        
    


    void OnGUI()
    {

        if (activo)
        {

            GUI.skin.label.alignment = TextAnchor.UpperCenter;
            GUI.Label(new Rect(Screen.width / 2 - 300, 50, 500, 50), "¿Estas seguro...?");
            using (var areaScope = new GUILayout.AreaScope(new Rect(Screen.width / 2 - 300, 100, 500, 50)))
            {


                if (GUILayout.Button("Si"))
                {
                    Reiniciar(true);

                }
                if (GUILayout.Button("No"))
                {
                    aux3.SetActive(false);
                    activo2 = false;
                }
            }
        }


        if (activo2)

        {


            GUI.skin.label.alignment = TextAnchor.UpperCenter;
            GUI.Label(new Rect(Screen.width / 2 - 400, 50, 500, 50), "¿Seguro?");

            using (var areaScope = new GUILayout.AreaScope(new Rect(Screen.width / 2 - 300, 100, 500, 50)))
            {

                if (GUILayout.Button("Si"))
                {
                    activo2 = false;
                    

                }
                if (GUILayout.Button("No"))
                {
                    activo2 = false;

                }
            }

                if (activo4)
                {
                    GUI.Label(new Rect(Screen.width / 2 - 300, 50, 500, 50), mensaje);
                  

                }
            }
    }
    private void PauseGame()
    {


        aux.SetActive(true);

    }

    private void ContinueGame()
    {
        Cursor.visible = false;
        aux.SetActive(false);
        Time.timeScale = 1;



    }
}
