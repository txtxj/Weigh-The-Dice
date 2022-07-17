using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMapFromFile : MonoBehaviour
{
    public GameObject dice;
    public GameObject[] gameAsserts;
    public GameObject winMenu;
    public GameObject dieMenu;
    
    [HideInInspector]
    public int assetsNumber;

    private TextAsset levelText;
    private TextAsset rotateTex;
    private Vector2Int size;
    private Vector2Int ori;

    private void CreateBlock(int x, int y, int index, int forward)
    {
        Quaternion q = Quaternion.identity;
        switch (forward)
        {
            case 0:
                q = Quaternion.Euler(0f, 0f, 0f);
                break;
            case 1:
                q = Quaternion.Euler(0f, 180f, 0f);
                break;
            case 2:
                q = Quaternion.Euler(0f, 90f, 0f);
                break;
            case 3:
                q = Quaternion.Euler(0f, 270f, 0f);
                break;
        }
        MapInfo.tiles[x, y] = Instantiate(gameAsserts[index], new Vector3(x, 0f, y) * 2, q, transform);
        if (MapInfo.IsTarget(x, y))
        {
            MapInfo.tiles[x, y].GetComponent<Renderer>().materials[1].SetColor("_Color", (Color)new Color32(0xff, 0x2c, 0x60, 0xff));
        }
        else if (MapInfo.IsWater(x, y))
        {
            MapInfo.tiles[x, y].GetComponent<Renderer>().materials[0].SetColor("_Color", (Color)new Color32(0x64, 0xb4, 0xff, 0xff));
        }
    }

    private void BuildMap(byte[] s, byte[] r)
    {
        size = new Vector2Int(s[1], s[0]);
        ori = new Vector2Int(s[2], s[3]);
        dice.GetComponent<Dice>().pos = ori;
        dice.GetComponent<Dice>().isLocalPlayer = true;
        MapInfo.target = new Vector2Int(r[0], r[1]);
        MapInfo.mapSize = size;
        MapInfo.mapInfo = new int[size.y, size.x];
        MapInfo.rotation = new int[size.y, size.x];
        MapInfo.hole = new int[size.y, size.x];
        MapInfo.tiles = new GameObject[size.y, size.x];
        for (int i = 0; i < size.y; i++)
        {
            for (int j = 0; j < size.x; j++)
            {
                int u = s[i * size.x + j + 4];
                int v = r[i * size.x + j + 2];
                if (u < assetsNumber)
                {
                    MapInfo.mapInfo[i, j] = u;
                    MapInfo.rotation[i, j] = v;
                    if (u == 0 || u == 3 || u == 6 || u == 9 || u == 11 || u == 13)
                    {
                        MapInfo.hole[i, j] = 1;
                    }
                    else
                    {
                        MapInfo.hole[i, j] = 0;
                    }
                    CreateBlock(i, j, u, v);
                }
                else
                {
                    MapInfo.mapInfo[i, j] = 15;
                    MapInfo.hole[i, j] = 2;
                }
            }
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(1);
    }

    public void Next()
    {
        GameObject levelTransfer = GameObject.Find("LevelObject");
        levelTransfer.GetComponent<LevelData>().id += 1;
        SceneManager.LoadScene(1);
    }

    private void Awake()
    {
        assetsNumber = gameAsserts.Length;
        EventHandler.winMenu = winMenu;
        EventHandler.dieMenu = dieMenu;
        Dice.ResetFlag();
        GameObject levelTransfer = GameObject.Find("LevelObject");
        int level = 99;
        if (levelTransfer != null)
        {
            level = levelTransfer.GetComponent<LevelData>().id;
        }
        levelText = Resources.Load<TextAsset>("Levels/" + level + "/level");
        rotateTex = Resources.Load<TextAsset>("Levels/" + level + "/rotate");
        if (levelText == null)
        {
            SceneManager.LoadScene(0);
            return;
        }
        BuildMap(levelText.bytes, rotateTex.bytes);
    }
}
