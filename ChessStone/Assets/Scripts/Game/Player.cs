using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayableClass {
    warrior=0,
    mage=1,
    necromancer=2
}

public class Player : MonoBehaviour
{
    public PlayableClass m_class = PlayableClass.warrior;
    public string m_playerName;
    public int maxMoveCount = 1;
    public int currentMoveCount = 1;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }
}
