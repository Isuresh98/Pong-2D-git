using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAP_Manager : MonoBehaviour
{
    private string gem80 ="80gem";
    private string gem500 ="500gem";
    private string gem1200 ="1200gem";
    private string gem2500 ="2500gem";
    private string gem6500 ="6500gem";
    private string gem14000 ="14000gem";

    private GemsManager gemsmanager;


    private void Start()
    {
        gemsmanager = FindObjectOfType<GemsManager>();

    }
    public void OnPurchaseComplete(Product product)
    {
        if(product.definition.id==gem80)
        {
            //reword player gem
            Debug.Log("get the 80 gems");
            gemsmanager.AddGems(80);

        }
        if (product.definition.id == gem500)
        {
            //reword player gem
            Debug.Log("get the 500 gems");
            gemsmanager.AddGems(500);

        }
        if (product.definition.id == gem1200)
        {
            //reword player gem
            Debug.Log("get the 1200 gems");
            gemsmanager.AddGems(1200);

        }
        if (product.definition.id == gem2500)
        {
            //reword player gem
            Debug.Log("get the 2500 gems");
            gemsmanager.AddGems(2500);

        }
        if (product.definition.id == gem6500)
        {
            //reword player gem
            Debug.Log("get the 6500 gems");
            gemsmanager.AddGems(6500);

        }
        if (product.definition.id == gem14000)
        {
            //reword player gem
            Debug.Log("get the 14000 gems");
            gemsmanager.AddGems(14000);

        }
       
    }
   
    public void OnPurchaseFailed(Product product,PurchaseFailureReason failureReason)
    {
        Debug.Log(product.definition.id + "Failear becouse " + failureReason);
    }


}
