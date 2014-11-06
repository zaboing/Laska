using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laska
{
    public class Action
    {
        public Position Start;
        public Position? Hop;
        public Position End;

        public Action(Position start, Position end)
        {
            Start = start;
            End = end;
        }

        public Action(Position start, Position hop, Position end)
        {
            Start = start;
            Hop = hop;
            End = end;
        }

        public void Perform(Board board)
        {
            if (!CanPerform(board))
            {
                return;
            }
            board[End] = board[Start];
            board[Start] = new Tower("");
            if (Hop.HasValue)
            {
                var token = board[Hop.Value].Pop();
                board[End].Append(token);
                board.LockedPosition = End;
                if (board.GetValidActions(End).Count == 0)
                {
                    board.ChangeTurns();
                }
            }
            else
            {
                board.ChangeTurns();
            }
        }

        public bool CanPerform(Board board)
        {
            var start = board[Start];
            if (start == null || start.Count() == 0)
            {
                return false;
            }
            if (start.Peek().Color != board.Turn)
            {
                return false;
            }
            var end = board[End];
            if (end != null && end.Count() != 0)
            {
                return false;
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            Action act = obj as Action;
            if (act == null)
            {
                return false;
            }
            return act.Start.Equals(Start) && act.End.Equals(End) && act.Hop.Equals(Hop);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
