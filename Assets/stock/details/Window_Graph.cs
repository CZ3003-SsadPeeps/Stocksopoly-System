using UnityEngine;
using UnityEngine.UI;

public class Window_Graph : MonoBehaviour
{
    static readonly Color32 COLOR_GRAPH_ELEMENTS = new Color32(110, 129, 160, 255);

    [SerializeField] private Sprite circleSprite;
    public RectTransform graphContainer;
    public RectTransform labelTemplateX;
    public RectTransform labelTemplateY;

    //creates the circles that you see on the graph
    public GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        gameObject.GetComponent<Image>().color = COLOR_GRAPH_ELEMENTS;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
        return gameObject;

    }

    //generates the graph
    public void ShowGraph(int[] valueList)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        // change yMaximum if your max price is diff i.e. 10 dollar means y max is 10, change xSize to change how wide the graph is
        float yMaximum = 100f;
        float xSize = 55f;
        GameObject lastCircleGameObject = null;
        for (int i = 0; i < valueList.Length; i++)
        {
            float xPosition = 250f + (i * xSize);
            float yPosition = (valueList[i] / yMaximum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            if (lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject;

            // this part of the code insantiates the x-axis
            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer.transform, false);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xPosition, 10f);
            labelX.GetComponent<Text>().text = (i * 1f + 1f).ToString();
        }

        // this part of the code insantiates the y-axis
        int separatorCount = 10;
        for (int i = 0; i < separatorCount; i++)
        {
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graphContainer.transform, false);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f / separatorCount;
            labelY.anchoredPosition = new Vector2(150f, -387f + normalizedValue * graphHeight);
            labelY.GetComponent<Text>().text = $"{Mathf.RoundToInt(normalizedValue * yMaximum)}C";
        }
    }

    //code for creating the line connecting the dots
    public void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = COLOR_GRAPH_ELEMENTS;
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(dir));
    }

    //angle to change a vector to a floating number
    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}
