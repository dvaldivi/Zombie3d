using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baldosa_info : MonoBehaviour {
    public string tipo;
    public Material[] materials;
    public float changeInterval = 0.33F;
    public Renderer rend;
    public string pos;
    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        if (tipo.Equals("bloqueo"))
        {
            rend.sharedMaterial = materials[2];
        }
        else if (tipo.Equals("salida"))
        {
            rend.sharedMaterial = materials[0];

        }
        else if (tipo.Equals("normal"))
        {
            rend.sharedMaterial = materials[1];
        }
    }
	
	// Update is called once per frame
	void Update () {
      
        
      
    }
}
