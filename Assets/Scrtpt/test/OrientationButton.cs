using UnityEngine;
using UnityEngine.UI;

public class OrientationButton : MonoBehaviour
{
    public Button orientationButton;

    private void Start()
    {
        // Check the initial orientation and set the button's initial behavior
        CheckOrientation();
    }

    private void Update()
    {
        // Check for orientation changes and update the button's behavior accordingly
        if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
        {
            // Portrait orientation
            orientationButton.onClick.RemoveAllListeners();
            orientationButton.onClick.AddListener(DoPortraitAction);
        }
        else if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            // Landscape orientation
            orientationButton.onClick.RemoveAllListeners();
            orientationButton.onClick.AddListener(DoLandscapeAction);
        }
    }

    public void CheckOrientation()
    {
        if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
        {
            // Portrait orientation
            orientationButton.onClick.RemoveAllListeners();
            orientationButton.onClick.AddListener(DoPortraitAction);

            // Trigger the button click event
            orientationButton.onClick.Invoke();
        }
        else if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            // Landscape orientation
            orientationButton.onClick.RemoveAllListeners();
            orientationButton.onClick.AddListener(DoLandscapeAction);

            // Trigger the button click event
            orientationButton.onClick.Invoke();
        }
    }

    private void DoPortraitAction()
    {
        // Define the action to be performed in portrait orientation
        Debug.Log("Portrait Action");
    }

    private void DoLandscapeAction()
    {
        // Define the action to be performed in landscape orientation
        Debug.Log("Landscape Action");
    }
}
