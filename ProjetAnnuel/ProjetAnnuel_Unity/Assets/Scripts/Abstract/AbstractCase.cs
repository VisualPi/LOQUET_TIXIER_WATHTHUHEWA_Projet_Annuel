using UnityEngine;
using System.Collections;
//public enum ECaseType { DEPART, FIN, BLUE, RED, GREEN, YELLOW }
public abstract class AbstractCase : MonoBehaviour
{
    [SerializeField]
    private AbstractCase _previousCase;
    [SerializeField]
    private AbstractCase _nextCase;
    [SerializeField]
    private MeshRenderer _meshRenderer;
    [SerializeField]
    private Transform _transform;

    private int _nbPlayerOnCase;

    [SerializeField]
    private Vector3 _alternativePosition1; //Les positions alternative sont en remplacements de la position de la case
    [SerializeField]
    private Vector3 _alternativePosition2; //Elles sont utilisées si plusieur personnages sont sur la meme cases
    [SerializeField]
    private Vector3 _alternativePosition3; //dans ce cas le premier sera sur la position1, le deuxieme sur la position2 etc...
    [SerializeField]
    private Vector3 _alternativePosition4; //TODO: les mettre en serialize pour le setter directement sur le prefab


    public AbstractCase GetNextCase()
    {
        return _nextCase;
    }
    public void SetNextCase(AbstractCase value)
    {
        _nextCase = value;
    }
    public AbstractCase GetPreviousCase()
    {
        return _previousCase;
    }
    public void SetPreviousCase(AbstractCase value)
    {
        _previousCase = value;
    }
    public MeshRenderer GetMeshRenderer()
    {
        return _meshRenderer;
    }
    public Transform GetTransform()
    {
        return _transform;
    }
    public int GetNbPlayerOnCase()
    {
        return _nbPlayerOnCase;
    }
    public void SetNbPlayerOnCase(int value)
    {
        _nbPlayerOnCase = value;
    }
    public Vector3 GetAlternatePosition1()
    {
        return _alternativePosition1;
    }
    public Vector3 GetAlternatePosition2()
    {
        return _alternativePosition2;
    }
    public Vector3 GetAlternatePosition3()
    {
        return _alternativePosition3;
    }
    public Vector3 GetAlternatePosition4()
    {
        return _alternativePosition4;
    }

    public Vector3 GetPositionForPlayer()//Retourne la position ou le joueur doit etre sur la case
    {
        switch(_nbPlayerOnCase)
        {
            case 0:
                return this.transform.position;
            case 1:
                return _alternativePosition2;
            case 2:
                return _alternativePosition3;
            case 3:
                return _alternativePosition4;
            default:
                return Vector3.zero;
        }
    }

}
