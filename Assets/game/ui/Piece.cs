using UnityEngine;

///<summary> Class of the pieces of Stocksopoly
///<br></br>
///Done by: Yi Shen
///</summary>

public class Piece : MonoBehaviour
{
    ///<summary>Number of laps that the piece has done</summary>
    public int NumLaps { get; private set; } = 0;

    ///<summary>Position of the piece on the board</summary>
    public int BoardPos { get; private set; } = 0;

    ///<summary>Updates the position of the piece and increments NumLaps if the piece completes a lap around the board</summary>
    internal void UpdateBoardPos(int numTilesMoved)
    {
        int prevBoardPos = BoardPos;
        BoardPos = (BoardPos + numTilesMoved) % 12;
        if (BoardPos < prevBoardPos) NumLaps++;
    }
}
