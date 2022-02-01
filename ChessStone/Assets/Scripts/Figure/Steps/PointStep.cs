using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PointStep : BaseStep
{
    [SerializeField] private Vector2Int m_offset;

    public override List<KeyValuePair<int, int>> GetMoves(KeyValuePair<int, int> from, KeyValuePair<int, int> shape) {
        var result = new KeyValuePair<int, int>(from.Key + m_offset.x, from.Value + m_offset.y);
        if (result.Key <= 0 || result.Value <= 0 || result.Key > shape.Key || result.Value > shape.Value)
            return new List<KeyValuePair<int, int>>();
        
        return new KeyValuePair<int, int>[]{result}.ToList();
    }    
}
