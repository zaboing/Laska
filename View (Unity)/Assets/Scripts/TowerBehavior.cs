using UnityEngine;
using System.Collections.Generic;

using Laska;

public class TowerBehavior : MonoBehaviour {
    public Board Board;

    public GameObject HighlightPrefab;

    private readonly List<GameObject> highlights = new List<GameObject>();

    void OnMouseDown()
    {
        if (GameBoard.SelectedTower != null)
        {
            GameBoard.SelectedTower.SendMessage("Unselect");
        }
        GameBoard.SelectedTower = gameObject;
        Select();
    }

    public void Select()
    {
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            var child = transform.GetChild(i);
            if (child.name == "Selection")
            {
                child.gameObject.SetActive(true);
            }
        }
        List<Position> actions = Board.GetValidActions((byte)transform.position.x, (byte)transform.position.y);
        foreach (Position position in actions)
        {
            Debug.Log(position.Col + " " + position.Row);
            if (HighlightPrefab)
            {
                GameObject highlight = Instantiate(HighlightPrefab) as GameObject;
                highlight.transform.parent = transform;
                highlight.name = "Highlight";
                highlight.transform.position = new Vector2(position.BCol, position.BRow) + new Vector2(.5f, .5f);
                highlights.Add(highlight);
            }
        }
    }

    public void Unselect()
    {
        foreach (GameObject highlight in highlights)
        {
            Destroy(highlight);
        }
        highlights.Clear();
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            var child = transform.GetChild(i);
            if (child.name == "Selection")
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
