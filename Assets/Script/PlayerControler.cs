using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    private new Rigidbody rigidbody;
    private AudioSource fuente{get{return GetComponent<AudioSource>();}}
    public AudioClip comer;
    public AudioClip vida;
    public AudioClip ganar;
    public AudioClip perder;
    public GameObject esfera;
    public Vector2 sensibility;
    private Transform camera;
    int contador;
    int contadorV;
    public Text puntuacion;
    public Text win;
    public Text vidaN;
    public Text lose;
    Rigidbody rb;
    bool isJump = false;
    bool floorDectected = false;
    public float jumpForce = 10.0f;

    public float speed = 10f;

    private void actualizarmarcador(){
        puntuacion.text = "Donas Comidas: " + contador + "/10";
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rigidbody = GetComponent<Rigidbody>();

        camera = transform.Find("Camera");
        Cursor.lockState = CursorLockMode.Locked;

        contadorV = 5;
        contador = 0;
        actualizarmarcador();
        vidaN.text = "Vidas: " + contadorV;

        win.gameObject.SetActive(false);
        lose.gameObject.SetActive(false);
        
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
        if (verC != 0){
            //camera.Rotate(Vector3.left * verC * sensibility.y);
            float angle = (camera.localEulerAngles.x - verC * sensibility.y + 360) % 360;
            if (angle > 180){
                angle -= 360;
            }
            angle = Mathf.Clamp(angle, -60, 55);
            camera.localEulerAngles = Vector3.right * angle;
        }
        Vector3 floor = transform.TransformDirection(Vector3.down);

        if(Physics.Raycast(transform.position, floor, 1.03f)){
            floorDectected = true;
            //print("Contacto con el suelo");
        } else {
            floorDectected = false;
            //print("No hay contacto con el suelo");
        }

        isJump = Input.GetButtonDown("Jump");

        if(isJump && floorDectected){
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }
    private void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.tag == "dona")
        {
            //print("Te haz comido la dona");
            fuente.PlayOneShot(comer);
            c.gameObject.SetActive(false);
            contador = contador + 1;
            actualizarmarcador();
            if (contador >= 10){
                win.gameObject.SetActive(true);
                fuente.PlayOneShot(ganar);
                Time.timeScale = 0f;
            }
        }
        if(c.gameObject.tag == "enemigo")
        {
            //print("Daño resivido");
            fuente.PlayOneShot(vida);
            contadorV = contadorV - 1;
            vidaN.text = "Vidas: " + contadorV;
            if (contadorV <= 0){
                lose.gameObject.SetActive(true);
                fuente.PlayOneShot(perder);
                Time.timeScale = 0f;
            }
        }
    }
}
