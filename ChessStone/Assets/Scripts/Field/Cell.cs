using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cell : MonoBehaviour
{
    public enum CellColor {
        white,
        black
    }

    public static readonly Dictionary<CellColor, Color> colorDict = new Dictionary<CellColor, Color> 
    {
        { CellColor.white , Color.white },
        { CellColor.black , Color.black }
    };

    [SerializeField] private CellColor m_color = CellColor.white;
    [SerializeField] private Color m_choosenColor = Color.blue;
    [SerializeField] private Color m_moveColor = Color.green;
    [SerializeField] private Color m_attackColor = Color.red;

    public int row=-1;
    public int col=-1;
    [System.NonSerialized] public UnityEvent<int, int> clickedEvent = new UnityEvent<int, int>();

    private Material m_material;
    private Color m_currentColor;
    private bool m_active = false;
    private Collider m_collider;

    public Vector3 GetChipPosition() {
        //chip.transform.position += Vector3.up * chip.GetComponent<Collider>().bounds.size.y / 2;
        return transform.position + Vector3.up * m_collider.bounds.size.y / 2;
    }
    
    public void ResetColor() {
        m_material.color = colorDict[m_color];
        m_currentColor = m_material.color;
        m_active = false;
    }

    public void MakePossibleToMove() {
        // Debug.Log("Change (to move) color to cell (" + row + ", " + col + ")");
        m_material.color = m_moveColor;
        m_currentColor = m_moveColor;
        m_active = true;
    }

    public void Choose() {
        m_currentColor = m_choosenColor;
        m_active = true;
    }

    // Start is called before the first frame update
    void Start() {
        m_collider = GetComponent<Collider>();
        m_material = GetComponent<Renderer>().material;
        ResetColor();
    }

    // Update is called once per frame
    void Update() {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo) && hitInfo.collider.gameObject == gameObject) {
            if (Input.GetMouseButtonDown(0))
                clickedEvent.Invoke(row, col);
            m_material.color = m_choosenColor;
        } else {
            //ResetColor();
            m_material.color = m_currentColor;
        }
    }
}
