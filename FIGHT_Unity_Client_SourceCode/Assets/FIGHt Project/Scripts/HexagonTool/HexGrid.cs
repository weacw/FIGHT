using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HexGrid
{
    private int width = 6;
    private int height = 6;
    private HexCell cellPrefab;
    private HexCell[] cells;
    private GameObject g = new GameObject("hex gird");

    public HexGrid(int width, int height, HexCell cell)
    {
        this.width = width;
        this.height = height;
        this.cellPrefab = cell;
    }
    //初始化hexcell
    public void Init()
    {
        cells = new HexCell[height * width];
      
        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateCell(x, z, i++);
            }
        }
    }

    //创建Hexcell
    private void CreateCell(int x, int z, int i)
    {
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
        position.y = 0f;
        position.z = z * (HexMetrics.outerRadius * 1.5f);
        HexCell cell = cells[i] = Object.Instantiate(cellPrefab);
        cell.transform.SetParent(g.transform, false);
        cell.transform.localPosition = position;
    }


}
