using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    private new Rigidbody rigidbody;
    private AudioSource fuente{get{return GetComponent<AudioSource>();}}
    public AudioClip comer;
    public AudioClip ganar;
    public GameObject esfera;
    public Vector2 sensibility;
    private Transform camera;
    int contador;
    public Text puntuacion;
    public Text win;

    public float speed = 10f;

    private void actualizarmarcador(){
        puntuacion.text = "Donas Comidas: " + contador;
    }
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        camera = transform.Find("Camera");
        Cursor.lockState = CursorLockMode.Locked;

        contador = 0;
        actualizarmarcador();

        win.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        if (hor != 0.0f || ver != 0.0f){
            Vector3 dir = transform.forward * ver + transform.right * hor;

            rigidbody.MovePosition(transform.position + dir * speed * Time.deltaTime);
        }

        float horC = Input.GetAxisRaw("Mouse X");
        float verC = Input.GetAxisRaw("Mouse Y");

        if (horC != 0){
            transform.Rotate(Vector3.up * horC * sensibility.x);
        }
        /*if (verC != 0){
            //camera.Rotate(Vector3.left * verC * sensibility.y);
            float angle = (camera.localEulerAngles.x - verC * sensibility.y + 360) % 360;
            if (angle > 180){
                angle -= 360;
            }
            angle = Mathf.Clamp(angle, 25, 55);
            camera.localEulerAngles = Vector3.right * angle;
        }*/
    }
    private void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.tag == "dona")
        {
            print("Te haz comido la dona");
            fuente.PlayOneShot(comer);
            c.gameObject.SetActive(false);
            //dona.gameObject.SetActive(true);
            contador = contador + 1;
            actualizarmarcador();
            if (contador >= 10){
                win.gameObject.SetActive(true);
                fuente.PlayOneShot(ganar);
            }
        }
    }
}
