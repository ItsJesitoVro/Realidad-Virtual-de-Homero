using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Color defaultcolor;
    public Color newcolor;
    public Renderer render;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();
        render.material.color = newcolor;
    }

    // Update is called once per frame
    void Update()
    {
        render = GetComponent<Renderer>();
        render.material.color = defaultcolor;
    }
}
