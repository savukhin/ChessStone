using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private Material m_material;
    
    private void ResetColor() {
        m_material.color = colorDict[m_color];
    }

    // Start is called before the first frame update
    void Start()
    {
        m_material = GetComponent<Renderer>().material;
        ResetColor();
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo) && hitInfo.collider.gameObject == gameObject) {
            m_material.color = m_choosenColor;
            Debug.Log(gameObject);
        } else {
            ResetColor();
        }
    }
}
