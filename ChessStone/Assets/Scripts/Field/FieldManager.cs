using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    [System.Serializable]
    class Row {
        public Cell[] m_cells;
    }
    [SerializeField] private Row[] m_rows;

    public static FieldManager m_instance = null;
    private KeyValuePair<int, int> shape;
    private List<Chip> m_chips = new List<Chip>();

    void Awake() {
        if (m_instance == null) {
            m_instance = this;
        } else if(m_instance == this) {
            Destroy(gameObject);
            return;
        }

        shape = new KeyValuePair<int, int>(m_rows.Length, m_rows[0].m_cells.Length);
        for (int i = 0; i < shape.Key; i++) {
            for (int j = 0; j < shape.Value; j++) {
                m_rows[i].m_cells[j].row = i + 1;
                m_rows[i].m_cells[j].col = j + 1;
            }
        }
    }

    int GetRows() { return shape.Key; }
    int GetColumns() { return shape.Value; }

    public void ClearField() {
        m_chips.Clear();
    }

    public void PlaceFigure(Figure prefab, int row, int col) {
        var chipPrefab = FigureManager.m_instance.GetChip(prefab.GetFigureClass(), prefab.GetFigureType());
        var cell = m_rows[row].m_cells[col];
        var collider = cell.GetComponent<Collider>();

        var position = cell.transform.position + Vector3.up * collider.bounds.size.y / 2;

        var chip = Instantiate( 
            chipPrefab,
            position,
            Quaternion.identity
            );

        chip.transform.position += Vector3.up * chip.GetComponent<Collider>().bounds.size.y / 2;

        chip.SetFigure(Instantiate(prefab));

        m_chips.Add(chip);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
