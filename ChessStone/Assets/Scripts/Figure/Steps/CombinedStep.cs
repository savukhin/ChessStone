using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombinedStep : BaseStep
{
    [SerializeField] private BaseStep[] m_steps;

    public override List<KeyValuePair<int, int>> GetMoves(KeyValuePair<int, int> from, KeyValuePair<int, int> shape)
    {
        var result = new List<KeyValuePair<int, int>>();
        foreach (var step in m_steps) {
            var middle = step.GetMoves(from, shape);
            foreach (var item in middle) {
                result.Add(item);
            }
        }
        return result;
    }
}
