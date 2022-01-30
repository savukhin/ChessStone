using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePackage {
    public class FigureState {
        public int id;
        public int row;
        public int col;

        public FigureState(int id=0, int row=0, int col=0) {
            this.id = id;
            this.row = row;
            this.col = col;
        }

        #region Operator overloading

        public static bool operator ==(FigureState a, FigureState b) => (a.id == b.id);

        public static bool operator !=(FigureState a, FigureState b) => !(a == b);

        public override bool Equals(System.Object obj) {
            if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else {
                FigureState p = obj as FigureState;
                
                return id == p.id;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }

    public class PlayerState {
        public string name;
        public FigureState[] figures;
        public PlayableClass m_class;

        public PlayerState(string name="Player", PlayableClass _class=PlayableClass.warrior, FigureState[] figures=null) {
            this.name = name;
            this.figures = figures;
            m_class = _class;
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
                
                if (figures.Length != p.figures.Length)
                    return false;
                for (int i = 0; i < figures.Length; i++) {
                    if (figures[i] != p.figures[i])
                        return false;
                }
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

    public StatePackage(PlayerState p1, PlayerState p2) {
        player1 = p1;
        player2 = p2;
    }

    public StatePackage() {
        player1 = new PlayerState();
        player2 = new PlayerState();
    }

    #region Operator overloading

   public static bool operator ==(StatePackage a, StatePackage b) {
        return a.Equals(b);
   }

   public static bool operator !=(StatePackage a, StatePackage b) => !(a == b);

    public override bool Equals(System.Object obj) {
        if ((obj == null) || ! this.GetType().Equals(obj.GetType())) {
            return false;
        } else {
            StatePackage p = obj as StatePackage;
            return (player1 == p.player1) && (player2 == p.player2);
        }
    }

    public override int GetHashCode() {
        return base.GetHashCode();
    }

    #endregion
}