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

        StatePackage result = null;
        if (!m_wasFirstStateRequest) {
            m_wasFirstStateRequest = true;

            StatePackage.FigureState[] figures1 = {
                new StatePackage.FigureState(0, 0, 0)};
                
            StatePackage.FigureState[] figures2 = {};

            result =  new StatePackage(
                new StatePackage.PlayerState("Player", PlayableClass.warrior, figures1),
                new StatePackage.PlayerState("Dummy", PlayableClass.warrior, figures2)
                );
        } else {
            result = GameManager.GetState();
        }
        
        receiveStateEvent.Invoke(result);
    }
}
