using UnityEngine;
using System.Collections.Generic;
using System;

using System.Collections;
public class Office : MonoBehaviour
{

    public GameObject floorPrefab,backgroundPrefab, WallPrefab, DoorPrefab, windowPrefab;

    public GameObject bossDeskPrefab,glassTablePrefab, employeeDeskPrefab, canapPrefab,tvPrefab, photocopierPrefab, toiletPrefab, vendingMachinePrefab, coffeeMachinePrefab, carpetPrefab, flowerPotPrefab, casier0Prefab, casier1Prefab;

    public GameObject[] corridorsDecoPreFab, corridorChillPrefab, corridorWorkPrefab,
                        boxesPrefab,
                        bathroomPrefab,
                        coffeeRoomDecoPrefab, coffeeRoomChillPrefab,
                        bossRoomDecoPrefab, bossRoomFixedPrefab;


    public List<GameObject> obstacles = new List<GameObject>();

    public List<Room> rooms = new List<Room>();

    public Cell[,] grid;
    public int size;
    public Material[] materials;

    public int nbBossRooms, nbCoffeeRooms, nbBathRooms, nbBoxes;

    public int nbCoffeeMachine, nbToilet, nbLavabo, nbVendingMachine, nbPhotocopier, nbFlowerPot, nbTV, nbCasier;

    public int floor;

    //array pour selection aleatoire dans les enums
    Array orientationValues = Enum.GetValues(typeof(Orientation));
    Array FurnitureValues = Enum.GetValues(typeof(FurnitureType));
    Array RoomValues = Enum.GetValues(typeof(RoomType));

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void unlockCells()
    {
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                grid[i, j].locked = false;
    }

    public void init(int floor, int nbBossRooms, int nbCoffeeRooms, int nbBathRooms, int nbBoxes)
    {

        this.floor = floor;
        this.nbBathRooms = nbBathRooms;
        this.nbBossRooms = nbBossRooms;
        this.nbCoffeeRooms = nbCoffeeRooms;
        this.nbBoxes = nbBoxes;
        grid = new Cell[size, size];

        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                grid[i, j] = new Cell(this, i, j, RoomType.Corridor);

        fillArea(0, 0, size, size, RoomType.Corridor, true); // on pose les murs

        placeFixedCorridors();
        createElevatorRoom();

        int rdmBossRoom = UnityEngine.Random.Range(0, 2);
        if (rdmBossRoom == 0)
            tryToRandomlyPlaceXRooms(nbBossRooms, 2, 3, RoomType.Bossroom);
        else
            tryToRandomlyPlaceXRooms(nbBossRooms, 3, 2, RoomType.Bossroom);

        tryToRandomlyPlaceXRooms(nbCoffeeRooms, 2, 2, RoomType.Coffeeroom);

        tryToRandomlyPlaceXRooms(nbBathRooms, 1, 1, RoomType.Bathroom);

        tryToRandomlyPlaceXRooms(nbBoxes, 1, 1, RoomType.Box);
        createCorridorRoom();
        unlockCells();
        placingFurnituresInOffice();
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                Create3DCell(i, j);

        GameObject newBackground = Instantiate(backgroundPrefab) as GameObject;
        newBackground.name = "Background floor" + floor;
        newBackground.transform.parent = transform;
        newBackground.transform.localPosition = new Vector3(4, -1.0f, 4);

        this.gameObject.transform.position=new Vector3(200*floor,0,0);
        this.transform.localScale = new Vector3(10, 10, 10);
    }

    public void placingFurnituresInOffice()
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
                    placingBossRoomFurniture(room);
                    break;
                case RoomType.Box:
                    placingBoxFurniture(room);
                    break;
                case RoomType.Corridor:
                    placingCorridorFurniture(room);
                    break;
            }
        }
    }

    public bool placingFurniture(Cell cell, FurnitureType type)
    {
        if (cell.locked) return false;
        bool furniturePlaced = true;
        bool againstAWall, oppositeToDoor, whatever, notOnDoorCell;
        List<Orientation> possibleOrientation = new List<Orientation>();
        notOnDoorCell = againstAWall = oppositeToDoor = whatever = false;
        switch (type)
        {
            case FurnitureType.Casier:
                againstAWall = true;
                break;
            case FurnitureType.FlowerPot:
                againstAWall = true;
                break;
            case FurnitureType.BossDesk:
                notOnDoorCell = againstAWall = againstAWall = true;
                break;
            case FurnitureType.EmployeeDesk:
                againstAWall = oppositeToDoor = true;
                break;
            case FurnitureType.Canap:
                notOnDoorCell = againstAWall = true;
                break;
            case FurnitureType.GlassTable:
                notOnDoorCell = whatever = true;
                break;
            case FurnitureType.Photocopier:
                againstAWall = true;
                break;
            case FurnitureType.Toilet:
                againstAWall = oppositeToDoor = true;
                break;
            case FurnitureType.VendingMachine:
                notOnDoorCell = againstAWall = true;
                break;
            case FurnitureType.CoffeeMachine:
                againstAWall = true;
                break;
            case FurnitureType.TV:
                whatever = notOnDoorCell = true;
                break;
            default:
                break;
        }
        //si il ne faut pas que ce soit sur la case de la porte, et qu'on est sur la case de la porte ...
        if (notOnDoorCell && (cell.doorEast || cell.doorNorth || cell.doorSouth || cell.doorWest))
            return !furniturePlaced;
        //si il faut que ce soit contre un mur, et qu'on a pas de mur ...
        if (againstAWall && !cell.wallEast && !cell.wallNorth && !cell.wallSouth && !cell.wallWest)
            return !furniturePlaced;
        // si il faut que ce soit oppose à la porte et contre un mur, et qu'en face de la porte y a pas de mur ...
        if (oppositeToDoor && againstAWall && ((!cell.wallEast && cell.doorWest) || (!cell.wallWest && cell.doorEast) || (!cell.wallSouth && cell.doorNorth) || (!cell.wallNorth && cell.doorSouth)))
            return !furniturePlaced;


        if (oppositeToDoor)
        {
            //print("oppositeToDoor");
            if (cell.doorWest) possibleOrientation.Add(Orientation.East);
            else if (cell.doorEast) possibleOrientation.Add(Orientation.West);
            else if (cell.doorNorth) possibleOrientation.Add(Orientation.South);
            else if (cell.doorSouth) possibleOrientation.Add(Orientation.North);
        }
        else if (againstAWall)
        {
            //print("againstAWall");
            if (cell.wallWest) possibleOrientation.Add(Orientation.West);
            else if (cell.wallEast) possibleOrientation.Add(Orientation.East);
            else if (cell.wallNorth) possibleOrientation.Add(Orientation.North);
            else if (cell.wallSouth) possibleOrientation.Add(Orientation.South);
        }
        else if (whatever)
        {
            //print("whatever");
            possibleOrientation.Add(Orientation.West);
            possibleOrientation.Add(Orientation.East);
            possibleOrientation.Add(Orientation.North);
            possibleOrientation.Add(Orientation.South);
        }
        int rdmOrientationIndex = UnityEngine.Random.Range(0, possibleOrientation.Count); // on en tire un au hasard
        cell.furnitures.Add(new Furniture(cell.posX, cell.posY, type, possibleOrientation[rdmOrientationIndex]));

        return furniturePlaced;
    }

    public void placeFixedCorridors()
    {
        for (int i = 0; i < size; i++)
        {
            grid[i, 2].type = RoomType.Corridor;
            grid[i, 2].locked = true;
            grid[i, 6].type = RoomType.Corridor;
            grid[i, 6].locked = true;
            grid[2, i].type = RoomType.Corridor;
            grid[2, i].locked = true;
            grid[6, i].type = RoomType.Corridor;
            grid[6, i].locked = true;
        }

        grid[0, 2].wallWest = false;
        grid[0, 6].wallWest = false;
        grid[8, 2].wallEast = false;
        grid[8, 6].wallEast = false;
        grid[2, 0].wallNorth = false;
        grid[6, 0].wallNorth = false;
        grid[2, 8].wallSouth = false;
        grid[6, 8].wallSouth = false;
    }

    public bool placingRoomFurnitures(List<FurnitureType> roomFurnitures, Room room)
    {
        List<int> randomIndexBagForFurnitures = new List<int>(); // on cree une liste nous permettant de tirer aleatoirement un index dans la liste des fournitures
        for (int i = 0; i < roomFurnitures.Count; i++) randomIndexBagForFurnitures.Add(i); // on la remplie 


        //print(room.type + " has " + roomFurnitures.Count + " furnitures to place");
        int nbFurnituresPlaced = 0;

        for (int i = 0; i < roomFurnitures.Count; i++) // pour chacun des meuble de la liste
        {
            int rdmFurnitureBagIndex = UnityEngine.Random.Range(0, randomIndexBagForFurnitures.Count); // on tire au hasard un index dans la liste des index
            int rdmFurnitureIndex = randomIndexBagForFurnitures[rdmFurnitureBagIndex];  // on recupere l'index de fourniture correspondant
            randomIndexBagForFurnitures.RemoveAt(rdmFurnitureBagIndex); // on enleve cette index de la liste en question

            List<int> randomIndexBagForCells = new List<int>(); // on cree une liste nous permettant de tirer aleatoirement un index dans la liste des cases 
            for (int j = 0; j < room.cells.Count; j++) randomIndexBagForCells.Add(j); // on la remplie

            bool furniturePlaced = false;// boolean vrai si le meuble a ete place
            // creer une classe pour ce genre de truc ?
            while (!furniturePlaced && randomIndexBagForCells.Count > 0) // tant que le meuble n'a pas ete place  ou qu'il y a encore des cases a essayer
            {

                int rdmCellBagIndex = UnityEngine.Random.Range(0, randomIndexBagForCells.Count); // on tire un index au hasard dans la liste des index
                int rdmCellIndex = randomIndexBagForCells[rdmCellBagIndex]; // on recupere l'index de case correspondant
                randomIndexBagForCells.RemoveAt(rdmCellBagIndex); // on retire l'index en question de la liste des index
                furniturePlaced = placingFurniture(room.cells[rdmCellIndex], roomFurnitures[rdmFurnitureIndex]);
                room.cells[rdmCellIndex].locked = true;




                //if (furniturePlaced) print(roomFurnitures[rdmFurnitureIndex] + " successfully placed in cell" + room.cells[rdmCellIndex].posX + ":" + room.cells[rdmCellIndex].posY);
                //else print(" failed to place " + roomFurnitures[rdmFurnitureIndex] + "in cell" + room.cells[rdmCellIndex].posX + ":" + room.cells[rdmCellIndex].posY);
            }
            if (furniturePlaced) nbFurnituresPlaced++;
        }
        if (nbFurnituresPlaced == roomFurnitures.Count) return true;
        else return false;
    }

    public void placingCoffeeRoomFurniture(Room room)
    {
        List<FurnitureType> coffeeRoomFurnitures = new List<FurnitureType>();
        for (int i = 0; i < nbCoffeeMachine; i++)
            coffeeRoomFurnitures.Add(FurnitureType.CoffeeMachine);
        //for (int i = 0; i < nbVendingMachine; i++)         
        coffeeRoomFurnitures.Add(FurnitureType.VendingMachine);
        for (int i = 0; i < nbTV; i++)
            coffeeRoomFurnitures.Add(FurnitureType.TV);

        placingRoomFurnitures(coffeeRoomFurnitures, room);

    }
    
    public void placingBossRoomFurniture(Room room)
    {
        List<FurnitureType> bossRoomFurnitures = new List<FurnitureType>();
        Furniture carpet = new Furniture(room.cells[0].posX, room.cells[0].posY, FurnitureType.Carpet, Orientation.North);
        if (room.width == 3)
        {
            carpet.flip = true;
            carpet.orientation = Orientation.West;
        }
        room.cells[0].furnitures.Add(carpet);

        bossRoomFurnitures.Add(FurnitureType.Casier);
        bossRoomFurnitures.Add(FurnitureType.FlowerPot);
       // bossRoomFurnitures.Add(FurnitureType.GlassTable);
        bossRoomFurnitures.Add(FurnitureType.Canap);
        bossRoomFurnitures.Add(FurnitureType.BossDesk);

        placingRoomFurnitures(bossRoomFurnitures, room);

    }

    public void placingBoxFurniture(Room room)
    {
        List<FurnitureType> boxFurnitures = new List<FurnitureType>();

        boxFurnitures.Add(FurnitureType.EmployeeDesk);

        placingRoomFurnitures(boxFurnitures, room);
    }

    public void placingBathroomFurniture(Room room)
    {
        List<FurnitureType> bathroomFurnitures = new List<FurnitureType>();

        bathroomFurnitures.Add(FurnitureType.Toilet);

        placingRoomFurnitures(bathroomFurnitures, room);
    }

    public void placingCorridorFurniture(Room room)
    {
        List<FurnitureType> corridorFurnitures = new List<FurnitureType>();
        for (int i = 0; i < nbPhotocopier; i++)
            corridorFurnitures.Add(FurnitureType.Photocopier);
        for (int i = 0; i < nbVendingMachine; i++)
            corridorFurnitures.Add(FurnitureType.VendingMachine);
        for (int i = 0; i < nbCasier; i++)
            corridorFurnitures.Add(FurnitureType.Casier);
        for (int i = 0; i < nbFlowerPot; i++)
            corridorFurnitures.Add(FurnitureType.FlowerPot);

        placingRoomFurnitures(corridorFurnitures, room);
    }





    private void Create3DCell(int x, int z)
    {
        GameObject newCell = Instantiate(floorPrefab) as GameObject;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(x , 0f, z);
        if (z == 0 && (x == 2 || x == 6))
        {
            GameObject newNorthWindow = Instantiate(windowPrefab) as GameObject;
            newNorthWindow.name = "NorthWindow " + x + ", " + z;
            newNorthWindow.transform.parent = transform;
            newNorthWindow.transform.Translate(new Vector3(x , 0f, z));
            obstacles.Add(newNorthWindow);
        }
        if (z == 8 && (x == 2 || x == 6))
        {
            GameObject newSouthWindow = Instantiate(windowPrefab) as GameObject;
            newSouthWindow.name = "SouthWindow " + x + ", " + z;
            newSouthWindow.transform.parent = transform;

            newSouthWindow.transform.Translate(new Vector3(x , 0f, z));
            //------------------------
            newSouthWindow.transform.Rotate(0, 180, 0);
            //-------------------
            obstacles.Add(newSouthWindow);

        }
        if (x == 0 && (z == 2 || z == 6))
        {
            GameObject newWestWindow = Instantiate(windowPrefab) as GameObject;
            newWestWindow.name = "WestWindow " + x + ", " + z;
            newWestWindow.transform.parent = transform;

            newWestWindow.transform.Translate(new Vector3(x , 0f, z));
            //------------------------
            newWestWindow.transform.Rotate(0, 90, 0);
            //-------------------
            obstacles.Add(newWestWindow);
        }
        if (x == 8 && (z == 2 || z == 6))
        {
            GameObject newEastWindow = Instantiate(windowPrefab) as GameObject;
            newEastWindow.name = "EastWindow " + x + ", " + z;
            newEastWindow.transform.parent = transform;

            newEastWindow.transform.Translate(new Vector3(x , 0f, z));
            //------------------------
            newEastWindow.transform.Rotate(0, -90, 0);
            //-------------------
            obstacles.Add(newEastWindow);
        }

        //newCell.transform.Rotate(0,180,0);

        switch (grid[x, z].type)
        {
            case RoomType.Corridor:
                newCell.name = "Corridor Cell " + x + ", " + z;
                newCell.GetComponent<Renderer>().material = materials[0];
                Color color =                newCell.GetComponent<Renderer>().material.color;
                float H=0, S=0, V=0;
                ColorConverter.RGBToHSV(color,out H,out S,out V);
                float rdm = UnityEngine.Random.Range(-0.02f,0.02f);
                V = V + rdm;
                if (V > 1) V = 1;
                Color noiseColor = ColorConverter.HSVToRGB(H, S, V);
                newCell.GetComponent<Renderer>().material.color = noiseColor;
                //print("H : " + H + " ,S :" + S + " ,V: " + V);

                break;
            case RoomType.Bossroom:
                newCell.name = "BossRoom Cell " + x + ", " + z;
                newCell.GetComponent<Renderer>().material = materials[1];
                break;
            case RoomType.Coffeeroom:
                newCell.name = "CoffeeRoom Cell " + x + ", " + z;
                newCell.GetComponent<Renderer>().material = materials[2];
                break;
            case RoomType.Bathroom:
                newCell.name = "Bathroom Cell " + x + ", " + z;
                newCell.GetComponent<Renderer>().material = materials[3];
                break;
            case RoomType.Box:
                newCell.name = "Box Cell " + x + ", " + z;
                newCell.GetComponent<Renderer>().material = materials[4];
                break;
            case RoomType.Elevator:
                newCell.name = "Elevator Cell " + x + ", " + z;
                newCell.GetComponent<Renderer>().material = materials[5];
                break;
        }

        obstacles.Add(newCell);
        if (grid[x, z].wallNorth)
        {
            GameObject newNorthWall = Instantiate(WallPrefab) as GameObject;
            newNorthWall.name = "NorthWall " + x + ", " + z;
            newNorthWall.transform.parent = transform;
            newNorthWall.transform.Translate(new Vector3(x , 0f, z));

            obstacles.Add(newNorthWall);
        }
        else if (grid[x, z].doorNorth && grid[x, z].type != RoomType.Box)
        {
            GameObject newNorthDoor = Instantiate(DoorPrefab) as GameObject;
            newNorthDoor.name = "NorthDoor " + x + ", " + z;
            newNorthDoor.transform.parent = transform;
            newNorthDoor.transform.Translate(new Vector3(x , 0f, z));

            obstacles.Add(newNorthDoor);
        }
        if (grid[x, z].wallSouth)
        {
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
            newSouthDoor.transform.Translate(new Vector3(x , 0f, z));
            //------------------------
            newSouthDoor.transform.Rotate(0, 180, 0);
            //-------------------
            obstacles.Add(newSouthDoor);
        }
        if (grid[x, z].wallEast)
        {
            //GameObject newEastWall = Instantiate(EastWallPrefab) as GameObject;
            GameObject newEastWall = Instantiate(WallPrefab) as GameObject;
            newEastWall.name = "EastWall " + x + ", " + z;
            newEastWall.transform.parent = transform;

            newEastWall.transform.Translate(new Vector3(x , 0f, z));
            //------------------------
            newEastWall.transform.Rotate(0, -90, 0);
            //-------------------
            obstacles.Add(newEastWall);
        }
        else if (grid[x, z].doorEast && grid[x, z].type != RoomType.Box)
        {
            GameObject newEastDoor = Instantiate(DoorPrefab) as GameObject;
            newEastDoor.name = "EastDoor " + x + ", " + z;
            newEastDoor.transform.parent = transform;
            newEastDoor.transform.Translate(new Vector3(x , 0f, z));
            //------------------------
            newEastDoor.transform.Rotate(0, -90, 0);
            //-------------------
            obstacles.Add(newEastDoor);
        }
        if (grid[x, z].wallWest)
        {
            GameObject newWestWall = Instantiate(WallPrefab) as GameObject;
            newWestWall.name = "WestWall " + x + ", " + z;
            newWestWall.transform.parent = transform;
            newWestWall.transform.Translate(new Vector3(x , 0f, z));
            //------------------------
            newWestWall.transform.Rotate(0, 90, 0);
            //-------------------
            obstacles.Add(newWestWall);
        }
        else if (grid[x, z].doorWest && grid[x, z].type != RoomType.Box)
        {
            GameObject newWestDoor = Instantiate(DoorPrefab) as GameObject;
            newWestDoor.name = "WestDoor " + x + ", " + z;
            newWestDoor.transform.parent = transform;
            newWestDoor.transform.Translate(new Vector3(x , 0f, z));

            //------------------------
            newWestDoor.transform.Rotate(0, 90, 0);
            //-------------------
            obstacles.Add(newWestDoor);
        }
        foreach (Furniture furniture in grid[x, z].furnitures)
        {
            GameObject newFurniture = null;
            string name;
            switch (furniture.type)
            {
                case FurnitureType.Casier:
                    if (UnityEngine.Random.Range(0, 2) == 0) newFurniture = Instantiate(casier0Prefab) as GameObject;
                    else newFurniture = Instantiate(casier1Prefab) as GameObject;
                    name = "Casier" + x + "," + z;
                    break;
                case FurnitureType.GlassTable:
                    newFurniture = Instantiate(glassTablePrefab) as GameObject;
                    name = "GlassTable" + x + "," + z;
                    break;
                case FurnitureType.FlowerPot:
                    newFurniture = Instantiate(flowerPotPrefab) as GameObject;
                    name = "FlowerPot" + x + "," + z;
                    break;
                case FurnitureType.Carpet:
                    newFurniture = Instantiate(carpetPrefab) as GameObject;
                    name = "Carpet" + x + "," + z;
                    break;
                case FurnitureType.BossDesk:
                    newFurniture = Instantiate(bossDeskPrefab) as GameObject;
                    name = "BossDesk" + x + "," + z;
                    break;
                case FurnitureType.Canap:
                    newFurniture = Instantiate(canapPrefab) as GameObject;
                    name = "Canap" + x + "," + z;
                    break;
                case FurnitureType.CoffeeMachine:
                    newFurniture = Instantiate(coffeeMachinePrefab) as GameObject;
                    name = "CoffeeMachine" + x + "," + z;
                    break;
                case FurnitureType.EmployeeDesk:
                    newFurniture = Instantiate(employeeDeskPrefab) as GameObject;
                    name = "EmployeeDesk" + x + "," + z;
                    break;
                case FurnitureType.Photocopier:
                    newFurniture = Instantiate(photocopierPrefab) as GameObject;
                    name = "Photocopier" + x + "," + z;
                    break;
                case FurnitureType.Toilet:
                    newFurniture = Instantiate(toiletPrefab) as GameObject;
                    name = "Toilet" + x + "," + z;
                    break;
                case FurnitureType.TV:
                    newFurniture = Instantiate(tvPrefab) as GameObject;
                    name = "TV" + x + "," + z;
                    break;
                case FurnitureType.VendingMachine:
                    newFurniture = Instantiate(vendingMachinePrefab) as GameObject;
                    name = "VendingMachine" + x + "," + z;
                    break;
            }
            newFurniture.transform.parent = transform;
            newFurniture.transform.Translate(new Vector3(x , 0f, z));

            //------------------------
            newFurniture.transform.Rotate(0, furniture.orientationToDegree(), furniture.flipToDegree());
            //-------------------
            obstacles.Add(newFurniture);
        }

    }

    public int tryToRandomlyPlaceXRooms(int X, int width, int height, RoomType type)
    {
        int nbSuccess = 0;
        bool success;
        for (int i = 0; i < X; i++)
        {
            success = placeRoom(width, height, type);
            if (success)
            {
                nbSuccess++;
                //print("success : " + type);
            }
            else
            {
               //print("defeat : "+type);
            }
        }
        return nbSuccess;
    }

    public void createCorridorRoom()
    {
        Room corridorsRoom = new Room(RoomType.Corridor);
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                if (grid[i, j].type == RoomType.Corridor) corridorsRoom.cells.Add(grid[i, j]);

        rooms.Add(corridorsRoom);
    }

    public void createElevatorRoom()
    {
        fillArea(4, 0, 1, 1, RoomType.Elevator, true); // on pose la salle
        rooms.Add(new Room(4, 0, 1, 1, RoomType.Elevator, this.grid));

        grid[4, 1].locked = true;
        grid[4, 0].wallSouth = false;
        grid[4, 0].doorSouth = true;
        grid[4, 1].wallNorth = false;
    }

    public bool placeRoom(int width, int height, RoomType type)
    {
        bool roomForTheRoom = false;
        List<Vector2> possiblePlacesForRoom = new List<Vector2>();
        for (int i = 0; i < size; i++)// on parcours l'intégralité de la grille
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
            rooms.Add(new Room(xRoom, yRoom, width, height, type, grid));
            // print("xRoom : " + xRoom + ", yRoom : " + yRoom);

            List<Vector2> possiblePlacesForDoor = new List<Vector2>(); // maintenant on va chercher la porte, on sait qu'il y a au moins un emplacement
            for (int i = xRoom; i < xRoom + width; i++) // parcours au dessus et en dessous
            {
                if (yRoom > 0 && grid[i, yRoom - 1].type == RoomType.Corridor) // si on trouve une case corridor autour de la salle
                    possiblePlacesForDoor.Add(new Vector2(i, yRoom - 1)); // on l'ajoute aux emplacements possibles pour la porte
                if (yRoom + height < size && grid[i, yRoom + height].type == RoomType.Corridor)
                    possiblePlacesForDoor.Add(new Vector2(i, yRoom + height));
            }
            for (int i = yRoom; i < yRoom + height; i++)// parcours a droite a gauche
            {
                if (xRoom > 0 && grid[xRoom - 1, i].type == RoomType.Corridor)
                    possiblePlacesForDoor.Add(new Vector2(xRoom - 1, i));
                if (xRoom + width < size && grid[xRoom + width, i].type == RoomType.Corridor)
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
            grid[xDoor, yDoor].locked = true; // on verrouille le corridor pour en tenir compte dans les collisions futures et eviter de boucher le passage

            if (xDoor == xRoom + width)
            { //si la porte se trouve a l'est
                grid[xDoor - 1, yDoor].wallEast = false;// on enleve le mur est de sa case ouest
                grid[xDoor - 1, yDoor].doorEast = true;// on ajoute la porte de sa case ouest

                grid[xDoor, yDoor].wallWest = false;// on enleve son mur ouest, pas propre, pour mobilier couloir, a revoir

            }
            if (xDoor < xRoom)
            { //si la porte se trouve a l'ouest
                grid[xDoor + 1, yDoor].wallWest = false;// on enleve le mur ouest de sa case est
                grid[xDoor + 1, yDoor].doorWest = true;// on ajoute la porte ouest de sa case est

                grid[xDoor, yDoor].wallEast = false;// on enleve son mur ouest, pas propre, pour mobilier couloir, a revoir
            }
            if (yDoor == yRoom + height)
            { //si la porte se trouve au sud
                grid[xDoor, yDoor - 1].wallSouth = false;// on enleve le mur sud de sa case nord
                grid[xDoor, yDoor - 1].doorSouth = true;// on ajoute la porte sud de sa case nord

                grid[xDoor, yDoor].wallNorth = false;// on enleve son mur ouest, pas propre, pour mobilier couloir, a revoir

            }
            if (yDoor < yRoom)
            { //si la porte se trouve au nord
                grid[xDoor, yDoor + 1].wallNorth = false;// on enleve le mur nord de sa case sud
                grid[xDoor, yDoor + 1].doorNorth = true;// on ajoute la porte nord de sa case sud

                grid[xDoor, yDoor].wallSouth = false;// on enleve son mur ouest, pas propre, pour mobilier couloir, a revoir
            }
            roomForTheRoom = true;
        }
        else
        {
            roomForTheRoom = false;
        }
        return roomForTheRoom;
    }



    public bool collision(int x, int y, int width, int height)
    {
        bool collision = true;
        if (!placeAlreadyTaken(x, y, width, height) && doorCanBePlaced(x, y, width, height) && !breakCorridorsConnexion(x, y, width, height))
            collision = false;

        return collision;

    }

    // renvoie vrai si la place est deja occupe par une case de type autre que corridor
    public bool placeAlreadyTaken(int x, int y, int width, int height)
    {
        bool placeAlreadyTaken = false;
        if (x >= 0 && y >= 0 && x + width <= size && y + height <= size)
        { // si notre salle ne depasse pas des limites de la grille
            //on parcours l'ensemble des cases qu'elle occupera
            for (int i = x; i < x + width; i++)
                for (int j = y; j < y + height; j++)
                    if (grid[i, j].type != RoomType.Corridor || grid[i, j].locked) // et si on trouve une case non corridor, ou corridor verrouillee ( pour laisser le passage )
                        placeAlreadyTaken = true; // alors la place est prise
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
            if (y > 0 && grid[i, y - 1].type == RoomType.Corridor) // si on trouve au moins une case corridor autour de la salle
                doorCanBePlaced = true;
            if (y + height < size && grid[i, y + height].type == RoomType.Corridor)
                doorCanBePlaced = true;
        }
        for (int i = y; i < y + height; i++)
        {
            if (x > 0 && grid[x - 1, i].type == RoomType.Corridor)
                doorCanBePlaced = true;
            if (x + width < size && grid[x + width, i].type == RoomType.Corridor)
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
        if (x > 0 && grid[x - 1, y].type == RoomType.Corridor && !grid[x - 1, y].check) // voisin de gauche
            crawlCorridorsCells(x - 1, y);
        if (y < size - 1 && grid[x, y + 1].type == RoomType.Corridor && !grid[x, y + 1].check) // voisin du bas
            crawlCorridorsCells(x, y + 1);
        if (y > 0 && grid[x, y - 1].type == RoomType.Corridor && !grid[x, y - 1].check) // voisin du haut
            crawlCorridorsCells(x, y - 1);
    }

    // remplie une zone de la grille avec un type de case
    public void fillArea(int x, int y, int width, int height, RoomType type, bool addTheWall)
    {

        for (int i = x; i < x + width; i++)
            for (int j = y; j < y + height; j++)
            {
                //grid [i, j] = new Cell (this, i, j, RoomType);
                grid[i, j].type = type;

                if (addTheWall)
                { // si on veut ajouter les murs de la salle

                    if (i == x)
                    {
                        grid[i, j].wallWest = true; // si le mur est sur le cote Ouest
                        if (i > 0) grid[i - 1, j].wallEast = true; // pas propre,necessaire pour le mobilier de couloir, a enlever plus tard ( peut etre :s )
                    }
                    if (i == x + width - 1)
                    {
                        grid[i, j].wallEast = true; // si le mur est sur le cote Est
                        if (i < size - 1) grid[i + 1, j].wallWest = true;
                    }
                    if (j == y)
                    {
                        grid[i, j].wallNorth = true; // si le mur est sur le cote Nord
                        if (j > 0) grid[i, j - 1].wallSouth = true;
                    }
                    if (j == y + height - 1)
                    {
                        grid[i, j].wallSouth = true; // si le mur est sur le cote Sud
                        if (j < size - 1) grid[i, j + 1].wallNorth = true;
                    }


                }// end if
            }//end for
        if (addTheWall)
        {

            //GetComponent<Triggers>().addTrigger(x, y, width, height, type);

        }

    }

}
