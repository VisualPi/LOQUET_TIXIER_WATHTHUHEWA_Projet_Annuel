using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public enum ECaseType { DEPART, FIN, BLUE, RED, GREEN, YELLOW, INTERSECTION }

public struct SPlayerCasePos
{
    public EPlayer player;
    public int casePos;//0 : centrale, 1 : alternatePos1, 2 : alternatePos2, 3 : alternatePos3, 4 : alternatePos4
}
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
    private int _caseID;
    [SerializeField]
    private ECaseType _caseType;

    [SerializeField]
    private Transform _alternativePosition1; //Les positions alternative sont en remplacements de la position de la case
    [SerializeField]
    private Transform _alternativePosition2; //Elles sont utilisées si plusieur personnages sont sur la meme cases
    [SerializeField]
    private Transform _alternativePosition3; //dans ce cas le premier sera sur la position1, le deuxieme sur la position2 etc...
    [SerializeField]
    private Transform _alternativePosition4; //TODO: les mettre en serialize pour le setter directement sur le prefab

    private List<SPlayerCasePos> _playersOnCase;

    void Start()
    {
        switch (_caseType)
        {
            case ECaseType.DEPART:
                _meshRenderer.material = Utils.Instance.purple;
                break;
            case ECaseType.FIN:
                _meshRenderer.material = Utils.Instance.black;
                break;
            case ECaseType.BLUE:
                _meshRenderer.material = Utils.Instance.blue;
                break;
            case ECaseType.RED:
                _meshRenderer.material = Utils.Instance.red;
                break;
            case ECaseType.GREEN:
                _meshRenderer.material = Utils.Instance.green;
                break;
            case ECaseType.YELLOW:
                _meshRenderer.material = Utils.Instance.yellow;
                break;
        }
        if (_caseType != ECaseType.DEPART)
            _nbPlayerOnCase = 0;
        else
            _nbPlayerOnCase = 4;
    }

    private int GetNoneOccupiedPos()
    {
        int i = 1;
        while (i < 5)
        {
            if (_playersOnCase.Any(f => f.casePos == i))
                i++;
            else
                return i;
        }
        return -1;
    }

    private Vector3 GetPositionByID(int value)
    {
        switch (value)
        {
            case 0:
                return _transform.position;
            case 1:
                return GetAlternatePosition1();
            case 2:
                return GetAlternatePosition2();
            case 3:
                return GetAlternatePosition3();
            case 4:
                return GetAlternatePosition4();
            default:
                return Vector3.zero;
        }
    }

    void Awake()
    {
        _playersOnCase = new List<SPlayerCasePos>();
    }

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
    public int GetCaseID()
    {
        return _caseID;
    }
    public int GetNextCaseID()
    {
        return GetNextCase().GetCaseID();
    }
    public void SetCaseID(int value)
    {
        _caseID = value;
    }
    public Vector3 GetAlternatePosition1()
    {
        return _alternativePosition1.position;
    }
    public Vector3 GetAlternatePosition2()
    {
        return _alternativePosition2.position;
    }
    public Vector3 GetAlternatePosition3()
    {
        return _alternativePosition3.position;
    }
    public Vector3 GetAlternatePosition4()
    {
        return _alternativePosition4.position;
    }

    public ECaseType GetCaseType()
    {
        return _caseType;
    }
    public void SetCaseType(ECaseType type)
    {
        _caseType = type;
    }
    public Vector3 GetCasePosition(EPlayer currentPlayer, bool isReturningFromMiniGame)
    {
        if (_previousCase && !isReturningFromMiniGame) //exclu la case départ
        {
            if (_previousCase.GetCaseType() != ECaseType.INTERSECTION)
                _previousCase.RemovePlayerFromCase(currentPlayer);//on supprime le player actuelle de la liste de la case d'avant (sauf si c'est la case de départ)
        }
        if (_caseType == ECaseType.INTERSECTION)
            return _transform.position;
        if (_playersOnCase.Count == 0)//si jamais la case est vide on ajoute dans la liste le joueur a la position 0 et on retourne la position de la case (position centrale)
        {
            _playersOnCase.Add(new SPlayerCasePos() { player = currentPlayer, casePos = 0 });//on pourrait assigner casePos avec la fonction GetNoneOccupiedPos mais bon
            return _transform.position;
        }
        else if (_playersOnCase.Count == 1)//s'il y a une personne dessus (en pos centrale) on la déplace avant de donner la position du nouveau joueur a mettre sur la case
        {
            var p = _playersOnCase[0].player;
            _playersOnCase[0] = new SPlayerCasePos() { player = p, casePos = 1 };
            Utils.Instance.GetPlayerByColor(_playersOnCase[0].player).transform.position = GetPositionByID(_playersOnCase[0].casePos);
        }
        var i = GetNoneOccupiedPos();//on prend l'id d'une position alternative pas déja prise
        _playersOnCase.Add(new SPlayerCasePos() { player = currentPlayer, casePos = i });//on ajoute le joueur courant a la position retournée par la fonction
        return GetPositionByID(i);//on retourne cette position pour le mouvement dans le GameManager
    }
    public void RemovePlayerFromCase(EPlayer player)
    {
        try
        {
            var index = _playersOnCase.FindIndex(f => f.player == player);
            _playersOnCase.RemoveAt(index);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }

    }
    public abstract void ApplyEffect(int playerID);

}
