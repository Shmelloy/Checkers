using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMovementHandlerNetwork : PieceMovementHandler
{
    public override void OnStartAuthority()
    {
        TilesSelectionHandler.OnTileSelected += HandleTileSelected;
    }

    public override void OnStopAuthority()
    {
        TilesSelectionHandler.OnTileSelected -= HandleTileSelected;
    }

    protected override void Move(Vector3 position, bool nextTurn)
    {
        MovePiece(position, nextTurn);
    }

    [Command]
    void MovePiece(Vector3 position, bool nextTurn)
    {
        base.Move(position, nextTurn);
    }

    protected override void Capture(Vector2Int piecePosition)
    {
        CapturePiece(piecePosition);
    }

    [Command]
    void CapturePiece(Vector2Int piecePosition)
    {
        base.Capture(piecePosition);
    }

}
