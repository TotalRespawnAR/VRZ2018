using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverSscript : MonoBehaviour {

     Vector3 _endPosition;
    float originalHeight = 0f;

    float _gameTime = 120f;
 

    public void StartMistMove(Vector3 argTohere)
    {
        if (_gameTime <= 0.1)
        {
            DebugConsole.print("Gametime is Wrong!!!");
        }
        else
        {
            DebugConsole.print("Mist Is Moving to");

            _endPosition = new Vector3(
                argTohere.x,
                originalHeight,
               argTohere.z);

            DebugConsole.print("here x=  " + _endPosition.x);
            DebugConsole.print("here y =  " + _endPosition.y);
            DebugConsole.print("here z=  " + _endPosition.z);

            StartCoroutine(MoveOverSeconds(_gameTime));

        }
    }

    void Start()
    {
       
        originalHeight = gameObject.transform.position.y;

        StartMistMove(Vector3.zero);
    }


    public IEnumerator MoveOverSeconds(float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = this.gameObject.transform.position;
        while (elapsedTime < seconds)
        {
            transform.position = Vector3.Lerp(startingPos, _endPosition, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = _endPosition;
    }
}
