using UnityEngine;
using System.Collections.Generic;

public class PatternManager : MonoBehaviour
{
	private Vector2[][] _logicalCases;

	int minX = 0;
	int maxX = 45;
	int minZ = -45;
	int maxZ = 0;

	void Start()
	{
		_logicalCases = new Vector2[10][];
		for( var i = 0 ; i < 10 ; i++ )
			_logicalCases[i] = new Vector2[10];
		for( var i = 0 ; i < 10 ; i++ )
			for( var j = 0 ; j < 10 ; j++ )
				_logicalCases[i][j] = new Vector2(i * 5, -j * 5);

		for( var y = 0 ; y < 10 ; y++ )
		{
			for( var x = 0 ; x < 10 ; x++ )
			{
				Debug.Log(_logicalCases[x][y]);
			}
		}
	}

	public List<Vector2> GenerateNewPattern( Vector2 pos, int forceH, int forceW )//force = largeur du pattern
	{
		var idx = GetLogicalCoord(pos);
		if(Random.Range(0, 20) > 10) //random pour la priorité (priorité = si le joueur va préféré commencer un pattern vertical ou horizontal)
		{
			if( pos.x + ( ( forceW + 1 ) * 5f ) <= maxX ) //si il y a la place pour un pattern qui a tant de case sur la droite
			{
				if( pos.y + ( ( forceH + 1 ) * 5f ) <= maxZ )//et vers le haut
				{
					return GeneratePatternHPriority(idx[0], idx[1], forceH, forceW, Vector3.up, Vector3.right);
				}
				if( pos.y - ( ( forceH + 1 ) * 5f ) >= minZ )//et/ou vers le bas
				{
					return GeneratePatternHPriority(idx[0], idx[1], forceH, forceW, Vector3.down, Vector3.right);
				}
			}
			if( pos.x - ( ( forceW + 1 ) * 5f ) >= minX )//si il y a la place pour un pattern qui a tant de case sur la gauche
			{
				if( pos.y + ( ( forceH + 1 ) * 5f ) <= maxZ )//et vers le haut
				{
					return GeneratePatternHPriority(idx[0], idx[1], forceH, forceW, Vector3.up, Vector3.left);
				}
				if( pos.y - ( ( forceH + 1 ) * 5f ) >= minZ )//et/ou vers le bas
				{
					return GeneratePatternHPriority(idx[0], idx[1], forceH, forceW, Vector3.down, Vector3.left);
				}
			}
		}
		else
		{
			if( pos.y + ( ( forceH + 1 ) * 5f ) <= maxZ ) //si il y a la place pour un pattern qui a tant de case vers le haut
			{
				if( pos.x + ( ( forceW + 1 ) * 5f ) <= maxX )//et vers la droite
				{
					return GeneratePatternVPriority(idx[0], idx[1], forceH, forceW, Vector3.up, Vector3.right);
				}
				if( pos.x - ( ( forceW + 1 ) * 5f ) >= minX )//et/ou vers la gauche
				{
					return GeneratePatternVPriority(idx[0], idx[1], forceH, forceW, Vector3.up, Vector3.left);
				}
			}
			if( pos.y - ( ( forceH + 1 ) * 5f ) >= minZ )//si il y a la place pour un pattern qui a tant de case vers le bas
			{
				if( pos.x + ( ( forceW + 1 ) * 5f ) <= maxX )//et vers la droite
				{
					return GeneratePatternVPriority(idx[0], idx[1], forceH, forceW, Vector3.down, Vector3.right);
				}
				if( pos.x - ( ( forceW + 1 ) * 5f ) >= minX )//et/ou vers la gauche
				{
					return GeneratePatternVPriority(idx[0], idx[1], forceH, forceW, Vector3.down, Vector3.left);
				}
			}
		}


		
		return null;
	}

	private List<Vector2> GeneratePatternHPriority( int x, int y, int forceH, int forceW, Vector3 vertical, Vector3 horizontal ) //horizontal priority
	{
		var ret = new List<Vector2>();
		if( horizontal == Vector3.right && vertical == Vector3.up )
		{
			for( var i = x ; i < ( x + ( forceW + 1 ) ) ; i++ )
			{
				ret.Add(_logicalCases[i][y]);
			}//GOOD
			for( var i = y ; i > ( y - ( forceH + 1 ) ) ; i-- )
			{
				ret.Add(_logicalCases[x + ( forceW + 1 )][i]);
			}//GOOD
			for( var i = x + ( forceW + 1 ) ; i > x ; i-- )
			{
				ret.Add(_logicalCases[i][y - ( forceH + 1 )]);
			}//GOOD
			for( var i = y - ( forceH + 1 ) ; i < y ; i++ )
			{
				ret.Add(_logicalCases[x][i]);
			}//GOOD
			return ret;
		}
		else if( horizontal == Vector3.right && vertical == Vector3.down )
		{
			for( var i = x ; i < ( x + ( forceW + 1 ) ) ; i++ )
			{
				ret.Add(_logicalCases[i][y]);
			}//GOOD
			for( var i = y ; i < ( y + ( forceH + 1 ) ) ; i++ )
			{
				ret.Add(_logicalCases[x + ( forceW + 1 )][i]);
			}//GOOD
			for( var i = x + ( forceW + 1 ) ; i > x ; i-- )
			{
				ret.Add(_logicalCases[i][y + ( forceH + 1 )]);
			}//GOOD
			for( var i = y + ( forceH + 1 ) ; i > y ; i-- )
			{
				ret.Add(_logicalCases[x][i]);
			}//GOOD
			return ret;
		}
		else if( horizontal == Vector3.left && vertical == Vector3.up )
		{
			for( var i = x ; i > ( x - ( forceW + 1 ) ) ; i-- )
			{
				ret.Add(_logicalCases[i][y]);
			}//GOOD
			for( var i = y ; i > ( y - ( forceH + 1 ) ) ; i-- )
			{
				ret.Add(_logicalCases[x - ( forceW + 1 )][i]);
			}//GOOD
			for( var i = x - ( forceW + 1 ) ; i < x ; i++ )
			{
				ret.Add(_logicalCases[i][y - ( forceH + 1 )]);
			}//GOOD
			for( var i = y - ( forceH + 1 ) ; i < y ; i++ )
			{
				ret.Add(_logicalCases[x][i]);
			}//GOOD
			return ret;
		}
		else if( horizontal == Vector3.left && vertical == Vector3.down )
		{
			for( var i = x ; i > ( x - ( forceW + 1 ) ) ; i-- )
			{
				ret.Add(_logicalCases[i][y]);
			}//GOOD
			for( var i = y ; i < ( y + ( forceH + 1 ) ) ; i++ )
			{
				ret.Add(_logicalCases[x - ( forceW + 1 )][i]);
			}//GOOD
			for( var i = x - ( forceW + 1 ) ; i < x ; i++ )
			{
				ret.Add(_logicalCases[i][y + ( forceH + 1 )]);
			}//GOOD
			for( var i = y + ( forceH + 1 ) ; i > y ; i-- )
			{
				ret.Add(_logicalCases[x][i]);
			}//GOOD
			return ret;
		}
		return null;

	}

	private List<Vector2> GeneratePatternVPriority( int x, int y, int forceH, int forceW, Vector3 vertical, Vector3 horizontal ) //vertical priority
	{
		var ret = new List<Vector2>();
		if( horizontal == Vector3.right && vertical == Vector3.up )
		{
			for( var i = y ; i > ( y - ( forceH + 1 ) ) ; i-- )
			{
				ret.Add(_logicalCases[x][i]);
			}//GOOD
			for( var i = x ; i < ( x + ( forceW + 1 ) ) ; i++ )
			{
				ret.Add(_logicalCases[i][y - ( forceH + 1 )]);
			}//GOOD
			for( var i = y - ( forceH + 1 ) ; i < y ; i++ )
			{
				ret.Add(_logicalCases[x + ( forceW + 1 )][i]);
			}//GOOD
			for( var i = x + ( forceW + 1 ) ; i > x ; i-- )
			{
				ret.Add(_logicalCases[i][y]);
			}//GOOD
			return ret;
		}
		else if( horizontal == Vector3.left && vertical == Vector3.up )
		{
			for( var i = y ; i > ( y - ( forceH + 1 ) ) ; i-- )
			{
				ret.Add(_logicalCases[x][i]);
			}//GOOD
			for( var i = x ; i > ( x - ( forceW + 1 ) ) ; i-- )
			{
				ret.Add(_logicalCases[i][y - ( forceH + 1 )]);
			}//GOOD
			for( var i = y - ( forceH + 1 ) ; i < y ; i++ )
			{
				ret.Add(_logicalCases[x - ( forceW + 1 )][i]);
			}//GOOD
			for( var i = x - ( forceW + 1 ) ; i < x ; i++ )
			{
				ret.Add(_logicalCases[i][y]);
			}//GOOD
			return ret;
		}
		else if( horizontal == Vector3.right && vertical == Vector3.down )
		{
			for( var i = y ; i < ( y + ( forceH + 1 ) ) ; i++ )
			{
				ret.Add(_logicalCases[x][i]);
			}//GOOD
			for( var i = x ; i < ( x + ( forceW + 1 ) ) ; i++ )
			{
				ret.Add(_logicalCases[i][y + ( forceH + 1 )]);
			}//GOOD
			for( var i = y + ( forceH + 1 ) ; i > y ; i-- )
			{
				ret.Add(_logicalCases[x + ( forceW + 1 )][i]);
			}//GOOD
			for( var i = x + ( forceW + 1 ) ; i > x ; i-- )
			{
				ret.Add(_logicalCases[i][y]);
			}//GOOD
			return ret;
		}
		else if( horizontal == Vector3.left && vertical == Vector3.down )
		{
			for( var i = y ; i < ( y + ( forceH + 1 ) ) ; i++ )
			{
				ret.Add(_logicalCases[x][i]);
			}//GOOD
			for( var i = x ; i > ( x - ( forceW + 1 ) ) ; i-- )
			{
				ret.Add(_logicalCases[i][y + ( forceH + 1 )]);
			}//GOOD
			for( var i = y + ( forceH + 1 ) ; i > y ; i-- )
			{
				ret.Add(_logicalCases[x - ( forceW + 1 )][i]);
			}//GOOD
			for( var i = x - ( forceW + 1 ) ; i < x ; i++ )
			{
				ret.Add(_logicalCases[i][y]);
			}//GOOD
			return ret;
		}
		return null;

	}

	private int[] GetLogicalCoord( Vector2 coord )
	{
		var ret = new int[2];
		for( var y = 0 ; y < 10 ; y++ )
		{
			for( var x = 0 ; x < 10 ; x++ )
			{
				if( _logicalCases[x][y].x == (int)coord.x && _logicalCases[x][y].y == (int)coord.y )
				{
					ret[0] = x;
					ret[1] = y;
					return ret;
				}
			}
		}
		return null;
	}
}
