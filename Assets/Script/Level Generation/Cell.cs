using UnityEngine;
using System.Collections.Generic;

public class Cell  {

    Office office;
    public bool wallNorth, wallEast, wallSouth, wallWest;
    public bool doorNorth, doorEast, doorSouth, doorWest;
	public int posX,posY;
	public bool check=false;
    public bool locked = false;

	public RoomType type;

    public List<Furniture> furnitures=new List<Furniture>();

	// Use this for initialization
	public Cell(Office office,int posX,int posY,RoomType type ){
		this.office = office;		
		this.posX = posX;
		this.posY = posY;
		this.type = type;
		wallNorth = wallEast = wallSouth = wallWest = doorEast = doorNorth = doorSouth = doorWest = false;
	}

	public void init(Office office,int posX,int posY, bool north,bool east,bool south,bool west,RoomType type ){
		this.office = office;
		
		this.posX = posX;
		this.posY = posY;

		this.wallNorth = north;
		this.wallEast = east;
		this.wallSouth = south;
		this.wallWest = west;
	}

	public void init(Office office,int posX,int posY,RoomType type ){
		this.office = office;

		this.posX = posX;
		this.posY = posY;
		this.type = type;		
	}

}
