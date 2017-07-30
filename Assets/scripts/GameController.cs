using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    /*entity*/
    public GameObject Player;
    public GameObject Zombie;
    /*entity actual*/
    public GameObject Player_act;
    /*level*/
    public GameObject baldosa;
    /*para llevar la creacion del nivel*/
    /*string sera pos x y */
   public  Dictionary<String, GameObject> nivel = new Dictionary<String, GameObject>();
    public Dictionary<String, GameObject> salidas = new Dictionary<String, GameObject>();
    public Dictionary<int, String> salidas_pos = new Dictionary<int, String>();
    public int n_salidas;
    public int n_salidas_act;
    /*turnos*/
    public enum Estado { baldosa, player, zombie };
    public Estado miestado;

    void Start () {
        miestado = Estado.baldosa;
        /*PIEZA INICIO*/
        var inicio = new GameObject();
        inicio.name = "Inicio";
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++) {
                GameObject temp = Instantiate(baldosa, new Vector3(i * 4, -1.5f, j * 4),Quaternion.Euler(0, 0, 0));
                temp.GetComponent<baldosa_info>().pos = i.ToString() + "x" + j.ToString();
                if (i == 1 && j == 1)
                {
                    temp.GetComponent<baldosa_info>().tipo = "normal";
                    Player_act =  Instantiate(Player, new Vector3(i * 4, 0, j * 4), Quaternion.Euler(0, 0, 0));
                }
                else if (i == 1 || j == 1)
                {
                    temp.GetComponent<baldosa_info>().tipo = "salida";
                    
                    salidas.Add(i.ToString() + "x" + j.ToString(), temp);
                    salidas_pos.Add(n_salidas, i.ToString() + "x" + j.ToString());
                    n_salidas++;
                    n_salidas_act++;
                }
                else {
                    temp.GetComponent<baldosa_info>().tipo = "bloqueo";
                }
                nivel.Add(i.ToString() + "x" + j.ToString(), temp);
                temp.transform.parent = inicio.transform;


            }
        }
        /*PIEZA INICIO*/


    }

    // Update is called once per frame
    void Update () {
        pasoJuego(miestado);
	}

    private void pasoJuego(Estado miestado)
    {
        if (miestado.Equals(Estado.baldosa)) {
            generaPieza();
            this.miestado = Estado.player;
        }
    }
    public void generaPieza() {
        bool error = true;
        int n_piezas = 0;
        do {
            int pieza_int = UnityEngine.Random.Range(0, 1);

            
            if (pieza_int == 0)
            {


                /*pieza todas las salidas */
                bool no_encaja = true;


                for (int i = 0; no_encaja && i < n_salidas; i++) {
                    if (salidas_pos.ContainsKey(i)) {
                        /*encuentra la salida*/

                        Debug.Log(salidas_pos[i]);
                        no_encaja = comprobarEncaje(salidas_pos[i],0,i);
                    }
                    

                }
              
            }
            else if (pieza_int == 1)
            {
                /*pieza recta dos salidas */

            }
            else if (pieza_int == 2)
            {

            }
            else {

            }
            n_piezas++;
        } while (error && n_piezas < 7);
        if (error && n_piezas == 5) {
            Debug.Log("error al colocar pieza");
        } else {
            /*
            añadirSalidas()
            quitasSalidasAnt()
            */
        }
        if(Time.time > 1)
        miestado = Estado.player;

    }

    private bool comprobarEncaje(string salida , int pieza,int salida_num)
    {

        /*comprobar salidas*/
        String[] poses;

        Vector2 pos_pieza_n = new Vector2(0,0);
        poses = salida.Split('x');
        string salida_decidida = "";
        int x = int.Parse(poses[0]);
        int y = int.Parse(poses[1]);
        /*
         * / SALIDA DERECHA
         * */
        
        if (!nivel.ContainsKey(x.ToString() + "x" + (y + 1).ToString())   )
           {

               if (!nivel.ContainsKey((x+2).ToString() + "x" + (y + 2).ToString()) || nivel[(x+2).ToString() + "x" + (y + 2).ToString()].GetComponent<baldosa_info>().tipo == "bloqueo")
               {

                   if (!nivel.ContainsKey((x-2).ToString() + "x" + (y + 2).ToString()) || nivel[(x-2).ToString() + "x" + (y + 2).ToString()].GetComponent<baldosa_info>().tipo == "bloqueo")
                   {

                       salida_decidida = "dch";
              
                       pos_pieza_n = new Vector2(x - 1, y + 1);

                   }
               }


           }
           

        /*
         * / SALIDA IZQUIERDA
         * */

        /*
          else if (!nivel.ContainsKey(x.ToString() + "x" + (y - 1).ToString()))
         {

             if (!nivel.ContainsKey((x + 2).ToString() + "x" + (y + -2).ToString()) || nivel[(x + 2).ToString() + "x" + (y -2).ToString()].GetComponent<baldosa_info>().tipo == "bloqueo")
             {

                 if (!nivel.ContainsKey((x - 2).ToString() + "x" + (y -2).ToString()) || nivel[(x - 2).ToString() + "x" + (y -2).ToString()].GetComponent<baldosa_info>().tipo == "bloqueo")
                 {

                     salida_decidida = "izq";

                     pos_pieza_n = new Vector2(x - 1, y - 3);

                 }
             }


         } else */
         
        /*
        /*
         * / SALIDA ARRIBA
         * */
        /*
         if (!nivel.ContainsKey((x+1).ToString() + "x" + (y).ToString())) {
            Debug.Log("a");

            if (!nivel.ContainsKey((x+2).ToString() + "x" + (y+2).ToString()) || nivel[(x+2).ToString() + "x" + (y+2).ToString()].GetComponent<baldosa_info>().tipo == "bloqueo")
               {
                Debug.Log("b");
                if (!nivel.ContainsKey((x+2).ToString() + "x" + (y -2).ToString()) || nivel[(x+2).ToString() + "x" + (y - 2).ToString()].GetComponent<baldosa_info>().tipo == "bloqueo")
                   {
                    Debug.Log("c");
                    salida_decidida = "arr";

                       pos_pieza_n = new Vector2(x + 1, y - 1);

                  }
               }


        }
         */



        /*
         * / SALIDA ABAJO
         * */
         /*
        if (!nivel.ContainsKey((x - 1).ToString() + "x" + (y).ToString()))
        {
           

            if (!nivel.ContainsKey((x - 2).ToString() + "x" + (y + 2).ToString()) || nivel[(x - 2).ToString() + "x" + (y + 2).ToString()].GetComponent<baldosa_info>().tipo == "bloqueo")
            {
               

                if (!nivel.ContainsKey((x - 2).ToString() + "x" + (y - 2).ToString()) || nivel[(x - 2).ToString() + "x" + (y - 2).ToString()].GetComponent<baldosa_info>().tipo == "bloqueo")
                {
                  
                    salida_decidida = "abj";

                    pos_pieza_n = new Vector2(x - 3, y - 1);

                }
            }


        }


        */



        if (salida_decidida != "")
        {


            var inicio = new GameObject();
            inicio.name = "pieza";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    GameObject temp = Instantiate(baldosa, new Vector3(i * 4 + pos_pieza_n.x * 4, -1.5f, j * 4 + pos_pieza_n.y * 4), Quaternion.Euler(0, 0, 0));
                    if (i == 1 && j == 1)
                    {
                        temp.GetComponent<baldosa_info>().tipo = "normal";

                    }
                    else if (i == 1 || j == 1)
                    {
                        temp.GetComponent<baldosa_info>().tipo = "salida";

                    }
                    else
                    {
                        temp.GetComponent<baldosa_info>().tipo = "bloqueo";
                    }
                  
                    if (!salidas.ContainsKey((i + pos_pieza_n.x+1).ToString() + "x" + (j + pos_pieza_n.y+2).ToString()))
                    {
                        n_salidas++;
                      
                        salidas.Add((i + pos_pieza_n.x + 1).ToString() + "x" + (j + pos_pieza_n.y + 2).ToString(), temp);
                        salidas_pos.Add(n_salidas, (i + pos_pieza_n.x + 1).ToString() + "x" + (j + pos_pieza_n.y + 2).ToString());
                        n_salidas_act++;
                        
                       
                    }
                    else {
                        salidas.Remove(salida);
                        salidas_pos.Remove(salida_num);
                        n_salidas_act--;
                    }
                    //this.GetComponent<baldosa_info>().pos = (i + pos_pieza_n.x).ToString() + "x" + (j + pos_pieza_n.y).ToString();

                    temp.transform.parent = inicio.transform;
                   // Debug.Log(n_salidas_act);

                }
            }
            



            return false;
        }

        
        else {
          //  Debug.Log("salida no decidida");
            return  true;
        }
    }

    void pasoJuego() {
        /*
         * colocaBaldosa(),
         *          buscar_sitios_libre(),
         *          do{
         *              cogerpiezaRandom()
         *              comprobarEncaje()
         *          }while(not encaja);
         *          if(noerror){
         *              añadirSalidas()
         *              quitasSalidasAnt()
         *          }
         *          
         *          
         * MovimientoJugador(Jugador jugador)
         * MovimienoZombie(Zombies[])
         * 
         * 
         * 
         * 
         */
    }

    void OnGUI()
    {

        GUI.Label(new Rect(10, 10, 100, 20), n_salidas.ToString());
    }
}
