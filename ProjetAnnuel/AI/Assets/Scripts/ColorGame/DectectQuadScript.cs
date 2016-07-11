using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class DectectQuadScript : MonoBehaviour 
{
    [SerializeField]
    List<GameObject> _board;

    [SerializeField]
    public Transform _cubeBottomLeft;

    [SerializeField]
    public Transform _cubeBottomLeftLeft;

    [SerializeField]
    public Transform _cubeBottomLeftUp;

    [SerializeField]
    Material _playerMaterial;

    [SerializeField]
    int _boardWidth;

    [SerializeField]
    int _boardHeight;

    //float _distanceBetweenCubeX;
    float _distanceBetweenCubeZ;

    void Start()
    {
        //_distanceBetweenCubeX = _cubeBottomLeftLeft.position.x - _cubeBottomLeft.position.x;
        _distanceBetweenCubeZ = _cubeBottomLeftUp.position.z - _cubeBottomLeft.position.z;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            List<List<int>> quadsIndex = GetQuadsIndex(DetectQuadsOnBoard(_playerMaterial));

            foreach(List<int> quadIndex in quadsIndex)
            {
                Debug.Log("Quads");

                foreach(int i in quadIndex)
                {
                    Debug.Log(_board[i].transform.position);
                }

                Debug.Log("-----------");
            }
        }
    }

    List<List<int>> GetQuadsIndex(List<List<List<GameObject>>> quads)
    {
        List<List<int>> quadsIndex = new List<List<int>>();
        List<int> quadIndex;
        foreach(List<List<GameObject>> quad in quads)
        {
            quadIndex = FindQuadIndex(quad);

            if (quadIndex.Count == 4)
                quadsIndex.Add(quadIndex);
        }

        return quadsIndex;
    }

    // Détecte les carrées sur le plateau
    List<List<List<GameObject>>> DetectQuadsOnBoard(Material playerMatrial)
    {
        List<List<List<GameObject>>> horizontalLines = new List<List<List<GameObject>>>();
        List<List<List<GameObject>>> verticalLines = new List<List<List<GameObject>>>();
        
        int index;
        
        for (int i = 0; i < _boardHeight; ++i)
        {
            index = i * _boardHeight;
            horizontalLines.Add(DetectHorizontalLines(index, playerMatrial));
        }

        for (int i = 0; i < _boardWidth; ++i)
        {
            index = i;
            verticalLines.Add(DetectVerticalLines(index, playerMatrial));
        }

        List<List<List<GameObject>>> quads = DetectQuads(horizontalLines, verticalLines);

        return quads;
    }
    // Détecte les lignes horizontales sur le plateau selon un materiel (couleur du joueur)
    List<List<GameObject>> DetectHorizontalLines(int indexBegin, Material playerMaterial)
    {
        List<List<GameObject>> horizontalLines = new List<List<GameObject>>();
        List<GameObject> horizontalLine = null;

        int index = indexBegin;
        Renderer renderer;

        for (int i = 0; i < _boardWidth; ++i)
        {
            renderer = _board[index].GetComponent<Renderer>();
            // Check si il y a bien un material
            if(renderer != null)
            {
                // Check si le material correspond bien à celui du joueur
                // Cas material différent de celui du joueur
                if(renderer.sharedMaterial != playerMaterial)
                {
                    // Cas ou on a une ligne horizontal en cours
                    if(horizontalLine != null)
                    {
                        // On ajout la ligne horizontal seulement s'il y a au moins 3 cases dans la ligne
                        // et on passe à une nouvelle ligne horizontal
                        if (horizontalLine.Count >= 3)
                        {
                            horizontalLines.Add(horizontalLine);
                            horizontalLine = null;
                        }
                        else
                        {
                            horizontalLine = null;
                        }

                    }
                }
                // Cas material identique à celui du joueur
                else
                {
                    // Cas ou on a une ligne horizontal en cours
                    // Ajout de la case à la ligne en cours
                    if (horizontalLine != null)
                    {
                        horizontalLine.Add(_board[index]);
                    }
                    // Cas nouvelle ligne horizontal
                    // on crée une nouvelle ligne et on ajout la case à la ligne
                    else
                    {
                        horizontalLine = new List<GameObject>();
                        horizontalLine.Add(_board[index]);
                    }
                }
            }

            // Incrémentation de l'index pour le parcourt du tableau (board)
            ++index;
        }

        // Cas ligne en cours non terminée sur la dernière cas traitée
        if(horizontalLine != null)
        {
            if (horizontalLine.Count >= 3)
            {
                horizontalLines.Add(horizontalLine);
            }
        }

        return horizontalLines;
    }

    // Détecte les lignes verticales sur le plateau selon un materiel (couleur du joueur)
    List<List<GameObject>> DetectVerticalLines(int indexBegin, Material playerMaterial)
    {
        List<List<GameObject>> verticalLines = new List<List<GameObject>>();
        List<GameObject> verticalLine = null;

        int index = indexBegin;
        Renderer renderer;

        for (int i = 0; i < _boardWidth; ++i)
        {
            renderer = _board[index].GetComponent<Renderer>();
            // Check si il y a bien un material
            if (renderer != null)
            {
                // Check si le material correspond bien à celui du joueur
                // Cas material différent de celui du joueur
                if (renderer.sharedMaterial != playerMaterial)
                {
                    // Cas ou on a une ligne horizontal en cours
                    if (verticalLine != null)
                    {
                        // On ajout la ligne horizontal seulement s'il y a au moins 3 cases dans la ligne
                        // et on passe à une nouvelle ligne horizontal
                        if (verticalLine.Count >= 3)
                        {
                            verticalLines.Add(verticalLine);
                            verticalLine = null;
                        }
                        else
                        {
                            verticalLine = null;
                        }

                    }
                }
                // Cas material identique à celui du joueur
                else
                {
                    // Cas ou on a une ligne horizontal en cours
                    // Ajout de la case à la ligne en cours
                    if (verticalLine != null)
                    {
                        verticalLine.Add(_board[index]);
                    }
                    // Cas nouvelle ligne horizontal
                    // on crée une nouvelle ligne et on ajout la case à la ligne
                    else
                    {
                        verticalLine = new List<GameObject>();
                        verticalLine.Add(_board[index]);
                    }
                }
            }

            // Incrémentation de l'index pour le parcourt du tableau (board)
            index += _boardWidth;
        }
        
        // Cas ligne en cours non terminée sur la dernière cas traitée
        if (verticalLine != null)
        {
            if (verticalLine.Count >= 3)
            {
                verticalLines.Add(verticalLine);
            }
        }

        return verticalLines;
    }

    // Detecte les carrées selon les lignes verticales et les lignes horizontales sur le plateau
    List<List<List<GameObject>>> DetectQuads(List<List<List<GameObject>>> horizontalLines, List<List<List<GameObject>>> verticalLines)
    {
        List<List<List<GameObject>>> quads = new List<List<List<GameObject>>>();

        if(HasAtLeastTwoHorizontalAndVerticalLines(horizontalLines, verticalLines))
        {
            for(int i = 0; i < horizontalLines.Count; ++i)
            {
                for(int j = 0; j < horizontalLines[i].Count; ++j)
                {
                    // 1ere ligne horizontale
                    List<GameObject> horizontalLineOne = horizontalLines[i][j];

                    List<List<GameObject>> verticalLinesForHorizontalLineOne = FindVerticalIntersectionLines(horizontalLineOne, verticalLines);

                    for (int k = 0; k < verticalLinesForHorizontalLineOne.Count; ++k)
                    {
                        // 1er ligne vertical
                        List<GameObject> verticalLineOne = verticalLinesForHorizontalLineOne[k];

                        List<List<GameObject>> horizontalLinesForVerticalLine = FindHorizontalIntersectionLines(verticalLineOne, horizontalLineOne, horizontalLines);

                        for(int l = 0; l < horizontalLinesForVerticalLine.Count; ++l)
                        {
                            // 2eme ligne horizontal
                            List<GameObject> horizontalLineTwo = horizontalLinesForVerticalLine[l];

                            List<List<GameObject>> verticalLineForTwoHorizontalLine = FindVerticalIntersectionLines(horizontalLineOne, horizontalLineTwo, verticalLineOne, verticalLinesForHorizontalLineOne);

                            for (int m = 0; m < verticalLineForTwoHorizontalLine.Count; ++m)
                            {
                                List<GameObject> verticalLineTwo = verticalLineForTwoHorizontalLine[m];

                                if((horizontalLineOne[0].transform.position.z - horizontalLineTwo[0].transform.position.z) >= (_distanceBetweenCubeZ * 2))
                                {
                                    List<List<GameObject>> quad = new List<List<GameObject>>();
                                    quad.Add(horizontalLineOne);
                                    quad.Add(verticalLineOne);
                                    quad.Add(horizontalLineTwo);
                                    quad.Add(verticalLineTwo);

                                    if(!CheckIfQuadExist(quads, quad))
                                        quads.Add(quad);
                                }
                            }
                        }

                    }
                }
            }
        }

        return quads;
    }

    // Détermine s'il y a au moins 2 lignes verticales et 2 lignes horizontales
    bool HasAtLeastTwoHorizontalAndVerticalLines(List<List<List<GameObject>>> horizontalLines, List<List<List<GameObject>>> verticalLines)
    {
        int nbHorizontalLines = 0;
        int nbVerticalLines = 0;

        for (int i = 0; i < verticalLines.Count; ++i)
        {
            if (verticalLines[i].Count > 0)
            {
                ++nbVerticalLines;

                if (nbVerticalLines >= 2)
                    break;
            }
        }

        for (int i = 0; i < horizontalLines.Count; ++i)
        {
            if (horizontalLines[i].Count > 0)
            {
                ++nbHorizontalLines;

                if (nbHorizontalLines >= 2)
                    break;
            }
        }

        if (nbHorizontalLines >= 2 && nbVerticalLines >= 2)
            return true;
        return false;
    }

    // Trouve les lignes verticales coupant une ligne horizontal
    List<List<GameObject>> FindVerticalIntersectionLines(List<GameObject> horizontalLine, List<List<List<GameObject>>> verticalLines)
    {
        List<List<GameObject>> verticalIntersectionLines = new List<List<GameObject>>();

        for (int i = 0; i < verticalLines.Count; ++i)
        {
            for (int j = 0; j < verticalLines[i].Count; ++j)
            {
                if(Intersection(horizontalLine, verticalLines[i][j]))
                    verticalIntersectionLines.Add(verticalLines[i][j]);
            }
        }

        return verticalIntersectionLines;
    }

    // Trouve les lignes horizontales coupant une ligne vertical, différente d'une certaine ligne horizontal
    List<List<GameObject>> FindHorizontalIntersectionLines(List<GameObject> verticallLine, List<GameObject> horizontalLine, List<List<List<GameObject>>> horizontalLines)
    {
        List<List<GameObject>> horizontalIntersectionLines = new List<List<GameObject>>();

        for (int i = 0; i < horizontalLines.Count; ++i)
        {
            for (int j = 0; j < horizontalLines[i].Count; ++j)
            {
                if(horizontalLines[i][j] != horizontalLine)
                {
                    if (Intersection(horizontalLines[i][j], verticallLine))
                        horizontalIntersectionLines.Add(horizontalLines[i][j]);
                }
            }
        }

        return horizontalIntersectionLines;
    }

    // Trouve les lignes verticales coutant deux lignes horizontales différente d'une certain ligne vertical
    List<List<GameObject>> FindVerticalIntersectionLines(List<GameObject> horizontalLineOne, List<GameObject> horizontalLineTwo, List<GameObject> verticalLine, List<List<GameObject>> verticalLines)
    {
        List<List<GameObject>> verticalIntersectionLines = new List<List<GameObject>>();

        for (int i = 0; i < verticalLines.Count; ++i)
        {
                if (verticalLines[i] != verticalLine)
                {
                    if (Intersection(horizontalLineOne, verticalLines[i]) && Intersection(horizontalLineTwo, verticalLines[i]))
                        verticalIntersectionLines.Add(verticalLines[i]);
                }
        }

        return verticalIntersectionLines;
    }

    // Vérifie s'il y a intersection entre une ligne horizontal et une ligne vertical
    bool Intersection(List<GameObject> horizontalLine, List<GameObject> verticalLine)
    {
        for (int i = 0; i < horizontalLine.Count; ++i)
        {
            for (int j = 0; j < verticalLine.Count; ++j)
            {
                if (horizontalLine[i].transform.position == verticalLine[j].transform.position)
                {
                    return true;
                }
            }
        }
        return false;
    }

    // Regarde si un carré se trouve dans la liste des carrés existant
    bool CheckIfQuadExist(List<List<List<GameObject>>> quads, List<List<GameObject>> quad)
    {
        for (int i = 0; i < quads.Count; ++i)
        {
            if(CheckQuad(quads[i], quad))
                return true;
        }
        return false;
    }

    // Compare si deux carrés sont identiques
    bool CheckQuad(List<List<GameObject>> quadOne, List<List<GameObject>> quadTwo)
    {
        List<bool> listBool = new List<bool>();

        if(quadOne.Count == quadTwo.Count)
        {
            foreach(List<GameObject> lineOne in quadOne)
            {
                foreach(List<GameObject> lineTwo in quadTwo)
                {
                    if (lineOne == lineTwo)
                        listBool.Add(true);
                }
            }
        }

        if (listBool.Count == quadOne.Count)
            return true;

        return false;
    }

    List<int> FindQuadIndex(List<List<GameObject>> quadOne)
    {
        List<int> quadIndex = new List<int>();

        List<GameObject> horizontalOne = quadOne[0];
        List<GameObject> verticalOne = quadOne[1];
        List<GameObject> horizontalTwo = quadOne[2];
        List<GameObject> verticalTwo = quadOne[3];

        int index;

        index = FindIndexIntersection(horizontalOne, verticalOne);
        if (index > -1)
            quadIndex.Add(index);

        index = FindIndexIntersection(horizontalTwo, verticalOne);
        if (index > -1)
            quadIndex.Add(index);

        index = FindIndexIntersection(horizontalTwo, verticalTwo);
        if (index > -1)
            quadIndex.Add(index);

        index = FindIndexIntersection(horizontalOne, verticalTwo);
        if (index > -1)
            quadIndex.Add(index);

        return quadIndex;
    }

    int FindIndexIntersection(List<GameObject> horizontalLine, List<GameObject> verticalLine)
    {
        int index = -1;
        for (int i = 0; i < horizontalLine.Count; ++i)
        {
            for (int j = 0; j < verticalLine.Count; ++j)
            {
                if (horizontalLine[i].transform.position == verticalLine[j].transform.position)
                {
                    index = i;
                    break;
                }

                if (index != -1)
                    break;
            }
        }

        if(index != -1)
        {
            for(int i = 0; i < _board.Count; ++i)
            {
                if (_board[i].transform.position == horizontalLine[index].transform.position)
                {
                    index = i;
                    break;
                }
            }
        }

        return index;
    }
}
