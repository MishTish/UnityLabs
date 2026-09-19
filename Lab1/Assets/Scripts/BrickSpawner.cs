using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    public GameObject regularBrick;
    public GameObject sturdyBrick;
    public GameObject bonusBrick;

    public int columns = 10;
    public int rows = 10;
    public float spacingX = 1.11f;
    public float spacingY = 1f;
    public Vector2 startPos = new Vector2(-5f, 8f); // adjust to your wall coordinates

    void Start()
    {
        int[,] currentLevelData = LevelData.AllLevels[GameManager.CurrentLevelNumber - 1];
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            { 

                int brickType = currentLevelData[row, col];
                if (brickType == 0)
                {
                    continue;
                }
                Vector2 pos = startPos + new Vector2(col * spacingX, -row * spacingY);
                GameObject prefabToSpawn = ChooseBrickType(brickType);
                Instantiate(prefabToSpawn, pos, Quaternion.identity, transform);
                GameManager.Instance.brickCount++;
            }
        }
    }
    
    GameObject ChooseBrickType(int brickType)
    {
        switch (brickType)
        {
            case 1:
                return regularBrick;
            case 2:
                return sturdyBrick;
            case 3:
                return bonusBrick;
            
        }
        return null;
    }
}