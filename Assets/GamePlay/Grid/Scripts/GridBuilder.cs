using UnityEngine;

/// <summary>
/// Base class for all grid building classes
/// </summary>
public abstract class GridBuilder : MonoBehaviour
{
    [SerializeField] protected RectTransform gridArea;
    [SerializeField] protected GridData gridData;

    protected float gridRowOffset = 0;
    protected float gridColumnOffset = 0;

    protected Vector3 gridAreaTopLeft;

    public virtual void Init()
    {
        CalculateGridOffset();

        Vector3[] corners = new Vector3[4];

        gridArea.GetLocalCorners(corners);

        //Get the top left corner of the grid area to start populating from. https://docs.unity3d.com/ScriptReference/RectTransform.GetLocalCorners.html
        gridAreaTopLeft = corners[1]; 
    }

    /// <summary>
    /// Calulates the spacing between each grid 
    /// </summary>
    protected void CalculateGridOffset()
    {
        if (gridData.rows > 0 && gridData.columns > 0)
        {
            gridRowOffset = gridArea.rect.height / (gridData.rows);
            gridColumnOffset = gridArea.rect.width / (gridData.columns);
        }
    }
}
