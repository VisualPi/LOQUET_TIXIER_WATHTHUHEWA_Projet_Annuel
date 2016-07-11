using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class PathScript : MonoBehaviour 
{
    [SerializeField]
    bool _enableGizmos;

    [SerializeField]
    float _gizmosRadius;

    public List<CheckPointScript> _path;

   
    void Update()
    {
       
        
    }

    void OnDrawGizmos()
    {
        if (_enableGizmos)
        {
            int listCount = _path.Count;

            if (listCount > 1)
            {
                for (int i = 0; i < (listCount - 1); ++i)
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine(_path[i].transform.position, _path[i + 1].transform.position);

                    Gizmos.color = Color.green;
                    Gizmos.DrawWireSphere(_path[i].transform.position, _gizmosRadius);
                }

                Gizmos.color = Color.white;
                Gizmos.DrawLine(_path[listCount - 1].transform.position, _path[0].transform.position);

                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(_path[listCount - 1].transform.position, _gizmosRadius);
            }
        }
    }
}
