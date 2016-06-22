using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class PlayerIntention
{
    public List<PlayerInput> _intention;

    public PlayerIntention()
    {
        _intention = new List<PlayerInput>();
    }

    public PlayerIntention(List<PlayerInput> intention)
    {
        _intention = intention;
    }
}
