using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float velocidadMovimiento = 2.3f;
    public bool hidden;
    [SerializeField] private float ejeX = 0f;
    [SerializeField] private GameObject tablet;
    [SerializeField] private bool tabletActiva = false;
    [SerializeField] private float stamina =  10;
    private Rigidbody rbPlayer;
    float ejeHorizontal;
    float ejeVertical;
    Vector3 moveDirection;
    float rbDrag = 6f;
    [SerializeField] private GameObject enemy;
    [SerializeField] private TextMeshProUGUI distanceInterface;
    [SerializeField] private TextMeshProUGUI staminaInterface;
    [SerializeField] private TextMeshProUGUI[] warningTexts;
    private float timer;



    // Start is called before the first frame update
    void Start()
    {   
        rbPlayer = GetComponent<Rigidbody>();
        rbPlayer.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {   
        TabletMostrar();
        Correr();
        Rotar();
        Inputs();
        ControlDrag();
        distanceInterface.text = "Distancia de ???: " + Mathf.Floor((GetEnemyDistance().magnitude)).ToString();
        staminaInterface.text = "Stamina: " + Mathf.Floor(stamina).ToString();
        if (Mathf.Floor((GetEnemyDistance().magnitude)) <= 5)
        {
            blinkingText(0);
        } else
        {
            warningTexts[0].enabled = false;
        }
        if (Mathf.Floor(stamina) == 0)
        {
            blinkingText(1);
        } else
        {
            warningTexts[1].enabled = false;
        }

    }
    void FixedUpdate()
    {
        Avanzar();
    }

    void Inputs()
    {
        ejeHorizontal = Input.GetAxisRaw("Horizontal");
        ejeVertical = Input.GetAxisRaw("Vertical");
        moveDirection = transform.forward * ejeVertical + transform.right * ejeHorizontal;
    }
    void ControlDrag()
    {
        rbPlayer.drag = rbDrag;
    }
    private void Avanzar()
    {
        rbPlayer.AddForce(moveDirection.normalized * velocidadMovimiento * 10, ForceMode.Acceleration);

    }

    void Rotar()
    {
        ejeX += Input.GetAxis("Mouse X");
        Quaternion angulo = Quaternion.Euler(0, ejeX, 0);
        transform.rotation = angulo;
    }

    void Correr()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            stamina -= 2.7f * Time.deltaTime;
            if(stamina <= 0)
            {
                velocidadMovimiento = 2f;
                stamina = 0;
                return;
            }
            velocidadMovimiento = 2.9f;
        }
        if(!Input.GetKey(KeyCode.LeftShift))
        {
            stamina += 2f * Time.deltaTime;
            if(stamina >= 10)
            {
                velocidadMovimiento = 2.5f;
                stamina = 10;
                return;
            }
            velocidadMovimiento = 2f;
        }
    }

    void TabletMostrar()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(tabletActiva == false)
            {
                tablet.SetActive(true);
                tabletActiva = true;
            } else if(tabletActiva == true)
            {
                tablet.SetActive(false);
                tabletActiva = false;
            }
        }
    }
    private Vector3 GetEnemyDistance()
    {
        return enemy.transform.position - transform.position;
    }
    private void blinkingText(int num)
    {
        timer = timer + Time.deltaTime;
        if (timer >= 0.1)
        {
            warningTexts[num].enabled = true;
        }
        if (timer >= 1)
        {
            warningTexts[num].enabled = false;
            timer = 0;
        }
    }

}
    