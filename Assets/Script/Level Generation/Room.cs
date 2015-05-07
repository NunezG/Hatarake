using UnityEngine;
using System.Collections.Generic;

public class Room {

    public int posX, posY, width, height;
    public RoomType type;
    public List<Cell> cells = new List<Cell>();

    public Room(int x,int y,int width,int height,RoomType type,Cell[,] grid)
    {
        this.posX = x;
        this.posY = y;
        this.width = width;
        this.height = height;
        this.type = type;
        for (int i = x; i < x + width; i++)
            for (int j = y; j < y + height; j++)
                cells.Add(grid[i, j]);

    }
}
