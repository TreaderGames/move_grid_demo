using System;
using UnityEngine;

public class GridInitialBuild : MonoBehaviour
{
    [SerializeField] RectTransform gridArea;
    [SerializeField] GameObject nodeTemplate;

    [SerializeField] GridData gridData;
    
    float gridRowOffset = 0;
    float gridColumnOffset = 0;

    Vector3 gridAreaTopLeft;
    // Start is called before the first frame update
    void Start()
    {
        Vector3[] corners = new Vector3[4];

        gridArea.GetLocalCorners(corners);
        gridAreaTopLeft = corners[1];
        PopulateGridNodes();
    }

    void CalculateGridOffset()
    {
        if (gridData.rows > 0 && gridData.columns > 0) 
        {
            gridRowOffset = gridArea.rect.height / (gridData.rows);
            gridColumnOffset = gridArea.rect.width / (gridData.columns);
        }
    }

    void PopulateGridNodes()
    {
        CalculateGridOffset();

        for (int i = 1; i < gridData.columns; i++)
        {
            for (int j = 1; j < gridData.rows; j++)
            {
                GameObject newNode = GameObject.Instantiate(nodeTemplate, gridArea);
                newNode.name = "(" + i + "," + j + ")";
                newNode.transform.localPosition = new Vector3((gridColumnOffset * i) + gridAreaTopLeft.x, gridAreaTopLeft.y - (gridRowOffset * j), 0);
                newNode.SetActive(true);
            }
        }
    }
}
