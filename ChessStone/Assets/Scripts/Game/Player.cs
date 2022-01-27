using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayableClass {
    warrior,
    mage,
    necromance
}

public class Player : MonoBehaviour
{
    private PlayableClass m_class = PlayableClass.warrior;
    private string m_playerName;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }
}
