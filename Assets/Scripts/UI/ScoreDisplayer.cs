using UnityEngine;

public class ScoreDisplayer : MonoBehaviour
{
    public GameObject scoreTemplatePrefab;

    private void OnEnable()
    {
        Transform displayer = transform.GetChild(0);

        foreach (Transform child in displayer)
        {
            Destroy(child.gameObject);
        }

        int rank = 0;
        foreach (ScoreData score in LeaderBoard.ScoreBoard)
        {
            rank++;
            GameObject ScoreDisplayer = Instantiate(scoreTemplatePrefab, displayer);
            ScoreDisplayer.GetComponent<ScoreTemplate>().SetScore(rank, score);
        }
    }
}
