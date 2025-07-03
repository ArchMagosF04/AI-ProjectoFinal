using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    [SerializeField] private GameObject hexPrefab;

    //Both should be odd.
    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;

    [SerializeField] private float gap;

    private float hexWidth = 1.732f * 20;
    private float hexHeight = 2.0f * 20;

    private Vector3 startPos;

    private void Start()
    {
        AddGap();
        CalcStartPos();
        CreateGrid();
    }

    private void AddGap()
    {
        hexWidth += hexWidth * gap;
        hexHeight += hexHeight * gap;
    }

    private void CalcStartPos()
    {
        float offset = 0;
        if (gridHeight / 2 % 2 != 0) offset = hexWidth / 2;

        float x = - hexWidth * (gridWidth / 2) - offset;
        float z = hexHeight * 0.75f * (gridHeight / 2);

        startPos = new Vector3 (x, 0, z);
    }

    private Vector3 CalcWorldPos(Vector3 pos)
    {
        float offset = 0;
        if (pos.y % 2 != 0) offset = hexWidth / 2;

        float x = startPos.x + pos.x * hexWidth + offset;

        float z = startPos.z - pos.y * hexHeight * 0.75f;

        return new Vector3 (x, 0, z);
    }

    private void CreateGrid()
    {
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                Transform hex = Instantiate(hexPrefab).transform;
                Vector2 gridPos = new Vector2 (x, y);

                hex.position = CalcWorldPos(gridPos);
                hex.parent = this.transform;
                hex.name = $"Hex ({x}, {y})";
            }
        }
    }
}
