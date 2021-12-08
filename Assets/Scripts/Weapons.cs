using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewWeapon", menuName = "Items/Weapons", order = 1)]
public class Weapons : ScriptableObject
{
    // Start is called before the first frame update
    public Sprite sprite;
    public int damage;
    public string info;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
