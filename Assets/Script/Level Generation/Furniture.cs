using UnityEngine;
using System.Collections;

public class Furniture {
    public int cellX, cellY;
    public FurnitureType type;
    public Orientation orientation;

    public Furniture(int x, int y, FurnitureType type, Orientation orientation)
    {
        cellX = x;
        cellY = y;
        this.type = type;
        this.orientation = orientation;
    }

}
