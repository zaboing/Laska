using UnityEngine;
using System.Collections.Generic;

using Laska;

public class GameBoard : MonoBehaviour {

    public const string TestBoard = "w,w,,,/,b,,/w,b,,,/b,,w/,w,,w,b/b,b,b,/b,b,b,b,/";

    public GameObject WhiteSoldier;
    public GameObject WhiteGeneral;
    public GameObject BlackSoldier;
    public GameObject BlackGeneral;

    public GameObject HighlightPrefab;

    private readonly Dictionary<char, GameObject> prefabs = new Dictionary<char, GameObject>();

    public static GameObject SelectedTower;

    public Board Board;

    void Start()
    {
        Build();
    }

    public void Build()
    {
        prefabs.Clear();
        prefabs.Add(Token.WHITE_SOLDIER, WhiteSoldier);
        prefabs.Add(Token.WHITE_GENERAL, WhiteGeneral);
        prefabs.Add(Token.BLACK_SOLDIER, BlackSoldier);
        prefabs.Add(Token.BLACK_GENERAL, BlackGeneral);
        foreach (var gameObject in GameObject.FindGameObjectsWithTag("Tower"))
        {
            Destroy(gameObject);
        }
        Board.Log = Debug.Log;
        if (Board == null)
        {
            Board = new Board(TokenColor.WHITE, TestBoard);
        }
        for (byte i = 0; i < 7; i++)
        {
            for (byte j = 0; j < 7; j++)
            {
                Position position = new Position();
                position.BCol = i;
                position.BRow = j;
                CreateTower(Board[position]);
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
        parent.tag = "Tower";
        float offsetZ = 0;
        bool first = true;
        for (int i = name.Length - 1; i >= 0; i--)
        {
            char c = name[i];
            GameObject gameObject = Instantiate(prefabs[c]) as GameObject;
            gameObject.name = c.ToString();
            gameObject.transform.parent = parent.transform;
            Vector3 position = new Vector3();
            position.x = tower.Position.BCol;
            position.y = tower.Position.BRow;
            position.z = offsetZ;
            gameObject.transform.position = position;
            offsetZ -= .2f;
            if (first)
            {
                TowerBehavior towerBehavior = gameObject.AddComponent<TowerBehavior>();
                towerBehavior.GameBoard = this;
                towerBehavior.HighlightPrefab = HighlightPrefab;
                if (Board.LockedPosition.HasValue && Board.LockedPosition.Value.Equals(tower.Position))
                {
                    Clicked(gameObject, false);
                }
            }
        }
    }

    public void Clicked(GameObject tower)
    {
        Clicked(tower, true);
    }

    private void Clicked(GameObject tower, bool checkLock)
    {
        if (checkLock && Board.LockedPosition.HasValue)
        {
            return;
        }
        else
        {
            if (SelectedTower)
            {
                SelectedTower.SendMessage("Unselect");
            }
            SelectedTower = tower;
            tower.SendMessage("Select");
        }
    }

    void OnGUI()
    {
        GUI.enabled = Board.LockedPosition.HasValue;
        if (GUI.Button(new Rect(30, Screen.height / 2 - 10, 100, 20), "Next Turn"))
        {
            Board.ChangeTurns();
        }
    }
}
