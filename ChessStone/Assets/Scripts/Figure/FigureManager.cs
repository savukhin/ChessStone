using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FigureManager : MonoBehaviour
{
    [System.Serializable]
    private class ChipContainer {

        [System.Serializable]
        public class Element {
            public FigureType type;
            public Chip chip;
        }
        public PlayableClass m_class;
        public Element[] types;
    }

    [SerializeField] private Figure[] m_figures;
    [SerializeField] private ChipContainer[] m_chips;
    public Dictionary<PlayableClass, Dictionary<FigureType, Chip> > chipPrefabs 
        = new Dictionary<PlayableClass, Dictionary<FigureType, Chip> >();

    public static Dictionary<int, Figure> figurePrefabs = new Dictionary<int, Figure>();
    
    public static FigureManager m_instance = null;

    public void FormChipPrefabs() {
            foreach (var container in m_chips)
            {
                var dict = new Dictionary<FigureType, Chip>();
                foreach (var item in container.types)
                {
                    dict[item.type] = item.chip;
                }

                chipPrefabs[container.m_class] = dict;
            }
        }
    
    void Awake() {
        if (m_instance == null) {
            m_instance = this;
        } else if(m_instance == this) {
            Destroy(gameObject);
            return;
        }

        FormChipPrefabs();

        foreach (var figure in m_figures)
        {
            figurePrefabs[figure.GetId()] = figure;
        }
    }

    public Chip GetChip(PlayableClass playerClass, FigureType type) {
        return chipPrefabs[playerClass][type];
    }
}
