using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonido : MonoBehaviour
{
    private AudioSource fuente{get{return GetComponent<AudioSource>();}}
    public AudioClip sonidoMoneda;
    public GameObject dona;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.tag == "Dona")
        {
            print("Te haz comido la dona");
            fuente.PlayOneShot(sonidoMoneda);
            c.gameObject.SetActive(false);
            //dona.gameObject.SetActive(true);
        }
    }
}
