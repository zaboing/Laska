using UnityEngine;

using Laska;

public class ActionHighlight : MonoBehaviour {

    private Move move;
    private GameBoard gameBoard;

    public void SetAction(Move move)
    {
        this.move = move;
    }

    public void SetGameBoard(GameBoard gameBoard)
    {
        this.gameBoard = gameBoard;
    }

    void OnMouseDown()
    {
        gameBoard.Board = gameBoard.Board.doMove(move);
        gameBoard.Build();
    }
}
