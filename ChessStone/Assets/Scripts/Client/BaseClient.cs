using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Threading;
using System.Threading.Tasks;

public abstract class GameAction {
    public enum ActionType {
        CastSpell,
        CastFigure,
        MoveFigure,

        Leave
    }

    //public ActionType type = 
    public abstract void Act(GameManager game);
}

public class CastSpellAction : GameAction {
    public override void Act(GameManager game)
    {
        //throw new System.NotImplementedException();
    }
}

public class EndTurnAction : GameAction {
    public override void Act(GameManager game)
    {
        //throw new System.NotImplementedException();
        game.EndTurn();
    }
}

public class GameState {
    public class PlayerState {
        public string name;

        public PlayerState(string name="Player") {
            this.name = name;
        }

        #region Operator overloading

        public static bool operator ==(PlayerState a, PlayerState b) => (a.name == b.name);

        public static bool operator !=(PlayerState a, PlayerState b) => !(a == b);

        public override bool Equals(System.Object obj) {
            if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else {
                PlayerState p = obj as PlayerState;
                return name == p.name;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
    public PlayerState player1;
    public PlayerState player2;

    public GameState(PlayerState p1, PlayerState p2) {
        player1 = p1;
        player2 = p2;
    }

    public GameState() {
        player1 = new PlayerState();
        player2 = new PlayerState();
    }

    #region Operator overloading

   public static bool operator ==(GameState a, GameState b) {
        return a.Equals(b);
   }

   public static bool operator !=(GameState a, GameState b) => !(a == b);

    public override bool Equals(System.Object obj) {
        if ((obj == null) || ! this.GetType().Equals(obj.GetType())) {
            return false;
        } else {
            GameState p = obj as GameState;
            return (player1 == p.player1) && (player2 == p.player2);
        }
    }

    public override int GetHashCode() {
        return base.GetHashCode();
    }

    #endregion
}

public abstract class BaseClient
{
    public UnityEvent<GameAction> receiveActionEvent = new UnityEvent<GameAction>();
    public UnityEvent<GameState> receiveStateEvent = new UnityEvent<GameState>();

    public BaseClient() {

    }

    public virtual async void StartClient() {
        await Task.Yield();
    }

    public virtual async void SendStateRequest() {
        await Task.Yield();
    }
}
