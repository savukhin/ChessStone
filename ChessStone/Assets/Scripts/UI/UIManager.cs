using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text m_player1Name;
    [SerializeField] private Text m_player2Name;

    private static UIManager m_instance = null;
    
    void Awake() {
        if (m_instance == null) {
            m_instance = this;
        } else if(m_instance == this) {
            Destroy(gameObject);
            return;
        }
    }

    public static void ChangePlayerName(int number, string name) {
        if (number == 1)
            m_instance.m_player1Name.text = name;
        else
            m_instance.m_player2Name.text = name;
    }
}
