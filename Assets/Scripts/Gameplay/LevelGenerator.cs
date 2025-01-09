using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelGenerator : MonoBehaviour
{
    public List<GameObject> pipeList = new List<GameObject>();
    public List<GameObject> groundList = new List<GameObject>();
    public List<GameObject> backgroundList = new List<GameObject>();

    private float backgroundSize;
    private float groundSize;

    public Transform resetPoint;

    public int gameState { get; private set; } = 0;

    private float gameSpeed = 2f;

    private float minHeight = -1f;
    private float maxHeight = 2f;

    private float minDistance = 2.2f;
    private float maxDistance = 4;

    private void OnEnable()
    {
        InputManager.OnGameStart += UpdateGameState;
        FloppyBehaviour.OnDeath += UpdateGameState;
    }

    private void OnDisable()
    {
        InputManager.OnGameStart -= UpdateGameState;
        FloppyBehaviour.OnDeath -= UpdateGameState;
    }

    void Start()
    {
        if(groundList.Count != 0)
            groundSize = GetElementsSize(groundList[0]);

        if (backgroundList.Count != 0)
            backgroundSize = GetElementsSize(backgroundList[0]);

        for (int pipeIndex = 0; pipeIndex <= pipeList.Count - 1; pipeIndex++)
        {
            RandomizePipe(pipeIndex);
        }
    }

    void Update()
    {
        MoveLevel();
    }

    /// <summary>
    /// 0 = not started; 1 = playing; 2 = gameover.
    /// </summary>
    private void UpdateGameState()
    {
        gameState++;
    }

    /// <summary>
    /// Move all level to simulate bird movement
    /// </summary>
    private void MoveLevel()
    {
        if (gameState == 2) return; //game over

        foreach (GameObject ground in groundList)
            HandleGround(ground);

        foreach (GameObject background in backgroundList)
            HandleBackGround(background);

        if (gameState == 0) return; //game not started

        foreach (GameObject pipe in pipeList)
            HandlePipe(pipe);
    }

    private void HandleGround(GameObject _ground)
    {
        _ground.transform.position += Vector3.left * (gameSpeed * Time.deltaTime);

        if (_ground.transform.position.x <= resetPoint.transform.position.x * 2)
        {
            GameObject furthestObject = groundList
            .OrderBy(element =>
            {
                return element.transform.position.x;
            })
            .LastOrDefault();
            _ground.transform.position = new Vector3(furthestObject.transform.position.x + groundSize, _ground.transform.position.y, 0);
        }
    }

    private void HandleBackGround(GameObject _background)
    {
        _background.transform.position += Vector3.left * (gameSpeed/5 * Time.deltaTime);

        if (_background.transform.position.x <= resetPoint.transform.position.x)
        {
            GameObject furthestObject = backgroundList
            .OrderBy(element =>
            {
                return element.transform.position.x;
            })
            .LastOrDefault();
            _background.transform.position = new Vector3(furthestObject.transform.position.x + backgroundSize, _background.transform.position.y, 0);
        }
    }

    private void HandlePipe(GameObject _pipe)
    {
        _pipe.transform.position += Vector3.left * (gameSpeed * Time.deltaTime);
            
        if (_pipe.transform.position.x <= resetPoint.transform.position.x)
        {
            RandomizePipe(_pipe);
        }
    }

    /// <summary>
    /// Randomize pipe by index in list.
    /// </summary>
    /// <param name="_pipeIndex"></param>
    private void RandomizePipe(int _pipeIndex)
    {
        float randomHeight = Random.Range(minHeight, maxHeight);
        float randomPos = Random.Range(minDistance, maxDistance);
        
        // Not working
        pipeList[_pipeIndex].GetComponent<Pipes>().SetRandomSpace();
        
        if (_pipeIndex == 0)
            pipeList[_pipeIndex].transform.position = new Vector3(pipeList[_pipeIndex].transform.position.x, randomHeight, 0);
        else
            pipeList[_pipeIndex].transform.position = new Vector3(pipeList[_pipeIndex - 1].transform.position.x + randomPos, randomHeight, 0);

    }
    /// <summary>
    /// Randomize pipe By gameObjects.
    /// </summary>
    /// <param name="pipe"></param>
    private void RandomizePipe(GameObject pipe)
    {
        float randomHeight = Random.Range(minHeight, maxHeight);
        float randomPos = Random.Range(minDistance, maxDistance);

        GameObject furthestObject = pipeList
        .OrderBy(element =>
            {
                return element.transform.position.x;
            })
        .LastOrDefault();

        // Not working
        pipe.GetComponent<Pipes>().SetRandomSpace();

        pipe.transform.position = new Vector3(furthestObject.transform.position.x + randomPos, randomHeight, 0);

    }

    private float GetElementsSize(GameObject _element)
    {
        return _element.GetComponent<SpriteRenderer>().sprite.bounds.size.x * _element.transform.localScale.x;
    }
}
