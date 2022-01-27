using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class DummyClient : BaseClient
{
    private bool m_wasFirstStateRequest = false;
    public DummyClient() : base() {
        
    }

    private void EnemyTurn() {
        receiveActionEvent.Invoke(new EndTurnAction());
    }

    public override async void StartClient() {
        await Task.Yield();
    }

    public override async void SendStateRequest() {
        await Task.Yield();

        GameState result = null;
        if (!m_wasFirstStateRequest) {
            m_wasFirstStateRequest = true;
            result =  new GameState(
                new GameState.PlayerState("Player"),
                new GameState.PlayerState("Dummy")
                );

            Debug.Log("Dummy created players name1 = " + result.player1.name + " name2 = " + result.player2.name);
        } else {
            result = GameManager.GetState();
        }
        
        receiveStateEvent.Invoke(result);
    }
}
