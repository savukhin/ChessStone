using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    #region Internal constructions

    class Flags {
        bool m_isMoving = false;
        bool m_isCasting = false;
        bool m_isPlacing = false;
    }
    private enum ClientType {
        Dummy,
        Network
    }

    #endregion
    #region Serialized Fields

    [SerializeField, FormerlySerializedAs("Client Type")] private ClientType m_clientType = ClientType.Dummy;
    #endregion

    #region Private variables

    private Player m_player1 = null;
    private Player m_player2 = null;
    private Player m_currentPlayer = null;
    //private Field m_field;
    private BaseClient m_client = null;
    private GameState m_gameState = new GameState();

    #endregion

    private static GameManager m_instance = null;

    void Awake() {
        if (m_instance == null) {
            m_instance = this;
        } else if(m_instance == this) {
            Destroy(gameObject);
            return;
        }

        switch (m_clientType)
        {
            case ClientType.Dummy:
                m_client = new DummyClient();
                break;
            case ClientType.Network:
                //m_client = new Client();
                break;
            default:
                break;
        }
    }

    #region Client interaction

    void ParseAction(GameAction action) {
        
    }

    void ParseState(GameState state) {
        if (state == null || state == m_gameState)
            return;
        
        m_gameState = state;
        UIManager.ChangePlayerName(1, m_gameState.player1.name);
        UIManager.ChangePlayerName(2, m_gameState.player2.name);

        return;
    }

    public static GameState GetState() {
        return m_instance.m_gameState;
    }

    #endregion
    #region Getters

    public static GameManager GetInstance() {
        return m_instance;
    }

    public void EndTurn() {

    }

    #endregion
    #region Unity callbacks

    // Start is called before the first frame update
    void Start() {
        m_client.receiveActionEvent.AddListener(ParseAction);
        m_client.receiveStateEvent.AddListener(ParseState);

        m_client.StartClient();

        m_client.SendStateRequest();
        m_client.SendStateRequest();
    }

    // Update is called once per frame
    void Update() {
        
    }

    #endregion
}
