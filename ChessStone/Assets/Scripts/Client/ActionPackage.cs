using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionPackage {
    public enum ActionType {
        CastSpell,
        CastFigure,
        MoveFigure,

        Leave
    }

    //public ActionType type = 
    public abstract void Act(GameManager game);
}

public class CastSpellAction : ActionPackage {
    public override void Act(GameManager game)
    {
        //throw new System.NotImplementedException();
    }
}

public class EndTurnAction : ActionPackage {
    public override void Act(GameManager game)
    {
        //throw new System.NotImplementedException();
        game.EndTurn();
    }
}
