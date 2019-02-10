using UnityEngine;

/// <summary>
/// Creates the line grid
/// </summary>
public class GridLineBuilder : GridBuilder
{
    [SerializeField] GameObject horizontalLine;
    [SerializeField] GameObject verticalLine;

    GameObject lineParent;

    public override void Init()
    {
        base.Init();
        InitLinesParent();
        PopulateLines();
    }

    void PopulateLines()
    {
        int rows = gridData.rows;
        int columns = gridData.columns;

        for (int i = 1; i < rows; i++)
        {
            InitLine(true, i);
        }

        for (int i = 1; i < columns; i++)
        {
            InitLine(false, i);
        }
    }

    void InitLinesParent()
    {
        lineParent = new GameObject();
        lineParent.transform.SetParent(gridArea);
    }

    void InitLine(bool isHorzontal, int index)
    {
        GameObject line = isHorzontal ? horizontalLine : verticalLine;

        line = GameObject.Instantiate<GameObject>(line, gridArea);

        if (isHorzontal)
        {
            line.transform.localPosition = new Vector3(0, gridAreaTopLeft.y - (gridRowOffset * index), 0);
        }
        else
        {
            line.transform.localPosition = new Vector3((gridColumnOffset * index) + gridAreaTopLeft.x, 0, 0);
        }

        line.SetActive(true);
        line.transform.SetAsFirstSibling();
    }
}
