using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgorithms
{
   public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength) 
   {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        path.Add(startPosition);
        var prevPosition = startPosition;

        for(int i = 0; i < walkLength; i++)
        {
            var newPosition = prevPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            prevPosition = newPosition;
        }
        return path;
   }
}

public static class Direction2D
{
    public static List<Vector2Int> cardinalDirections = new List<Vector2Int>
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirections[Random.Range(0, cardinalDirections.Count)];
    }
}