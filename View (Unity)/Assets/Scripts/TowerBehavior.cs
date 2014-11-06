using UnityEngine;
using System.Collections.Generic;

using Laska;

public class TowerBehavior : MonoBehaviour {

    public GameBoard GameBoard;

    public Board Board
    {
        get
        {
            return GameBoard.Board;
        }
    }

    public GameObject HighlightPrefab;

    private readonly List<GameObject> highlights = new List<GameObject>();


    void OnMouseDown()
    {
        GameBoard.Clicked(gameObject);
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
        var actions = Board.GetValidActions((byte)transform.position.x, (byte)transform.position.y);
        foreach (var action in actions)
        {
            var position = action.End;
            if (HighlightPrefab)
            {
                GameObject highlight = Instantiate(HighlightPrefab) as GameObject;
                highlight.transform.parent = transform;
                highlight.name = "Highlight";
                highlight.SendMessage("SetAction", action, SendMessageOptions.DontRequireReceiver);
                highlight.SendMessage("SetGameBoard", GameBoard, SendMessageOptions.DontRequireReceiver);
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
