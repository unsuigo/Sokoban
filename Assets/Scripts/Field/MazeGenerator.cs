using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MazeGenerator : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public int width;
    public int height;
    public int minRoomSize;
    public int maxSplits;
    public int roomCount; //number of rooms to generate
    
    private int[,] maze;
    private Room[] rooms;

    private void Start()
    {
        GenerateMaze();
    }

    public void GenerateMaze() {
        maze = new int[width, height];

        // Initialize all cells as walls
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                maze[x, y] = 1;
            }
        }

        // Use BSP to generate the maze
        // SplitMaze(0, 0, width, height, maxSplits);
        
        // Use Recusive method to generate the maze
        // RecursiveBacktracking(Random.Range(0, width), Random.Range(0, height));
        
        //Generate Rooms
        rooms = new Room[roomCount];
        for (int i = 0; i < roomCount; i++)
        {
            int roomWidth = Random.Range(3, width / 2);
            int roomHeight = Random.Range(3, height / 2);
            int roomX = Random.Range(1, width - roomWidth - 1);
            int roomY = Random.Range(1, height - roomHeight - 1);

            rooms[i] = new Room(roomX, roomY, roomWidth, roomHeight);
            for (int x = roomX; x < roomX + roomWidth; x++)
            {
                for (int y = roomY; y < roomY + roomHeight; y++)
                {
                    maze[x, y] = 0;
                }
            }
        }

        for (int i = 0; i < roomCount; i++)
        {
            int nextRoom = Random.Range(i + 1, roomCount);
            int x = Random.Range(rooms[i].x + 1, rooms[i].x + rooms[i].width - 1);
            int y = Random.Range(rooms[i].y + 1, rooms[i].y + rooms[i].height - 1);
            while (x != rooms[nextRoom].x)
            {
                maze[x, y] = 0;
                if (x < rooms[nextRoom].x)
                {
                    x++;
                }
                else
                {
                    x--;
                }
            }
            while (y != rooms[nextRoom].y)
            {
                maze[x, y] = 0;
                if (y < rooms[nextRoom].y)
                {
                    y++;
                }
                else
                {
                    y--;
                }
            }
        }
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (maze[x, y] == 0)
                {
                    Instantiate(floorPrefab, new Vector3(x, y, 0), Quaternion.identity);
                }
                else
                {
                    Instantiate(wallPrefab, new Vector3(x, y, 0), Quaternion.identity);
                }
            }
        }
    }
    
    
    private void RecursiveBacktracking(int x, int y)
    {
        maze[x, y] = 0;

        while (true)
        {
            var neighbors = new Vector2[] {
                new Vector2(x + 1, y),
                new Vector2(x - 1, y),
                new Vector2(x, y + 1),
                new Vector2(x, y - 1)
            };
            
            
            var shuffledNeighbors = neighbors.OrderBy(n => Random.value).ToArray();

            var nextCellVisited = true;
            foreach (var next in shuffledNeighbors)
            {
                if (next.x > 0 && next.x < width && next.y > 0 && next.y < height && maze[(int)next.x, (int)next.y] != 0)
                {
                    RecursiveBacktracking((int)next.x, (int)next.y);
                    nextCellVisited = false;
                    break;
                }
            }
            if (nextCellVisited)
                break;
        }
    }

    private void SplitMaze(int x, int y, int w, int h, int splits) {
        if (splits <= 0 || w < minRoomSize || h < minRoomSize) {
            CreateRoom(x, y, w, h);
            return;
        }

        // Randomly choose to split horizontally or vertically
        if (Random.Range(0, 2) == 0) {
            int splitPos = Random.Range(minRoomSize, h - minRoomSize);
            CreateWall(x + w / 2, y, splitPos, true);
            SplitMaze(x, y, w / 2, splitPos, splits - 1);
            SplitMaze(x, y + splitPos, w / 2, h - splitPos, splits - 1);
        } else {
            int splitPos = Random.Range(minRoomSize, w - minRoomSize);
            CreateWall(x, y + h / 2, splitPos, false);
            SplitMaze(x, y, splitPos, h / 2, splits - 1);
            SplitMaze(x + splitPos, y, w - splitPos, h / 2, splits - 1);
        }
    }
    
    private void CreateRoom(int x, int y, int w, int h) {
        for (int i = x; i < x + w; i++) {
            for (int j = y; j < y + h; j++) {
                maze[i, j] = 0;
                Instantiate(floorPrefab, new Vector3(i, j, 0), Quaternion.identity);
            }
        }
    }

    private void CreateWall(int x, int y, int len, bool isVertical) {
        for (int i = 0; i < len; i++) {
            if (isVertical) {
                maze[x, y + i] = 1;
                Instantiate(wallPrefab, new Vector3(x, y + i, 0), Quaternion.identity);
            } else {
                maze[x + i, y] = 1;
                Instantiate(wallPrefab, new Vector3(x + i, y, 0), Quaternion.identity);
            }
        }
    }

}
        
