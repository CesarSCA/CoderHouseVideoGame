using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;
    public bool dontDestroyOnLoad;
    public string[] logros;
    public GameObject[] objetos;

    private void Awake()
     {
        if(instance == null)
        {
            instance = this;

            if(dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
            else {
                Destroy(gameObject);
            }
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
