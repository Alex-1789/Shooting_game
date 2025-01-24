using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PointsManager : MonoBehaviour
{
    public static PointsManager Instance;
    [SerializeField] public TextMeshProUGUI UIPoints;
    private int points = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameObject uiObject = GameObject.FindWithTag("Points");
        if (uiObject != null)
        {
            UIPoints = uiObject.GetComponent<TextMeshProUGUI>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPoints(int value)
    {
        points += value;
        Debug.Log("punkt " + points);
        UpdatePointsText();
    }

    private void UpdatePointsText()
    {
        if (UIPoints != null)
        {
            UIPoints.text = "Points: " + points;
        }
    }

    public void ResetPoints()
    {
        points = 0;
    }
}
