using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStep : MonoBehaviour
{
    public abstract List< KeyValuePair<int, int> > GetMoves(KeyValuePair<int, int> from, KeyValuePair<int, int> shape);
}
