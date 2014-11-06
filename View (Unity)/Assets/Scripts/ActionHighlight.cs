using UnityEngine;

using Laska;

public class ActionHighlight : MonoBehaviour {

    private Action action;
    private GameBoard gameBoard;

    public void SetAction(Action action)
    {
        this.action = action;
    }

    public void SetGameBoard(GameBoard gameBoard)
    {
        this.gameBoard = gameBoard;
    }

    void OnMouseDown()
    {
        action.Perform(gameBoard.Board);
        gameBoard.Build();
    }
}
