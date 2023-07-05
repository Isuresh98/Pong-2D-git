using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    public GameObject portraitPanel;
    public GameObject landscapePanel;
  

    
   

    private void Start()
    {
        portraitPanel.SetActive(true);
        landscapePanel.SetActive(false);
       
    }

    public void lancapeON()
    {

        portraitPanel.SetActive(false);
        landscapePanel.SetActive(true);
    }

    public void PotrateON()
    {
        portraitPanel.SetActive(true);
        landscapePanel.SetActive(false);
    }
}
