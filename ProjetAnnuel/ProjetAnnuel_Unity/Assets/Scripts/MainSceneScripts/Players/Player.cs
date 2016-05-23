using UnityEngine;
using System.Collections;

public enum EPlayer { BLUE = 0, GREEN, RED, YELLOW};

public class Player : MonoBehaviour
{
    [SerializeField]
    private bool _isAI;
    [SerializeField]
    private EPlayer _playerColor;

    public void SetIsAI(bool value)
    {
        _isAI = value;
    }
    public bool GetIsAI()
    {
        return _isAI;
    }
    public EPlayer GetPlayerColor()
    {
        return _playerColor;
    }

}
