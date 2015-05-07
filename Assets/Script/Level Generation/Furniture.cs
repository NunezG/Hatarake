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

    public int orientationToDegree()
    {
        int orientationInDegree = 0;
        switch (orientation)
        {
            case Orientation.North:
                orientationInDegree = 0;
                break;
            case Orientation.South:
                orientationInDegree = 180;
                break;
            case Orientation.West:
                orientationInDegree = 90;
                break;
            case Orientation.East:
                orientationInDegree = -90;
                break;
        }
        return orientationInDegree;
    }

}
