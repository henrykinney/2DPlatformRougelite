using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System;

public class LevelGenerator : MonoBehaviour
{
    //public GameObject[] Rooms;
    [SerializeField]
    public RoomGenConfig[] Rooms;
    public int generateSeed;
    public Vector2Int MapSize;
    // Start is called before the first frame update
    void Start()
    {
        Generate(generateSeed);
    }

    int ChooseRandomDirection(bool[] PossibleDirections)
    {
        int numDirections = 0;
        for (int i = 0; i<PossibleDirections.Length; i++) {
            if (PossibleDirections[i] == true) {
                numDirections++;
            }
        }
        int Choosen = UnityEngine.Random.Range(1, numDirections + 1);

        for (int i = 0; i<PossibleDirections.Length; i++) {
            if (PossibleDirections[i] == true) {
                Choosen--;
                if (Choosen == 0) {
                    return i;
                }
            }
        }
        Debug.LogError("All direction values are false!");
        return -1;
    }

    RoomGenConfig RandomRoomWithDirections(bool[] Directions, string requiredTag) {
        //RoomGenConfig[] PossibleRooms = {};
        List<RoomGenConfig> PossibleRooms = new List<RoomGenConfig>();
        // conditionally add stuff to this list and return it
        for (int i = 0; i<Rooms.Length; i++) {
            if (Directions[0] == Rooms[i].BranchUp && Directions[1] == Rooms[i].BranchRight && Directions[2] == Rooms[i].BranchDown && Directions[3] == Rooms[i].BranchLeft) {
                if (requiredTag == Rooms[i].Tag) {
                    PossibleRooms.Add(Rooms[i]);
                }
            }

        }
        if (PossibleRooms.Count == 0) { // I literally forgot this if statement so that's why i was getting false error messages, it's here now.
            Debug.LogError("There are no rooms with tag " + requiredTag + ", with directions, Up: " + Directions[0] + ", Right: " + Directions[1] + ", Down: " + Directions[2] + ", Left: " + Directions[3]);
        }
        Assert.AreNotEqual(0, PossibleRooms.Count);
        int ChoosenRoom = UnityEngine.Random.Range(0, PossibleRooms.Count);
        return PossibleRooms[ChoosenRoom];
        
    }

    public void Generate(int seed)
    {
        UnityEngine.Random.InitState(seed);
        /*
        for (int x = 0; x<MapSize.x; x++) {
            for (int y = 0; y<MapSize.y; y++) {
                GameObject newobj = Instantiate(Rooms[Random.Range(0, Rooms.Length)].RoomToSpawn);
                newobj.transform.position = new Vector3(x * 10, y * -10, 0);
                newobj.transform.SetParent(gameObject.transform);
                newobj.name = "Room " + x + "," + y;
            }
        }
        */

        List<List<bool[]>> RoomMap = new List<List<bool[]>>();

        for (int xMap = 0; xMap<MapSize.x; xMap++) {
            RoomMap.Add(new List<bool[]>());
            for (int yMap = 0; yMap<MapSize.y; yMap++) {
                RoomMap[xMap].Add(new bool[] {false, false, false, false});
            }
        }
        print("Initialization complete!");


        int x = UnityEngine.Random.Range(0, MapSize.x - 1);
        int y = 0;
        int Startx = x;
        int Starty = y;
        /*
        bool[] StartingRoomDirections = new bool[] {false, false, false, false};
        GameObject startingnewobj = Instantiate(RandomRoomWithDirections(StartingRoomDirections).RoomToSpawn);
        startingnewobj.transform.position = new Vector3(x * 10, 0, 0);
        startingnewobj.transform.SetParent(gameObject.transform);
        startingnewobj.name = "Room " + x + "," + y;
        */
        RoomMap[x][0] = new bool[] {false, false, false, false};
        print("x: " + x + "y: " + y);

        
        int GenDirection = 0;
        while (true) {
            
            
            

            bool[] PossibleDirections = new bool[] {false, GenDirection != -1, true, GenDirection != 1}; //Up, Right, Down, Left
            if (x == 0) {
                PossibleDirections[3] = false;
            }
            if (x == MapSize.x - 1) {
                PossibleDirections[1] = false;
            }
            
            int ChoosenDirection = ChooseRandomDirection(PossibleDirections);
            print("Direction: " + ChoosenDirection);
            RoomMap[x][y][ChoosenDirection] = true;
            switch (ChoosenDirection) {
                case 0:
                    y -= 1;
                    break;
                case 1:
                    x += 1;
                    GenDirection = 1;
                    break;
                case 2:
                    y += 1;
                    GenDirection = 0;
                    break;
                case 3:
                    x -= 1;
                    GenDirection = -1;
                    break;
                default:
                    break;
            }
            if (y >= MapSize.y) {
                break;
            }
            
            RoomMap[x][y] = new bool[] {ChoosenDirection == 2, ChoosenDirection == 3, ChoosenDirection == 0, ChoosenDirection == 1};
            print("x: " + x + "y: " + y);
        }
        for (int xGen = 0; xGen<MapSize.x; xGen++) {
            for (int yGen = 0; yGen<MapSize.y; yGen++) {
                if (RoomMap[xGen][yGen] != null && !(xGen == Startx && yGen == Starty)) {
                    int d = UnityEngine.Random.Range(0, 4);
                    switch (d) {
                        case 0:
                            if (yGen == 0 || (xGen == Startx && yGen - 1 == Starty)) {
                                break;
                            }
                            RoomMap[xGen][yGen][0] = true;
                            RoomMap[xGen][yGen - 1][2] = true;
                            break;
                        case 1:
                            if (xGen == MapSize.x - 1 || (xGen + 1 == Startx && yGen == Starty)) {
                                break;
                            }
                            RoomMap[xGen][yGen][1] = true;
                            RoomMap[xGen + 1][yGen][3] = true;
                            break;
                        case 2:
                            if (yGen == MapSize.y - 1 || (xGen == Startx && yGen + 1 == Starty)) {
                                break;
                            }
                            RoomMap[xGen][yGen][2] = true;
                            RoomMap[xGen][yGen + 1][0] = true;
                            break;
                        case 3:
                            if (xGen == 0 || (xGen - 1 == Startx && yGen == Starty)) {
                                break;
                            }
                            RoomMap[xGen][yGen][3] = true;
                            RoomMap[xGen - 1][yGen][1] = true;
                            break;
                    }
                }
            }
        }
        
        for (int xGen = 0; xGen<MapSize.x; xGen++) {
            for (int yGen = 0; yGen<MapSize.y; yGen++) {
                //print("x: " + xGen + "y: " + yGen + "Room is: " + RoomMap[xGen][yGen]);
                string reqTag = "";
                Debug.Log(x + ", " + y);
                if (xGen == Startx && yGen == Starty) {
                    reqTag = "Start";
                }
                if (xGen == x && yGen == y - 1) {
                    reqTag = "End";
                }
                if (RoomMap[xGen][yGen] != null) {
                    GameObject startingnewobj = Instantiate(RandomRoomWithDirections(RoomMap[xGen][yGen], reqTag).RoomToSpawn);
                    startingnewobj.transform.position = new Vector3(xGen * 10, yGen * -10, 0);
                    startingnewobj.transform.SetParent(gameObject.transform);
                    startingnewobj.name = "Room " + xGen + "," + yGen;
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
