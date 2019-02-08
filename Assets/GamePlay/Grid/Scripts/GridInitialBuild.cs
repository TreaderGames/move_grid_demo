using System;
using UnityEngine;

public class GridInitialBuild : MonoBehaviour
{
    [SerializeField] RectTransform gridArea;
    [SerializeField] GameObject nodeTemplate;

    [SerializeField] GridData gridData;

    PlayerSpawn playerSpawn = null;
    
    float gridRowOffset = 0;
    float gridColumnOffset = 0;

    Vector3 gridAreaTopLeft;

    public PlayerSpawn pPlayerSpawn { get => playerSpawn; }
    public RectTransform pGridArea { get => gridArea; }

    // Start is called before the first frame update
    public void Init()
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
        Vector2 centerNode = GetCenterNodeCount();

        bool isCenterNode = false;
        CalculateGridOffset();

        for (int i = 1; i < gridData.columns; i++)
        {
            for (int j = 1; j < gridData.rows; j++)
            {
                isCenterNode = j == centerNode.x && i == centerNode.y;
                InstantiateNode(i, j, isCenterNode);
            }
        }
    }

    void InstantiateNode(int indexColumn, int indexRow, bool isCenter)
    {
        GameObject newNode = GameObject.Instantiate(nodeTemplate, gridArea);
        newNode.name = "(" + indexColumn + "," + indexRow + ")";
        newNode.transform.localPosition = new Vector3((gridColumnOffset * indexColumn) + gridAreaTopLeft.x, gridAreaTopLeft.y - (gridRowOffset * indexRow), 0);
        if (isCenter)
        {
            playerSpawn = newNode.AddComponent<PlayerSpawn>();
        }

        newNode.SetActive(true);
    }

    Vector2 GetCenterNodeCount()
    {
        Vector2 gridNodeCenter = new Vector2(Mathf.CeilToInt((gridData.rows-1)/2f) , Mathf.CeilToInt((gridData.columns-1)/2f));
        Debug.Log("Grid node center: " + gridNodeCenter);
        return gridNodeCenter;
    }
}
