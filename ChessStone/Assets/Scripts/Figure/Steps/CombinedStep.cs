using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombinedStep : BaseStep
{
    [SerializeField] private BaseStep[] m_steps;

    public override List<KeyValuePair<int, int>> GetMoves(KeyValuePair<int, int> from, KeyValuePair<int, int> shape)
    {
        return m_steps
            .Select(step => step.GetMoves(from, shape))
            .OfType<KeyValuePair<int, int>>().ToList();
    }
}
