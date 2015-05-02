using UnityEngine;
using System.Collections.Generic;

public class Office : MonoBehaviour {

	public GameObject floorPrefab,NorthWallPrefab,SouthWallPrefab,EastWallPrefab,WestWallPrefab;

	public List<GameObject> obstacles = new List<GameObject>();

	Cell [,] grid;
	public int size = 10;
	public Material[] materials;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void init(){
		
		grid = new Cell[size,size];
		
		for (int i =0; i<size; i++) 
			for (int j =0; j<size; j++)
				grid[i, j]= new Cell(this,i,j,Cell.CellType.Corridor);
		int bossRoomTry = 10;
		bool bossRoomPlaced = false;

		fillArea (0, 0, size, size, Cell.CellType.Corridor, true,false);
		fillArea (0, 0, 3, 2, Cell.CellType.Bossroom, true,true);
		
		fillArea (0,2, 1, 1, Cell.CellType.Corridor, true,true);
		fillArea (0,3, 1, 1, Cell.CellType.Corridor, true,true);
		fillArea (0,4, 1, 1, Cell.CellType.Corridor, true,true);
		fillArea (0,5, 1, 1, Cell.CellType.Corridor, true,true);

		
		fillArea (4,0, 2, 2, Cell.CellType.Coffeeroom, true,true);
		
		fillArea (4,3, 2, 2, Cell.CellType.Bathroom, true,true);

		
		fillArea (8,8, 2, 2, Cell.CellType.Coffeeroom, true,true);
		fillArea (6,5, 3, 3, Cell.CellType.Bathroom, true,true);

		
		fillArea (6,5, 3, 3, Cell.CellType.Bathroom, true,true);
		fillArea (0,6, 4, 4, Cell.CellType.Coffeeroom, true,true);

		
		fillArea (7,0, 3, 3, Cell.CellType.Coffeeroom, true,true);
		//fillArea (4,4, 2, 2, Cell.CellType.Corridor, true,true);

		/*while (bossRoomTry>0 && !bossRoomPlaced) {
			bossRoomTry--;
			bossRoomPlaced=placeBossRoom();
		}*/


		
		for (int i =0; i<size; i++) 
			for (int j =0; j<size; j++)
				Create3DCell (i, j);

	}
	public void addCorresponding3DObject(){
		
		for (int i =0; i<size; i++) 
			for (int j =0; j<size; j++)
				grid[i, j]= new Cell(this,i,j,Cell.CellType.Corridor);

	}

	
	private void Create3DCell (int x, int z) {
		GameObject newCell = Instantiate(floorPrefab) as GameObject;
		newCell.name = "Maze Cell " + x + ", " + z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3(x , 0f, z);

		switch(grid[x, z].cellType){
			case Cell.CellType.Corridor :
				newCell.GetComponent<Renderer>().material=materials[0];
				break;
			case Cell.CellType.Bossroom :
				newCell.GetComponent<Renderer>().material=materials[1];
				break;
			case Cell.CellType.Coffeeroom :
				newCell.GetComponent<Renderer>().material=materials[2];
				break;
			case Cell.CellType.Bathroom :
				newCell.GetComponent<Renderer>().material=materials[3];
				break;
		}
		
		obstacles.Add (newCell);
		if (grid [x, z].wallNorth) {			
			GameObject newNorthWall = Instantiate(NorthWallPrefab) as GameObject;
			newNorthWall.name = "NorthWall " + x + ", " + z;
			newNorthWall.transform.parent = transform;
			newNorthWall.transform.Translate(new Vector3(x , 0f, z));
			
			obstacles.Add (newNorthWall);
		}		
		if (grid [x, z].wallSouth) {
			GameObject newNSouthWall = Instantiate(SouthWallPrefab) as GameObject;
			newNSouthWall.name = "SouthWall " + x + ", " + z;
			newNSouthWall.transform.parent = transform;

			newNSouthWall.transform.Translate(new Vector3(x , 0f, z ));
			obstacles.Add (newNSouthWall);
		}		
		if (grid [x, z].wallEast) {
			GameObject newEastWall = Instantiate(EastWallPrefab) as GameObject;
			newEastWall.name = "EastWall " + x + ", " + z;
			newEastWall.transform.parent = transform;

			newEastWall.transform.Translate(new Vector3(x , 0f, z));
			obstacles.Add (newEastWall);
		}		
		if (grid [x, z].wallWest) {
			GameObject newWestWall = Instantiate(WestWallPrefab) as GameObject;
			newWestWall.name = "WestWall " + x + ", " + z;
			newWestWall.transform.parent = transform;
			newWestWall.transform.Translate(new Vector3(x , 0f, z));
			obstacles.Add (newWestWall);
		}

	}


	public bool placeBossRoom(){
		bool successfullyPlaced = true;
		int posX = Random.Range (0, size);
		int posY = Random.Range (0, size);
		if (!collisionWithGrid (posX, posY, 2, 3))
			fillArea (posX, posY, 2, 3, Cell.CellType.Bossroom,true,false);
		else if (!collisionWithGrid (posX, posY, 3, 2))			
			fillArea (posX, posY, 2, 3, Cell.CellType.Bossroom,true,false);
		else
			successfullyPlaced = false;

		return successfullyPlaced;
	}

	public bool collisionWithGrid(int x,int y, int width, int height){
		bool collision = true;
		//si il ya au moins une cell de type corridor
		for (int i =x; i<x+width; i++) 
			for (int j =y; j<y+width; j++)
				if(grid[i,j].cellType==Cell.CellType.Corridor) return collision ;
		//si on ne trouve pas  AU MOINS UNE case de libre ( == corridor ) autour de cette position il y a aussi collision
		bool noCellFree = true;
		for (int i = x; i<x+width; i++) {
			if (y!=0 && grid[i,y-1].cellType==Cell.CellType.Corridor)	noCellFree = false;			
			if (y + height!= size && grid [i,y + height].cellType==Cell.CellType.Corridor) noCellFree = false;
		}
		for (int i = y; i<y+height; i++) {			
			if (x!=0 && grid[x-1,i].cellType==Cell.CellType.Corridor)	noCellFree = false;			
			if (x+width!=size && grid [x+width,i].cellType==Cell.CellType.Corridor) noCellFree = false;
		}
		if (noCellFree)
			return collision;

		// maintenant on remplie la zone en question
		fillArea (x, y, width, height, Cell.CellType.Bossroom,false,false);
		// et on verifie que nos corridors sont toujours bien tous connecte
		if (!corridorsAllConnected ()) {// si ce n'est pas le cas
			fillArea (x, y, width, height, Cell.CellType.Corridor, false,false); //on annule le remplissage
			return collision; //et il ya collision
		} else {
			fillArea (x, y, width, height, Cell.CellType.Corridor, false,false); //on annule le remplissage
			return !collision;
		}

	}



	//methode renvoyant vrai si toutes les cases corridors sont bien connectees les unes aux autres, faux sinon
	public bool corridorsAllConnected(){
		
		bool corridorsAintAllConnected = true;

		// nettoyage avant parcours ( à virer plus tard une fois que tous sera fait proprement
		for (int i =0; i<size; i++)
			for (int j =0; j<size; j++)
				grid [i,j].check = false;

		// on recupere la premiere case corridor que l'on trouve
		bool found = false;
		for (int i =0; i<size; i++) {
			for (int j =0; j<size; j++) {
				if (grid [i, j].cellType == Cell.CellType.Corridor) { 
					found=true;
					crawlCorridorsCells (i, j); // à partir de cette case on marque toutes les cases corridors connexe
					break;
				}
			}
			
			if(found) break;
		}

		for (int i =0; i<size; i++)
			for (int j =0; j<size; j++)
				if(grid [i,j].cellType == Cell.CellType.Corridor && !grid [i,j].check)  // si on trouve une seule case corridor non marquee
					return corridorsAintAllConnected; // c'est que toutes nos cases corridors ne sont plus connexes


		return !corridorsAintAllConnected; // sinon tout va bien
	}

	//parcours l'ensemble des cases corridor connectees à la case passee en parametre, et les marques
	public void crawlCorridorsCells(int x,int y){
		grid [x,y].check = true;
			if (x<size && grid [x + 1, y].cellType == Cell.CellType.Corridor && !grid [x + 1, y].check)
				crawlCorridorsCells (x + 1, y);
			if (x>=0 && grid [x - 1, y].cellType == Cell.CellType.Corridor && !grid [x - 1, y].check)
				crawlCorridorsCells (x - 1, y);
			if (y<size && grid [x, y + 1].cellType == Cell.CellType.Corridor && !grid [x, y + 1].check)
				crawlCorridorsCells (x, y + 1);
			if (y>=0 && grid [x, y - 1].cellType == Cell.CellType.Corridor && !grid [x, y - 1].check)
				crawlCorridorsCells (x, y - 1);

		
		
	}
	// remplie une zone de la grille avec un type de case
	public void fillArea(int x,int y, int width, int height, Cell.CellType cellType, bool addTheWall,bool addTheDoor){

		for (int i =x; i<x+width; i++)
			for (int j =y; j<y+height; j++) {
				grid [i, j] = new Cell (this, i, j, cellType);


				if (addTheWall) { // si on veut ajouter les murs de la salle

					if (i == x)
						grid [i, j].wallWest = true; // si le mur est sur le cote Ouest
					if (i == x + width - 1)
						grid [i, j].wallEast = true; // si le mur est sur le cote Est
					if (j == y)
						grid [i, j].wallSouth = true; // si le mur est sur le cote Nord
					if (j == y + height - 1)
						grid [i, j].wallNorth = true; // si le mur est sur le cote Sud
					
					
				}// end if
		}//end for
		if (addTheWall && addTheDoor) {
			
			int doorX =x+1, doorY=y+1;
			for (int k = x; k<x+width; k++) {
				if (y != 0 && grid [k, y - 1].cellType == Cell.CellType.Corridor) { // parcours en haut
					doorX = k;
					doorY = y - 1;
				}
				if (y + height != size && grid [k, y + height].cellType == Cell.CellType.Corridor) { // parcours en bas
					doorX = k;
					doorY = y + height;
				}
			}
			for (int k = y; k<y+height; k++) {			
				if (x != 0 && grid [x - 1, k].cellType == Cell.CellType.Corridor) {	// parcours a gauche
					doorX = x - 1;
					doorY = k;		
				}	
				if (x + width != size && grid [x + width, k].cellType == Cell.CellType.Corridor) {// parcours a droite
					doorX = x + width;
					doorY = k;
				}
			}

			if (doorX < x)
				grid [x, doorY].wallWest = false;
			if (doorX == x+width)
				grid [x+width-1, doorY].wallEast = false;
			if(doorY<y)
				grid [doorX, y].wallNorth = false;
			if(doorY==y+height)
				grid [doorX, y+height-1].wallSouth = false;
		}


		GetComponent<Triggers> ().addTrigger (x, y, width, height, cellType);

	}






}
