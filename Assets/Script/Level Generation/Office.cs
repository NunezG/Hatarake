using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class Office : MonoBehaviour {

	public GameObject floorPrefab,WallPrefab,DoorPrefab;

    public GameObject bossDeskPrefab, employeeDeskPrefab, canapPrefab, photocopierPrefab, toiletPrefab, vendingMachinePrefab, coffeeMachinePrefab;

	public List<GameObject> obstacles = new List<GameObject>();

    public List<Room> rooms = new List<Room>();


	public Cell [,] grid;
	public int size ;
	public Material[] materials;

    public int nbBossRooms, nbCoffeeRooms, nbBathRooms, nbBoxes;

    public int nbCoffeeMachine, nbToilet, nbLavabo, nbVendingMachine, nbPhotocopier, nbFlowerPot,nbTV;

    //array pour selection aleatoire dans les enums
    Array orientationValues = Enum.GetValues(typeof(Orientation));
    Array FurnitureValues = Enum.GetValues(typeof(FurnitureType));
    Array RoomValues = Enum.GetValues(typeof(RoomType));


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
				grid[i, j]= new Cell(this,i,j,RoomType.Corridor);


		fillArea(0, 0, size, size, RoomType.Corridor, true); // on pose les murs

        tryToRandomlyPlaceXRooms(nbBossRooms, 2, 3, RoomType.Bossroom);


        tryToRandomlyPlaceXRooms(nbCoffeeRooms, 2, 2, RoomType.Coffeeroom);


        tryToRandomlyPlaceXRooms(nbBathRooms, 1, 1, RoomType.Bathroom);


        tryToRandomlyPlaceXRooms(nbBoxes,1, 1, RoomType.Box);

        placingFurnitures();
		for (int i =0; i<size; i++) 
			for (int j =0; j<size; j++)
				Create3DCell (i, j);


        this.transform.localScale=new Vector3 (10, 10, 10);

	}

    public void placingFurnitures()
    {
        foreach (Room room in rooms)
        {
            switch (room.type)
            {
                case RoomType.Coffeeroom:
                    placingCoffeeRoomFurniture(room);
                    break;
                case RoomType.Bathroom:
                    placingBathroomFurniture(room);
                    break;
                case RoomType.Bossroom:
                    break;
                case RoomType.Box:
                    placingBoxFurniture(room);
                    break;
                case RoomType.Corridor:
                    break;
            }
        }
    }

    public void placingCoffeeRoomFurniture(Room room)
    {
        List<FurnitureType> coffeeRoomFurnitures = new List<FurnitureType>();
        for(int i=0;i<nbCoffeeMachine;i++){
            coffeeRoomFurnitures.Add(FurnitureType.CoffeeMachine);
        }

        for (int i = 0; i < nbVendingMachine; i++)
        {
            coffeeRoomFurnitures.Add(FurnitureType.VendingMachine);
        }
        for (int i = 0; i < nbTV; i++)
        {
            coffeeRoomFurnitures.Add(FurnitureType.TV);
        }
        Orientation randomOrientation;
        System.Random random = new System.Random();
        foreach (Cell cell in room.cells)
        {

            if (!cell.doorWest && !cell.doorSouth && !cell.doorNorth && !cell.doorEast)
            {
                randomOrientation = (Orientation)orientationValues.GetValue(random.Next(orientationValues.Length));
                int i = UnityEngine.Random.Range(0, coffeeRoomFurnitures.Count);
                cell.furnitures.Add(new Furniture(cell.posX, cell.posY, coffeeRoomFurnitures[i], randomOrientation));
                coffeeRoomFurnitures.RemoveAt(i);
            }

        }
    }
    public void placingBoxFurniture(Room room)
    {
        foreach (Cell cell in room.cells)
        {
            Orientation orientation=Orientation.North;
            if(cell.doorEast)
                orientation=Orientation.West;
            else if(cell.doorNorth)
                orientation=Orientation.South;
            else if(cell.doorSouth)
                orientation=Orientation.North;
            else if(cell.doorWest)
                orientation=Orientation.East;
            cell.furnitures.Add(new Furniture(cell.posX, cell.posY, FurnitureType.EmployeeDesk, orientation));
        }
    }

    public void placingBathroomFurniture(Room room)
    {
        foreach (Cell cell in room.cells)
        {
            Orientation orientation = Orientation.North;
            if (cell.doorEast)
                orientation = Orientation.West;
            else if (cell.doorNorth)
                orientation = Orientation.South;
            else if (cell.doorSouth)
                orientation = Orientation.North;
            else if (cell.doorWest)
                orientation = Orientation.East;
            cell.furnitures.Add(new Furniture(cell.posX, cell.posY, FurnitureType.Toilet, orientation));
        }
    }



    public int tryToRandomlyPlaceXRooms(int X,int width, int height, RoomType type)
    {
        int nbSuccess = 0;
        bool success;
        for (int i = 0; i < X; i++)
        {
            success = placeRoom(width, height, type);
            if (success)
            {
                nbSuccess++;
                print("success");
            }
            else
            {
                print("defeat");
            }
        }
        return nbSuccess;
    }
	
	private void Create3DCell (int x, int z) {
		GameObject newCell = Instantiate(floorPrefab) as GameObject;
		newCell.name = "Maze Cell " + x + ", " + z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3(x , 0f, z);

		switch(grid[x, z].type){
			case RoomType.Corridor :
				newCell.GetComponent<Renderer>().material=materials[0];
				break;
			case RoomType.Bossroom :
				newCell.GetComponent<Renderer>().material=materials[1];
				break;
			case RoomType.Coffeeroom :
				newCell.GetComponent<Renderer>().material=materials[2];
				break;
            case RoomType.Bathroom:
                newCell.GetComponent<Renderer>().material = materials[3];
                break;
            case RoomType.Box:
                newCell.GetComponent<Renderer>().material = materials[4];
                break;
		}
		
		obstacles.Add (newCell);
		if (grid [x, z].wallNorth) {
			GameObject newNorthWall = Instantiate(WallPrefab) as GameObject;
			newNorthWall.name = "NorthWall " + x + ", " + z;
			newNorthWall.transform.parent = transform;
			newNorthWall.transform.Translate(new Vector3(x , 0f, z));
			
			obstacles.Add (newNorthWall);
        }
        else if (grid[x, z].doorNorth && grid[x, z].type!=RoomType.Box)
        {
            GameObject newNorthDoor = Instantiate(DoorPrefab) as GameObject;
            newNorthDoor.name = "NorthDoor " + x + ", " + z;
            newNorthDoor.transform.parent = transform;
            newNorthDoor.transform.Translate(new Vector3(x, 0f, z));

            obstacles.Add(newNorthDoor);
        }
		if (grid [x, z].wallSouth) {
            GameObject newSouthWall = Instantiate(WallPrefab) as GameObject;
            newSouthWall.name = "SouthWall " + x + ", " + z;
            newSouthWall.transform.parent = transform;

            newSouthWall.transform.Translate(new Vector3(x, 0f, z));
            //------------------------
            newSouthWall.transform.Rotate(0, 180, 0);
            //-------------------
            obstacles.Add(newSouthWall);
        }
        else if (grid[x, z].doorSouth && grid[x, z].type != RoomType.Box)
        {
            GameObject newSouthDoor = Instantiate(DoorPrefab) as GameObject;
            newSouthDoor.name = "SouthDoor " + x + ", " + z;
            newSouthDoor.transform.parent = transform;
            newSouthDoor.transform.Translate(new Vector3(x, 0f, z));
            //------------------------
            newSouthDoor.transform.Rotate(0, 180, 0);
            //-------------------
            obstacles.Add(newSouthDoor);
        }
		if (grid [x, z].wallEast) {
			//GameObject newEastWall = Instantiate(EastWallPrefab) as GameObject;
            GameObject newEastWall = Instantiate(WallPrefab) as GameObject;
			newEastWall.name = "EastWall " + x + ", " + z;
			newEastWall.transform.parent = transform;

			newEastWall.transform.Translate(new Vector3(x , 0f, z));
            //------------------------
            newEastWall.transform.Rotate(0,-90,0);
            //-------------------
			obstacles.Add (newEastWall);
        }
        else if (grid[x, z].doorEast && grid[x, z].type != RoomType.Box)
        {
            GameObject newEastDoor = Instantiate(DoorPrefab) as GameObject;
            newEastDoor.name = "EastDoor " + x + ", " + z;
            newEastDoor.transform.parent = transform;
            newEastDoor.transform.Translate(new Vector3(x, 0f, z));
            //------------------------
            newEastDoor.transform.Rotate(0, -90, 0);
            //-------------------
            obstacles.Add(newEastDoor);
        }
		if (grid [x, z].wallWest) {
            GameObject newWestWall = Instantiate(WallPrefab) as GameObject;
			newWestWall.name = "WestWall " + x + ", " + z;
			newWestWall.transform.parent = transform;
            newWestWall.transform.Translate(new Vector3(x, 0f, z));
            //------------------------
            newWestWall.transform.Rotate(0, 90, 0);
            //-------------------
			obstacles.Add (newWestWall);
        }
        else if (grid[x, z].doorWest && grid[x, z].type != RoomType.Box)
        {
            GameObject newWestDoor = Instantiate(DoorPrefab) as GameObject;
            newWestDoor.name = "WestDoor " + x + ", " + z;
            newWestDoor.transform.parent = transform;
            newWestDoor.transform.Translate(new Vector3(x, 0f, z));

            //------------------------
            newWestDoor.transform.Rotate(0, 90, 0);
            //-------------------
            obstacles.Add(newWestDoor);
        }
        foreach (Furniture furniture in grid[x, z].furnitures)
        {

            GameObject newFurniture =null;
            string name;
            switch (furniture.type)
            {
                case FurnitureType.BossDesk:
                    newFurniture = Instantiate(bossDeskPrefab) as GameObject;
                    name ="BossDesk"+x+","+z;
                    break;
                case FurnitureType.Canap:
                    newFurniture = Instantiate(canapPrefab) as GameObject;
                    name ="Canap"+x+","+z;
                    break;
                case FurnitureType.CoffeeMachine:
                    newFurniture = Instantiate(coffeeMachinePrefab) as GameObject;
                    name ="CoffeeMachine"+x+","+z;
                    break;
                case FurnitureType.EmployeeDesk:
                    newFurniture = Instantiate(employeeDeskPrefab) as GameObject;
                    name ="EmployeeDesk"+x+","+z;
                    break;
                case FurnitureType.Photocopier:
                    newFurniture = Instantiate(photocopierPrefab) as GameObject;
                    name ="Photocopier"+x+","+z;
                    break;
                case FurnitureType.Toilet:
                    newFurniture = Instantiate(toiletPrefab) as GameObject;
                    name ="Toilet"+x+","+z;
                    break;
                case FurnitureType.TV:
                    newFurniture = Instantiate(canapPrefab) as GameObject;
                    name ="TV"+x+","+z;
                    break;
                case FurnitureType.VendingMachine:
                    newFurniture = Instantiate(vendingMachinePrefab) as GameObject;
                    name ="VendingMachine"+x+","+z;
                    break;
            }
            newFurniture.transform.parent = transform;
            newFurniture.transform.Translate(new Vector3(x, 0f, z));

            //------------------------
            newFurniture.transform.Rotate(0, furniture.orientationToDegree(), 0);
            //-------------------
            obstacles.Add(newFurniture);
        }

	}


    public bool placeRoom(int width, int height, RoomType type)
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
            int rPlace = UnityEngine.Random.Range(0, possiblePlacesForRoom.Count); // on en tire un au hasard

            int xRoom = (int)possiblePlacesForRoom[rPlace].x;
            int yRoom = (int)possiblePlacesForRoom[rPlace].y;

            fillArea(xRoom, yRoom, width, height, type, true); // on pose la salle
            rooms.Add(new Room(xRoom, yRoom, width, height, type,grid));
           // print("xRoom : " + xRoom + ", yRoom : " + yRoom);

            List<Vector2> possiblePlacesForDoor = new List<Vector2>(); // maintenant on va chercher la porte, on sait qu'il y a au moins un emplacement
            for (int i = xRoom; i < xRoom + width; i++) // parcours au dessus et en dessous
            {
                if (yRoom > 1 && grid[i, yRoom - 1].type == RoomType.Corridor) // si on trouve une case corridor autour de la salle
                    possiblePlacesForDoor.Add(new Vector2(i, yRoom - 1)); // on l'ajoute aux emplacements possibles pour la porte
                if (yRoom + height < size - 1 && grid[i, yRoom + height].type == RoomType.Corridor)
                    possiblePlacesForDoor.Add(new Vector2(i, yRoom + height));
            }
            for (int i = yRoom; i < yRoom + height; i++)// parcours a droite a gauche
            {
                if (xRoom > 1 && grid[xRoom - 1, i].type == RoomType.Corridor)
                    possiblePlacesForDoor.Add(new Vector2(xRoom - 1, i));
                if (xRoom + width < size - 1 && grid[xRoom + width, i].type == RoomType.Corridor)
                    possiblePlacesForDoor.Add(new Vector2(xRoom + width, i));
            }
           // for (int i = 0; i < possiblePlacesForDoor.Count;i++ ){

               // print("xDoor : " + possiblePlacesForDoor[i].x + ", yDoor : " + possiblePlacesForDoor[i].y);
            //}
                //print("Count : " + possiblePlacesForDoor.Count);
            int rDoor = UnityEngine.Random.Range(0, possiblePlacesForDoor.Count); // on en tire un au hasard

            int xDoor = (int)possiblePlacesForDoor[rDoor].x;
            int yDoor = (int)possiblePlacesForDoor[rDoor].y;

            //print("/xDoor : " + xDoor + ", yDoor : " + yDoor);

           // print("=============================================================");
            grid[xDoor,yDoor].locked = true; // on verrouille le corridor pour en tenir compte dans les collisions futures et eviter de boucher le passage

            if (xDoor == xRoom + width){ //si la porte se trouve a l'est
                grid[xDoor - 1, yDoor].wallEast = false;// on enleve le mur est de sa case ouest
                grid[xDoor - 1, yDoor].doorEast = true;// on ajoute la porte de sa case ouest
            }
            if (xDoor < xRoom){ //si la porte se trouve a l'ouest
                grid[xDoor + 1, yDoor].wallWest = false;// on enleve le mur ouest de sa case est
                grid[xDoor + 1, yDoor].doorWest = true;// on ajoute la porte ouest de sa case est
            }
            if (yDoor == yRoom + height){ //si la porte se trouve au sud
                grid[xDoor, yDoor - 1].wallSouth = false;// on enleve le mur sud de sa case nord
                grid[xDoor, yDoor - 1].doorSouth = true;// on ajoute la porte sud de sa case nord
            }
            if (yDoor < yRoom) { //si la porte se trouve au nord
                grid[xDoor, yDoor + 1].wallNorth = false;// on enleve le mur nord de sa case sud
                grid[xDoor, yDoor + 1].doorNorth = true;// on ajoute la porte nord de sa case sud
            }
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
                    if (grid[i, j].type != RoomType.Corridor || grid[i, j].locked) // et si on trouve une case non corridor, ou corridor verrouillee ( pour laisser le passage )
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
            if (y > 1 && grid[i, y - 1].type == RoomType.Corridor) // si on trouve au moins une case corridor autour de la salle
                doorCanBePlaced = true;
            if (y + height < size - 1 && grid[i, y + height].type == RoomType.Corridor)
                doorCanBePlaced = true;
        }
        for (int i = y; i < y + height; i++)
        {
            if (x > 1 && grid[x - 1, i].type == RoomType.Corridor) 
                doorCanBePlaced = true;
            if (x + width < size - 1 && grid[x + width, i].type == RoomType.Corridor) 
                doorCanBePlaced = true;
        }

        return doorCanBePlaced;
    }

    public bool breakCorridorsConnexion(int x, int y, int width, int height)
    {
        bool breakCorridorsConnexion = false;

        fillArea(x, y, width, height, RoomType.Bossroom, false);// on remplit la zone pour tester
        // et on verifie que nos corridors sont toujours bien tous connecte
        if (!corridorsAllConnected())// si ce n'est pas le cas
            breakCorridorsConnexion = true;
        else
            breakCorridorsConnexion = false;

        fillArea(x, y, width, height, RoomType.Corridor, false); // on remet la zone dans son etat initial

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
                if (grid[i, j].type == RoomType.Corridor)
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
                if (grid[i, j].type == RoomType.Corridor && !grid[i, j].check)  // si on trouve une seule case corridor non marquee
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
        if (x < size - 1 && grid[x + 1, y].type == RoomType.Corridor && !grid[x + 1, y].check) // voisin de droite
            crawlCorridorsCells(x + 1, y);
        if (x > 1 && grid[x - 1, y].type == RoomType.Corridor && !grid[x - 1, y].check) // voisin de gauche
            crawlCorridorsCells(x - 1, y);
        if (y < size - 1 && grid[x, y + 1].type == RoomType.Corridor && !grid[x, y + 1].check) // voisin du bas
            crawlCorridorsCells(x, y + 1);
        if (y > 1 && grid[x, y - 1].type == RoomType.Corridor && !grid[x, y - 1].check) // voisin du haut
            crawlCorridorsCells(x, y - 1);
    }

    public bool collision (int x, int y, int width, int height)
    {
        bool collision = true;
        if (!placeAlreadyTaken(x, y, width, height)   && doorCanBePlaced(x, y, width, height) && !breakCorridorsConnexion(x, y, width, height))
            collision = false;

        return collision;

    }

	// remplie une zone de la grille avec un type de case
	public void fillArea(int x,int y, int width, int height, RoomType type, bool addTheWall){

		for (int i =x; i<x+width; i++)
			for (int j =y; j<y+height; j++) {
				//grid [i, j] = new Cell (this, i, j, RoomType);
                grid[i, j].type = type;

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
        if (addTheWall) {

            GetComponent<Triggers>().addTrigger(x, y, width, height, type);
        
        }
		
	}





}
