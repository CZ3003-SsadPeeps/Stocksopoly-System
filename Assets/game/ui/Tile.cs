﻿using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileType Type;
    private readonly Piece[] pieceSlots = new Piece[4];

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.tag.Contains("Player")) return;

        Piece currentPiece = other.GetComponentInParent<Piece>();
        for (int i = 0; i < pieceSlots.Length; i++)
        {
            if (pieceSlots[i] != currentPiece) continue;

            pieceSlots[i] = null;
            break;
        }
    }

    internal Vector3 GetEmptySlotForPiece(Piece piece)
    {
        Vector3 pos = gameObject.transform.position;

        for (int i = 0; i < pieceSlots.Length; i++)
        {
            if (pieceSlots[i] != null) continue;

            pieceSlots[i] = piece;
            // Account for possible collision with other pieces on the tile
            switch (i)
            {
                case 0:
                    pos.Set(pos.x + 0.2f, 0, pos.z + 0.2f);
                    break;
                case 1:
                    pos.Set(pos.x + 0.2f, 0, pos.z - 0.2f);
                    break;
                case 2:
                    pos.Set(pos.x - 0.2f, 0, pos.z + 0.2f);
                    break;
                default:
                    pos.Set(pos.x - 0.2f, 0, pos.z - 0.2f);
                    break;
            }

            return pos;
        }

        return pos;
    }
}
