using System.Collections;
using UnityEngine;

public class CamShaker : MonoBehaviour
{

    IEnumerator Shakeit(float duration, float magnitude)
    {

        Vector3 originalPos = this.transform.position;
        Vector3 originalLocPos = this.transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {

            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = new Vector3(x, originalLocPos.y, y);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        transform.localPosition = originalLocPos;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            StartCoroutine(Shakeit(0.75f, 0.08f));
        }
    }
}
