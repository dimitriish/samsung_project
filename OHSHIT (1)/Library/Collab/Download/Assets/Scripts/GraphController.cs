using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphController : MonoBehaviour
{

    public List<GameObject> roadNodes = new List<GameObject>();

    public List<List<int>> nodesRelative = new List<List<int>>();

    public Dictionary<string, List<GameObject>> roads = new Dictionary<string, List<GameObject>>();

    public List<MicroChelAgent> chels = new List<MicroChelAgent>();

    // Start is called before the first frame update
    void Start()
    {
        nodesRelative.Add(new List<int> {4});
        nodesRelative.Add(new List<int> {4, 5, 7});
        nodesRelative.Add(new List<int> {3, 4});
        nodesRelative.Add(new List<int> {2,5});
        nodesRelative.Add(new List<int> {0, 1, 2});
        nodesRelative.Add(new List<int> {1, 3, 6});
        nodesRelative.Add(new List<int> {5});
        nodesRelative.Add(new List<int> {1, 8, 9});
        nodesRelative.Add(new List<int> {7});
        nodesRelative.Add(new List<int> {7});


        for(int i = 0; i<roadNodes.Count; i++)
        {
            for(int j = 0; j<roadNodes.Count; j++)
            {
                if (i != j)
                {
                    if (nodesRelative[i].Contains(j))
                    {
                        if(!roads.ContainsKey(j + "-" + i))
                        {
                            roads.Add(i + "-" + j, new List<GameObject>());
                        }
                    }
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
