using UnityEngine;

public class Pipes : MonoBehaviour
{
    public GameObject topPipe;
    public GameObject bottomPipe;

    public float minSpace;
    public float maxSpace;

    public void SetRandomSpace()
    {
        float randomSpace = Random.Range(minSpace, maxSpace);

        topPipe.transform.position = new Vector3(topPipe.transform.position.x, randomSpace, 0);
        bottomPipe.transform.position = new Vector3(topPipe.transform.position.x, -randomSpace, 0);
    }
}
