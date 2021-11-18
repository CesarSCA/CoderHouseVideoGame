using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    
    [SerializeField] private Animator animEnemy;

    [SerializeField] private float enemySpeed = 1.7f;
    private Rigidbody rbEnemy;

    enum Mode {Watcher = 1, Follower}
    [SerializeField] private Mode enemyMode;

    enum Range {MidRange = 1, FullRange}
    [SerializeField] private Range enemyRange;

    private float range = 4f;
    private Vector3 playerDirection;
    private Vector3 posInPlayer;
    float rbDrag = 6f;
        [SerializeField] private float MaxDistance = 2f;
    [SerializeField] private LayerMask layerToCollide;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Jugador");       //Busca al player con el nombre de el game object "Jugador"
        posInPlayer = player.GetComponent<Transform>().position;
        rbEnemy = GetComponent<Rigidbody>();

        animEnemy.SetBool("isWalking",false);
    }

    // Update is called once per frame
    void Update()
    {
        ControlDrag();
    }
    
    private void FixedUpdate() {
        playerDirection = GetplayerDirection();

        switch(enemyMode)
        {
            case Mode.Watcher:
            animEnemy.SetBool("isWalking",false);                                              // Comienza la Animacion de Idle
            LookAtPlayer();
                break;
            case Mode.Follower:
            animEnemy.SetBool("isWalking",true);                                              // Comienza la Animacion de Caminar
            LookAtPlayer();
            ChasePlayer();
                break;
        }
        Raycast();

    }

    private void Raycast()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, MaxDistance, layerToCollide))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Veo al jugador");
        }
    }
    void ControlDrag()
    {
        rbEnemy.drag = rbDrag;
    }
    private void LookAtPlayer()                                                        //Funcion para Solo Mirar al PLayer
    {
        rbEnemy.rotation = Quaternion.LookRotation(new Vector3(playerDirection.x, 0 ,playerDirection.z));
    }

    private void ChasePlayer(){                                                    // El enemigo seguira al player en un rango definido desde Inspector
    
    if (enemyRange == Range.FullRange)
    {   
        playerDirection = GetplayerDirection();
        rbEnemy.AddForce(playerDirection.normalized * enemySpeed * 4.2f, ForceMode.Acceleration);
        // Reemplaza a ChasePlayer  // Movimiento Con Seguimiento del Player con velocidad variable
    } 
    else
    {
        if (Vector3.Distance(transform.position, player.transform.position) >= range)
            {
            playerDirection = GetplayerDirection();
            rbEnemy.AddForce(playerDirection.normalized * enemySpeed * 4.2f, ForceMode.Acceleration);               // Movimiento Con Seguimiento del Player con velocidad variable
            }
    }

    }

    private Vector3 GetplayerDirection(){
        return player.transform.position - transform.position;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Player"))
        {
            other.transform.position = posInPlayer;
            Debug.Log("anda");
        }
    }
}
