using UnityEngine;

public class Danger : MonoBehaviour
{
    [SerializeField] private Vector2 rightScreenPosition = new Vector2(200, 0);
    [SerializeField] private Vector2 leftScreenPosition = new Vector2(-200, 0);

    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }

    public void ActivateIndicator(bool comingFromRight)
    {
        // Simply ensure the arrow is active when called
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        // Position and Flip
        if (comingFromRight)
        {
            rectTransform.anchoredPosition = rightScreenPosition;
            rectTransform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            rectTransform.anchoredPosition = leftScreenPosition;
            rectTransform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    public void DeactivateIndicator()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}