using UnityEngine;


namespace Sokoban
{
    
    public class FielGenerator : MonoBehaviour
    {
        public int width = 10;
        public int height = 10;
        public int[,] levelArray;

        void Start()
        {
            GenerateLevel();
        }

        void GenerateLevel()
        {
            levelArray = new int[width, height];

            // Initialize the array with floor tiles
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    levelArray[x, y] = 0;
                }
            }

            // Randomly place walls
            for (int i = 0; i < width * height / 8; i++)
            {
                int x = Random.Range(1, width - 1);
                int y = Random.Range(1, height - 1);

                levelArray[x, y] = 1;
            }

            // Randomly place boxes and targets
            for (int i = 0; i < width * height / 4; i++)
            {
                int x = Random.Range(1, width - 1);
                int y = Random.Range(1, height - 1);

                if (Random.Range(0, 2) == 0)
                {
                    levelArray[x, y] = 2;
                }
                else
                {
                    levelArray[x, y] = 3;
                }
            }
        }

    }

}