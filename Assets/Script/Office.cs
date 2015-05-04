using UnityEngine;
using System.Collections.Generic;

public class Office : MonoBehaviour {

	public GameObject floorPrefab,NorthWallPrefab,SouthWallPrefab,EastWallPrefab,WestWallPrefab;

	public List<GameObject> obstacles = new List<GameObject>();

	public Cell [,] grid;
	public int size = 10;
	public Material[] materials;

    public int nbBossRooms, nbCoffeeRooms, nbBathRooms, nbBoxes;
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


		fillArea(0, 0, size, size, Cell.CellType.Corridor, true); // on pose les murs
        for (int i = 0; i < nbBossRooms; i++)
        {
            if (placeRoom(2, 3, Cell.CellType.Bossroom))
                print("success");
            else
                print("defeat");
        }


        for (int i = 0; i < nbCoffeeRooms; i++)
        {
            if (placeRoom(2, 2, Cell.CellType.Coffeeroom))
                print("success");
            else
                print("defeat");
        }

        for (int i = 0; i < nbBathRooms; i++)
        {
            if (placeRoom(1, 1, Cell.CellType.Bathroom))
                print("success");
            else
                print("defeat");
        }

        for (int i = 0; i < nbBoxes; i++)
        {
            if (placeRoom(1, 1, Cell.CellType.Box))
                print("success");
            else
                print("defeat");
        }

		for (int i =0; i<size; i++) 
			for (int j =0; j<size; j++)
				Create3DCell (i, j);

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
            case Cell.CellType.Bathroom:
                newCell.GetComponent<Renderer>().material = materials[3];
                break;
            case Cell.CellType.Box:
                newCell.GetComponent<Renderer>().material = materials[4];
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


    public bool placeRoom(int width, int height, Cell.CellType cellType)
    {
        bool roomForTheRoom = false;
        List<Vector2> possiblePlacesForRoom = new List<Vector2>();
        for (int i =0; i<size; i++)// on parcours l'intégralité de la grille
            for (int j = 0; j < size; j++)
            {
                if (!collision(i, j, width, height))
                    possiblePlacesForRoom.Add(new Vector2(i, j)); // on recupere les emplacements ou l'on peut poser notre piece
            }



        if (possiblePlacesForRoom.Count > 0) // si on a trouve au moins un emplacement pour la salle
        {
            int rPlace = Random.Range(0, possiblePlacesForRoom.Count); // on en tire un au hasard

            int xRoom = (int)possiblePlacesForRoom[rPlace].x;
            int yRoom = (int)possiblePlacesForRoom[rPlace].y;

            fillArea(xRoom, yRoom, width, height, cellType, true); // on pose la salle

           // print("xRoom : " + xRoom + ", yRoom : " + yRoom);

            List<Vector2> possiblePlacesForDoor = new List<Vector2>(); // maintenant on va chercher la porte, on sait qu'il y a au moins un emplacement
            for (int i = xRoom; i < xRoom + width; i++) // parcours au dessus et en dessous
            {
                if (yRoom > 1 && grid[i, yRoom - 1].cellType == Cell.CellType.Corridor) // si on trouve une case corridor autour de la salle
                    possiblePlacesForDoor.Add(new Vector2(i, yRoom - 1)); // on l'ajoute aux emplacements possibles pour la porte
                if (yRoom + height < size-1 && grid[i, yRoom + height].cellType == Cell.CellType.Corridor)
                    possiblePlacesForDoor.Add(new Vector2(i, yRoom + height));
            }
            for (int i = yRoom; i < yRoom + height; i++)// parcours a droite a gauche
            {
                if (xRoom > 1 && grid[xRoom - 1, i].cellType == Cell.CellType.Corridor)
                    possiblePlacesForDoor.Add(new Vector2(xRoom - 1, i));
                if (xRoom + width < size-1 && grid[xRoom + width, i].cellType == Cell.CellType.Corridor)
                    possiblePlacesForDoor.Add(new Vector2(xRoom + width, i));
            }
            for (int i = 0; i < possiblePlacesForDoor.Count;i++ ){

               // print("xDoor : " + possiblePlacesForDoor[i].x + ", yDoor : " + possiblePlacesForDoor[i].y);
            }
                //print("Count : " + possiblePlacesForDoor.Count);
            int rDoor = Random.Range(0, possiblePlacesForDoor.Count); // on en tire un au hasard

            int xDoor = (int)possiblePlacesForDoor[rDoor].x;
            int yDoor = (int)possiblePlacesForDoor[rDoor].y;

            //print("/xDoor : " + xDoor + ", yDoor : " + yDoor);

           // print("=============================================================");
            grid[xDoor,yDoor].locked = true; // on verrouille le corridor pour en tenir compte dans les collisions futures et eviter de boucher le passage

            if (xDoor == xRoom + width) //si la porte se trouve a l'est
                grid[xDoor - 1, yDoor].wallEast = false;// on enleve le mur est de sa case ouest
            if (xDoor < xRoom ) //si la porte se trouve a l'ouest
                grid[xDoor + 1, yDoor].wallWest = false;// on enleve le mur ouest de sa case est
            if (yDoor == yRoom + height) //si la porte se trouve au sud
                grid[xDoor , yDoor-1].wallSouth = false;// on enleve le mur sud de sa case nord
            if (yDoor <yRoom ) //si la porte se trouve au nord
                grid[xDoor, yDoor+1].wallNorth = false;// on enleve le mur nord de sa case sud

            roomForTheRoom = true;
        }
        else
        {
            roomForTheRoom = false;
        }
        return roomForTheRoom;


    }


    // renvoie vrai si la place est deja occupe par une case de type autre que corridor
    public bool placeAlreadyTaken(int x, int y, int width, int height)
    {
        bool placeAlreadyTaken = false;
        if (x >= 0 && y >= 0 && x + width <= size && y + height <= size) { // si notre salle ne depasse pas des limites de la grille
            //on parcours l'ensemble des cases qu'elle occupera
            for (int i = x; i < x + width; i++)
                for (int j = y; j < y + height; j++)
                    if (grid[i, j].cellType != Cell.CellType.Corridor ||grid[i, j].locked) // et si on trouve une case non corridor, ou corridor verrouillee ( pour laisser le passage )
                        placeAlreadyTaken=true; // alors la place est prise
        }
        else
        {
            placeAlreadyTaken = true;
        }
        return placeAlreadyTaken;
    }

    // renvoie vrai si il y a au moins une case de libre autour pour mettre une porte
    public bool doorCanBePlaced(int x, int y, int width, int height)
    {
        bool doorCanBePlaced = false;
        for (int i = x; i < x + width; i++) // parcours au dessus et en dessous
        {
            if (y > 1 && grid[i, y - 1].cellType == Cell.CellType.Corridor) // si on trouve au moins une case corridor autour de la salle
                doorCanBePlaced = true;
            if (y + height < size -1 && grid[i, y + height].cellType == Cell.CellType.Corridor)
                doorCanBePlaced = true;
        }
        for (int i = y; i < y + height; i++)
        {
            if (x > 1 && grid[x - 1, i].cellType == Cell.CellType.Corridor) 
                doorCanBePlaced = true;
            if (x + width < size -1 && grid[x + width, i].cellType == Cell.CellType.Corridor) 
                doorCanBePlaced = true;
        }

        return doorCanBePlaced;
    }

    public bool breakCorridorsConnexion(int x, int y, int width, int height)
    {
        bool breakCorridorsConnexion = false;

        fillArea(x, y, width, height, Cell.CellType.Bossroom, false);// on remplit la zone pour tester
        // et on verifie que nos corridors sont toujours bien tous connecte
        if (!corridorsAllConnected())// si ce n'est pas le cas
            breakCorridorsConnexion = true;
        else
            breakCorridorsConnexion = false;

        fillArea(x, y, width, height, Cell.CellType.Corridor, false); // on remet la zone dans son etat initial

        return breakCorridorsConnexion;
    }

    //methode renvoyant vrai si toutes les cases corridors sont bien connectees les unes aux autres, faux sinon
    public bool corridorsAllConnected()
    {

        bool corridorsAllConnected = true;

        // on recupere la premiere case corridor que l'on trouve
        bool found = false;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (grid[i, j].cellType == Cell.CellType.Corridor)
                {
                    found = true;
                    crawlCorridorsCells(i, j); // à partir de cette case on marque toutes les cases corridors connexe
                    break;
                }
            }

            if (found) break;
        }

        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                if (grid[i, j].cellType == Cell.CellType.Corridor && !grid[i, j].check)  // si on trouve une seule case corridor non marquee
                    corridorsAllConnected = false; // c'est que toutes nos cases corridors ne sont plus connexes

        // nettoyage après parcours
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                grid[i, j].check = false;

        return corridorsAllConnected; 
    }

    //parcours l'ensemble des cases corridor non marquees connectees à la case passee en parametre, et les marques
    public void crawlCorridorsCells(int x, int y)
    {
        grid[x, y].check = true;
        if (x < size-1 && grid[x + 1, y].cellType == Cell.CellType.Corridor && !grid[x + 1, y].check) // voisin de droite
            crawlCorridorsCells(x + 1, y);
        if (x > 1 && grid[x - 1, y].cellType == Cell.CellType.Corridor && !grid[x - 1, y].check) // voisin de gauche
            crawlCorridorsCells(x - 1, y);
        if (y < size-1 && grid[x, y + 1].cellType == Cell.CellType.Corridor && !grid[x, y + 1].check) // voisin du bas
            crawlCorridorsCells(x, y + 1);
        if (y > 1 && grid[x, y - 1].cellType == Cell.CellType.Corridor && !grid[x, y - 1].check) // voisin du haut
            crawlCorridorsCells(x, y - 1);
    }

    public bool collision (int x, int y, int width, int height)
    {
        bool collision = true;
        if (!placeAlreadyTaken(x, y, width, height)   && doorCanBePlaced(x, y, width, height) && !breakCorridorsConnexion(x, y, width, height))
            collision = false;

        return collision;

    }




    // à l'initialisation, créer une list des cases libres



	// remplie une zone de la grille avec un type de case
	public void fillArea(int x,int y, int width, int height, Cell.CellType cellType, bool addTheWall){

		for (int i =x; i<x+width; i++)
			for (int j =y; j<y+height; j++) {
				//grid [i, j] = new Cell (this, i, j, cellType);
                grid[i, j].cellType = cellType;

				if (addTheWall) { // si on veut ajouter les murs de la salle

					if (i == x)
						grid [i, j].wallWest = true; // si le mur est sur le cote Ouest
					if (i == x + width - 1)
						grid [i, j].wallEast = true; // si le mur est sur le cote Est
					if (j == y)
                        grid[i, j].wallNorth = true; // si le mur est sur le cote Nord
					if (j == y + height - 1)
						grid [i, j].wallSouth = true; // si le mur est sur le cote Sud
					
					
				}// end if
		    }//end for

		GetComponent<Triggers> ().addTrigger (x, y, width, height, cellType);
		
	}




}
