using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class MazeGenerator : MonoBehaviour
{
    public GameObject[] tiles;

    const int N = 1;
    const int E = 2;
    const int S = 4;
    const int W = 8;

    Dictionary<Vector2, int> cell_walls = new Dictionary<Vector2, int>();

    public float tile_size = 10;
    public int width = 10;   // Width of map  
    public int height = 10;  // Height of map
    public GameObject coin;
    public GameObject player;

    List<List<int>> map = new List<List<int>>();


    // Start is called before the first frame update
    void Start()
    {
        cell_walls[new Vector2(0, -1)] = N;
        cell_walls[new Vector2(1, 0)] = E;
        cell_walls[new Vector2(0, 1)] = S;
        cell_walls[new Vector2(-1, 0)] = W;

	/*
	if (GameObject.FindWithTag("Player") == null)
	{
		GameObject p = GameObject.Instantiate(player);
		p.transform.position = new Vector3(UnityEngine.Random.Range
				(1,width)*tile_size, 1.0f, 
				UnityEngine.Random.Range(1,height)*tile_size);
	}
	*/

        MakeMaze();

	GameObject c = GameObject.Instantiate(coin);
	c.transform.position = new Vector3(UnityEngine.Random.Range(1,width)
			*tile_size, 1.0f, 
			UnityEngine.Random.Range(1,height)*tile_size);
    }

    private List<Vector2> CheckNeighbors(Vector2 cell, List<Vector2> unvisited) {
        // Returns a list of cell's unvisited neighbors
        List<Vector2> list = new List<Vector2>();

        foreach (var n in cell_walls.Keys)
        {
            if (unvisited.IndexOf((cell + n)) != -1) { 
                list.Add(cell+ n);
            }
                    
        }
        return list;
    }


    private void MakeMaze()
    {
        List<Vector2> unvisited = new List<Vector2>();
        List<Vector2> stack = new List<Vector2>();

        // Fill the map with #15 tiles
        for (int i = 0; i < width; i++)
        {
            map.Add(new List<int>());
            for (int j = 0; j < height; j++)
            {
                map[i].Add(N | E | S | W);
                unvisited.Add(new Vector2(i, j));
            }

        }

        Vector2 current = new Vector2(0, 0);

        unvisited.Remove(current);

        while (unvisited.Count > 0) {
            List<Vector2> neighbors = CheckNeighbors(current, unvisited);

            if (neighbors.Count > 0)
            {
                Vector2 next = neighbors[UnityEngine.Random.Range(0, neighbors.Count)];
                stack.Add(current);

                Vector2 dir = next - current;

                int current_walls = map[(int)current.x][(int)current.y] - cell_walls[dir];

                int next_walls = map[(int)next.x][(int)next.y] - cell_walls[-dir];

                map[(int)current.x][(int)current.y] = current_walls;

                map[(int)next.x][(int)next.y] = next_walls;

                current = next;
                unvisited.Remove(current);

            }
            else if (stack.Count > 0) { 
                current = stack[stack.Count - 1];
                stack.RemoveAt(stack.Count - 1);
            
            }

            
        }

        for (int i = 0; i < width; i++)
        {
            
            for (int j = 0; j < height; j++)
            {
                GameObject tile = GameObject.Instantiate(tiles[map[i][j]]);
                tile.transform.parent = gameObject.transform;

                tile.transform.Translate(new Vector3 (j*tile_size, 0, i * tile_size));
                tile.name += " " + i.ToString() + ' ' + j.ToString(); 
		tile.GetComponentInChildren<NavMeshSurface>().BuildNavMesh();
            }

        }

    }
}
