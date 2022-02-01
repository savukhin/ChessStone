using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public enum StepDirection {
    MainDiagonalUpward=0,
    MainDiagonalDownward=1,
    SecondaryDiagonalUpward=3,
    SecondaryDiagonalDownward=4,
    Upward=5,
    Downward=6,
    Leftward=7,
    Rightward=8,
}

public class DirectionalStep : BaseStep
{
    public StepDirection direction = StepDirection.MainDiagonalUpward;
    public bool isEndless = true;
    public int range = 1;

    public override List<KeyValuePair<int, int>> GetMoves(KeyValuePair<int, int> from, KeyValuePair<int, int> shape) {
        switch (direction)
        {
            case StepDirection.MainDiagonalUpward:
                return Enumerable.Range(1, Mathf.Min(shape.Key - from.Key, from.Value - 1))
                    .Select(item => new KeyValuePair<int, int>(from.Key + item, from.Value + item))
                    .OfType<KeyValuePair<int, int>>().ToList();

            case StepDirection.MainDiagonalDownward:
                return Enumerable.Range(1, Mathf.Min(from.Key - 1, shape.Value - from.Value))
                    .Select(item => new KeyValuePair<int, int>(from.Key - item, from.Value - item))
                    .OfType<KeyValuePair<int, int>>().ToList();
            
            case StepDirection.SecondaryDiagonalUpward:
                return Enumerable.Range(1, Mathf.Min(shape.Key - from.Key, shape.Value - from.Value))
                    .Select(item => new KeyValuePair<int, int>(from.Key + item, from.Value + item))
                    .OfType<KeyValuePair<int, int>>().ToList();

            case StepDirection.SecondaryDiagonalDownward:
                return Enumerable.Range(1, Mathf.Min(from.Key - 1, from.Value - 1))
                    .Select(item => new KeyValuePair<int, int>(from.Key - item, from.Value - item))
                    .OfType<KeyValuePair<int, int>>().ToList();

            case StepDirection.Upward:
                return Enumerable.Range(1, shape.Key - from.Key)
                    .Select(item => new KeyValuePair<int, int>(from.Key + item, from.Value))
                    .OfType<KeyValuePair<int, int>>().ToList();

            case StepDirection.Downward:
                return Enumerable.Range(1, from.Key - 1)
                    .Select(item => new KeyValuePair<int, int>(from.Key - item, from.Value))
                    .OfType<KeyValuePair<int, int>>().ToList();

            case StepDirection.Leftward:
                return Enumerable.Range(1, shape.Value - from.Value)
                    .Select(item => new KeyValuePair<int, int>(from.Key, from.Value + item))
                    .OfType<KeyValuePair<int, int>>().ToList();
            case StepDirection.Rightward:
                return Enumerable.Range(1, from.Value - 1)
                    .Select(item => new KeyValuePair<int, int>(from.Key, from.Value - item))
                    .OfType<KeyValuePair<int, int>>().ToList();
            default:
                return new List<KeyValuePair<int, int>>();
        }
    }
}


[CustomEditor(typeof(DirectionalStep))]
public class DirectionalStepEditor : Editor
{
    SerializedProperty isEndless;
    
    private void OnEnable(){
        isEndless = serializedObject.FindProperty("isEndless");
    }

    public override void OnInspectorGUI()
    {
        var myScript = target as DirectionalStep;

        myScript.direction = (StepDirection)EditorGUILayout.EnumPopup("Direction:", myScript.direction);        
        myScript.isEndless = EditorGUILayout.Toggle("Is Endles:", myScript.isEndless);
        
        if(!myScript.isEndless)
            myScript.range = EditorGUILayout.IntSlider("Range field:", myScript.range , 1 , 8);
    }
}