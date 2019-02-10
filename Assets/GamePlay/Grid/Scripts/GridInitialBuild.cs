using UnityEngine;

/// <summary>
/// Populates the nodes in the grid with respect to the grid area.
/// </summary>
public class GridInitialBuild : GridBuilder
{
    [SerializeField] GameObject nodeTemplate;
    
    PlayerSpawn playerSpawn = null;
    
    public PlayerSpawn pPlayerSpawn { get => playerSpawn; }
    public RectTransform pGridArea { get => gridArea; }

    public override void Init()
    {
        base.Init();
        PopulateGridNodes();
    }

    void PopulateGridNodes()
    {
        Vector2 centerNode = GetCenterNodeCount();

        bool isCenterNode = false;

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
        newNode.name = "(" + indexColumn + "," + indexRow + ")"; //Naming by row column for easier debugging
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
        return gridNodeCenter;
    }
}
