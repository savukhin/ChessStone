using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FigureType : int {
    king=0,
    queen=1,
    diagonal=2,
    mobile=3,
    flank=4,
    pawn=5,
    other=6
}

public class Figure : MonoBehaviour
{
    [SerializeField] private int m_id;
    [SerializeField] private PlayableClass m_class;
    [SerializeField] private FigureType m_type;
    [SerializeField] private BaseStep m_step;

    public int GetId() { return m_id; }
    public FigureType GetFigureType() { return m_type; }
    public PlayableClass GetFigureClass() { return m_class; }
    public List< KeyValuePair<int, int> > GetMoves(KeyValuePair<int, int> from, KeyValuePair<int, int> shape) { return m_step.GetMoves(from, shape); }
}
