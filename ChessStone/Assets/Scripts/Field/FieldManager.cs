using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    [System.Serializable]
    class Row {
        public Cell[] m_cells;
        [System.NonSerialized] public Chip[] m_chips;
    }
    [SerializeField] private Row[] m_rows;

    public static FieldManager m_instance = null;
    private KeyValuePair<int, int> shape;
    private List<Chip> m_chips = new List<Chip>();

    private Cell m_choosenCell = null;
    private HashSet<Cell> m_ableToMoveCells = new HashSet<Cell>();

    void Awake() {
        if (m_instance == null) {
            m_instance = this;
        } else if(m_instance == this) {
            Destroy(gameObject);
            return;
        }

        shape = new KeyValuePair<int, int>(m_rows.Length, m_rows[0].m_cells.Length);
        for (int i = 0; i < shape.Key; i++) {
            m_rows[i].m_chips = new Chip[shape.Value];
            for (int j = 0; j < shape.Value; j++) {
                m_rows[i].m_cells[j].row = i;
                m_rows[i].m_cells[j].col = j;
                m_rows[i].m_cells[j].clickedEvent.AddListener(CellClickedCallback);

                //m_rows[i].m_chips[j] = null;
            }
        }
    }

    #region Getters

    int GetRows() { return shape.Key; }
    int GetColumns() { return shape.Value; }
    Cell GetCell(int row, int col) { return m_rows[row].m_cells[col]; }
    Chip GetChip(int row, int col) { return m_rows[row].m_chips[col]; }
    
    #endregion

    public void ClearField() {
        m_chips.Clear();
        foreach (var row in m_rows) {
            for (int i = 0; i < row.m_chips.Length; i++) {
                row.m_chips[i] = null;
            }
        }
    }

    public void PlaceFigure(Figure prefab, int row, int col) {
        var chipPrefab = FigureManager.m_instance.GetChip(prefab.GetFigureClass(), prefab.GetFigureType());
        var cell = m_rows[row].m_cells[col];
        var collider = cell.GetComponent<Collider>();

        //var position = cell.transform.position + Vector3.up * collider.bounds.size.y / 2;

        var chip = Instantiate( 
            chipPrefab);
            // position,
            // Quaternion.identity
            // );

        chip.transform.position = cell.GetChipPosition() + Vector3.up * chip.GetComponent<Collider>().bounds.size.y / 2;
        // chip.transform.position = cell.GetChipPoint();

        chip.SetFigure(Instantiate(prefab));
        chip.SetCell(cell);

        m_chips.Add(chip);
        m_rows[row].m_chips[col] = chip;
    }

    void ResetCellColors() {
        foreach (var row in m_rows) {
            foreach (var cell in row.m_cells) {
                cell.ResetColor();
            }
        }
    }

    void ResetChoosenCell() {
        if (m_choosenCell == null)
            return;
        ResetCellColors();
        m_choosenCell = null;
        m_ableToMoveCells.Clear();
    }

    void ChooseChip(int row, int col) {
        var chip = m_rows[row].m_chips[col];
        if (chip == null) {
            ResetChoosenCell();
            return;
        }

        if (m_choosenCell != null)
            ResetCellColors();

        var figure = chip.GetFigure();
        var moves = figure.GetMoves(new KeyValuePair<int, int>(row, col), shape);
        foreach (var move in moves) {
            //m_rows[move.Key].m_cells[move.Value].MakePossibleToMove();
            var cell = GetCell(move.Key, move.Value);
            cell.MakePossibleToMove();
            m_ableToMoveCells.Add(cell);
        }

        m_choosenCell = GetCell(row, col);// m_rows[row].m_cells[col];
        m_choosenCell.Choose();
    }

    void MoveChip(Chip chip, Cell destination) {
        var displacement = destination.GetChipPosition() - chip.transform.position;
        chip.MoveTo(new Vector2(displacement.x, displacement.z));
        var source = chip.GetCell();
        m_rows[source.row].m_chips[source.col] = null;

        m_rows[destination.row].m_chips[destination.col] = chip;
        chip.SetCell(destination);
    }

    void CellClickedCallback(int row, int col) {
        var cell = GetCell(row, col);
        if (m_choosenCell != null && m_ableToMoveCells.Contains(cell) && GameManager.m_instance.IsAbleToMove()) {
            var chip = GetChip(m_choosenCell.row, m_choosenCell.col);
            MoveChip(chip, cell);
            ResetChoosenCell();
            GameManager.m_instance.PlayerMove();
        } else {
            ChooseChip(row, col);
        }
    }
}
