using UnityEngine;
using System.Collections.Generic;

using Laska;

public class GameBoard : MonoBehaviour {

    public GameObject WhiteSoldier;
    public GameObject WhiteGeneral;
    public GameObject BlackSoldier;
    public GameObject BlackGeneral;

    public GameObject HighlightPrefab;

    private readonly Dictionary<char, GameObject> prefabs = new Dictionary<char, GameObject>();

    public static GameObject SelectedTower;

    private Board board;

    void Start()
    {
        prefabs.Add(Token.WHITE_SOLDIER, WhiteSoldier);
        prefabs.Add(Token.WHITE_GENERAL, WhiteGeneral);
        prefabs.Add(Token.BLACK_SOLDIER, BlackSoldier);
        prefabs.Add(Token.BLACK_GENERAL, BlackGeneral);
        Board.Log = Debug.Log;
        board = new Board(TokenColor.WHITE, Board.DEFAULT);
        for (byte i = 0; i < 7; i++)
        {
            for (byte j = 0; j < 7; j++)
            {
                Position position = new Position();
                position.BCol = i;
                position.BRow = j;
                CreateTower(board[position]);
            }
        }
    }

    public void CreateTower(Tower tower)
    {
        if (tower == null)
        {
            return;
        }
        string name = tower.ToString();
        GameObject parent = new GameObject("Tower (" + name + ")");
        foreach (char c in name)
        {
            GameObject gameObject = Instantiate(prefabs[c]) as GameObject;
            TowerBehavior towerBehavior = gameObject.GetComponent<TowerBehavior>();
            towerBehavior.Board = board;
            towerBehavior.HighlightPrefab = HighlightPrefab;
            gameObject.name = c.ToString();
            gameObject.transform.parent = parent.transform;
            gameObject.transform.position = new Vector2(tower.Position.BCol, tower.Position.BRow);
        }
    }
}
