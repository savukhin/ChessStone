using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Threading;
using System.Threading.Tasks;

public abstract class BaseClient
{
    public UnityEvent<ActionPackage> receiveActionEvent = new UnityEvent<ActionPackage>();
    public UnityEvent<StatePackage> receiveStateEvent = new UnityEvent<StatePackage>();

    public BaseClient() {

    }

    public virtual async void StartClient() {
        await Task.Yield();
    }

    public virtual async void SendStateRequest() {
        await Task.Yield();
    }
}
