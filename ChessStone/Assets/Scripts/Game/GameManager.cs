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

    public class Flags {
        public bool isMoving = false;
        public bool isCasting = false;
        public bool isPlacing = false;
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

    private Player m_player1 = new Player();
    private Player m_player2 = new Player();
    private Player m_currentPlayer = null;
    private BaseClient m_client = null;
    private StatePackage m_gameState = new StatePackage();
    public Flags flags = new Flags();

    #endregion

    public static GameManager m_instance = null;

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

    void ParseAction(ActionPackage action) {
        
    }

    void UpdateFigures(StatePackage state) {
        FieldManager.m_instance.ClearField();
        var figures = state.player1.figures;
        var field = FieldManager.m_instance;

        for (int i = 0; i < figures.Length; i++) {
            var figure = figures[i];

            field.PlaceFigure(
                FigureManager.figurePrefabs[figure.id],
                figure.row,
                figure.col
                );
        }
    }

    void ParseState(StatePackage state) {
        if (state == null || state == m_gameState)
            return;
        
        m_gameState = state;
        UIManager.ChangePlayerName(1, m_gameState.player1.name);
        UIManager.ChangePlayerName(2, m_gameState.player2.name);

        m_player1.m_playerName = m_gameState.player1.name;
        m_player2.m_playerName = m_gameState.player2.name;
        m_player1.m_class = m_gameState.player1.m_class;
        m_player2.m_class = m_gameState.player2.m_class;

        UpdateFigures(state);

        return;
    }

    public static StatePackage GetState() {
        return m_instance.m_gameState;
    }

    #endregion
    #region Getters

    public static GameManager GetInstance() {
        return m_instance;
    }

    public void StartTurn(Player player) {
        m_currentPlayer = player;
    }

    public void EndTurn() {

    }

    public void PlayerMove() {
        m_currentPlayer.currentMoveCount--;
    }

    public bool IsAbleToMove() {
        return m_currentPlayer.currentMoveCount != 0;
    }

    #endregion
    #region Unity callbacks

    // Start is called before the first frame update
    void Start() {
        m_client.receiveActionEvent.AddListener(ParseAction);
        m_client.receiveStateEvent.AddListener(ParseState);

        m_client.StartClient();

        m_client.SendStateRequest();

        StartTurn(m_player1);
    }

    // Update is called once per frame
    void Update() {
        
    }

    #endregion
}
