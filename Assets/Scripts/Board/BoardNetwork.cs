using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardNetwork : Board
{
    readonly SyncList<int[]> boardListSyn = new SyncList<int[]>();
    public override IList<int[]> BoardList
    {
        get { return boardListSyn; }

    }

    public override void OnStartServer()
    {
        FillBoardList(boardListSyn);
    }

    [Server]
    public override void MoveOnBoard(Vector2Int oldPosition, Vector2Int newPosition, bool nextTurn)
    {
        base.MoveOnBoard( oldPosition, newPosition, nextTurn);
        MoveOnBoard(BoardList, oldPosition, newPosition);
        MoveOnBoardClient(oldPosition, newPosition, nextTurn);
    }

    [ClientRpc]
    void MoveOnBoardClient(Vector2Int oldPosition, Vector2Int newPosition, bool nextTurn)
    {
        if (!NetworkServer.active)
        {
            MoveOnBoard(BoardList, oldPosition, newPosition);
            if (nextTurn)
            {
                NetworkClient.connection.identity.GetComponent<PlayerNetwork>().NextTurn();
            }
        }
    }
}
