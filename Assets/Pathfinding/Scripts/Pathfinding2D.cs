using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding2D : MonoBehaviour
{
    public List<Vector3> Path = new List<Vector3>();
    public bool JS = false;

    public void FindPath(Vector3 startPosition, Vector3 endPosition)
    {
        Pathfinder2D.Instance.InsertInQueue(startPosition, endPosition, SetList);
    }

  

    //A test move function, can easily be replaced
    public void Move()
    {
        if (Path.Count > 0)
        {

			print("PQTHTHTH :" +Path[0]);

			transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y), Path[0], Time.deltaTime * 300F);

			//transform.position = Vector3.MoveTowards(transform.position, new Vector3(10,10,-10), Time.deltaTime * 30F);

            if (Vector3.Distance(transform.position, Path[0]) < 0.4F)
            {
				print("REMOVE " );

                Path.RemoveAt(0);
            }
        }
    }

    protected virtual void SetList(List<Vector3> path)
    {
        if (path == null)
        {
            return;
        }

      
            Path.Clear();
            Path = path;
            Path[0] = new Vector3(Path[0].x, Path[0].y);
            Path[Path.Count - 1] = new Vector3(Path[Path.Count - 1].x, Path[Path.Count - 1].y);

    }
}
