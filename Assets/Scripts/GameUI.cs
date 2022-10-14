using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Image FadeImage;
    public GameObject gameOverUI;
    void Start()
    {
        FindObjectOfType<Player>().OnDeath += OnGameOver;
    }

    // Update is called once per frame
    void OnGameOver()
    {
        StartCoroutine(Fade(Color.clear, Color.black, 1f));
        gameOverUI.SetActive(true);
    }

    IEnumerator Fade(Color from, Color to, float time)
    {
        float speed = 1 / time;
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * speed;
            FadeImage.color = Color.Lerp(from, to, percent);
            yield return null;

        }

    }

    public void LoadLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
}
