using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
public class Gusrdadoch : MonoBehaviour
{
    public GameObject aux2;
    private Vidamanager vid;
    private float x;
    private float y;
    private float z;
   public string escena;
    void Awake()
    {
       
       ;
        aux2 = GameObject.Find("Jugador");
        vid = aux2.GetComponent<Vidamanager>();
        
    }
    void Update()
    {
      

    }

        public static void SaveInventario()
    {

        BinaryFormatter bf = new BinaryFormatter();
        Stream filein = new FileStream(Application.persistentDataPath + "Guardado", FileMode.Create);
        bf.Serialize(filein, ClaseInventario.Invs);
        filein.Close();


    }
    public static void LoadInventario()
    {
        if (File.Exists(Application.persistentDataPath + "Guardado"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "Guardado" ,FileMode.Open);
            ClaseInventario.Invs = (List<ClaseInventario>)bf.Deserialize(file);
            file.Close();
        }
    }
    public void GuardarEscena()
    {
        Scene scene = SceneManager.GetActiveScene();
        PlayerPrefs.SetString("Escena",scene.name );
    }
   public  IEnumerator LoadYourAsyncScene()
    {
        escena = PlayerPrefs.GetString("Escena");
        if(escena==SceneManager.GetActiveScene().ToString())
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(escena, LoadSceneMode.Single);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            CargarJugador();
            SceneManager.MoveGameObjectToScene(aux2, SceneManager.GetSceneByName(escena));
           
        }
        else
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(escena, LoadSceneMode.Additive);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            CargarJugador();
            SceneManager.MoveGameObjectToScene(aux2, SceneManager.GetSceneByName(escena));
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        }
      
    }
        public bool CargarJugador()
    {
        x = PlayerPrefs.GetFloat("x");
        y = PlayerPrefs.GetFloat("y");
        z = PlayerPrefs.GetFloat("z");
        Vector3 posVec = new Vector3(x, y, z);
        aux2.transform.position = posVec;
        vid.Vida= PlayerPrefs.GetInt("Vida");
        return true;
    }
        public void GuardarJugador()
    {
        PlayerPrefs.SetInt("Vida", vid.Vida);
        PlayerPrefs.SetFloat("x",aux2.transform.position.x);
        PlayerPrefs.SetFloat("y", aux2.transform.position.y);
        PlayerPrefs.SetFloat("z", aux2.transform.position.z);
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {

            GuardarEscena();
            GuardarJugador();
          

        }



    }
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            
            Destroy(gameObject);
        }
    }
}