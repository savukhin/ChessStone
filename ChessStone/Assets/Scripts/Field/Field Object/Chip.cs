using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : MonoBehaviour
{
    private Figure m_figure;
    private Cell m_cell = null;

    public void SetFigure(Figure figure) {
        m_figure = figure;
    }
    
    public void SetCell(Cell cell) {
        m_cell = cell;
    }

    public Figure GetFigure() {
        return m_figure;
    }

    public Cell GetCell() {
        return m_cell;
    }

    IEnumerator MoveAnimation(Vector2 displacement, float maxHeight=1, float speed=10f) {
        // f(x) = -a * x (x - 2 sqrt(h));
        GameManager.m_instance.flags.isMoving = true;

        float x = 0.0f;
        float l = displacement.magnitude;
        var startPosition = transform.position;
        var startPositionXoZ = new Vector2(transform.position.x, transform.position.z);
        var direction = displacement.normalized;
        float a = - 4 * maxHeight / (l * l);

        for (; x < l; x += speed * Time.deltaTime) {
            Vector2 nextPositionXoZ = startPositionXoZ;
            nextPositionXoZ += direction * ((float)x);
            transform.position = new Vector3(
                    nextPositionXoZ.x,
                    a * x * (x - l) + startPosition.y,
                    nextPositionXoZ.y
                );
            yield return null;
        }

        transform.position = startPosition + new Vector3(displacement.x, 0, displacement.y);

        GameManager.m_instance.flags.isMoving = false;
    }

    public void MoveTo(Vector2 displacement) {
        //StartCoroutine("MoveAnimation", displacement);
        StartCoroutine(MoveAnimation(displacement));
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
