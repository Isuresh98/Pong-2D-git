using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAP_Manager : MonoBehaviour
{
    private string gem80 ="80gem";
    private string gem100 ="100gem";
    private string gem200 ="200gem";
    private string gem300 ="300gem";
    private string gem500 ="500gem";
    private string gem750 ="750gem";
    private string gem1k ="1kgem";
    private string gem2k ="2kgem";
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
        if (product.definition.id == gem100)
        {
            //reword player gem
            Debug.Log("get the 100 gems");
            gemsmanager.AddGems(100);

        }
        if (product.definition.id == gem200)
        {
            //reword player gem
            Debug.Log("get the 200 gems");
            gemsmanager.AddGems(200);

        }
        if (product.definition.id == gem300)
        {
            //reword player gem
            Debug.Log("get the 300 gems");
            gemsmanager.AddGems(300);

        }
        if (product.definition.id == gem500)
        {
            //reword player gem
            Debug.Log("get the 500 gems");
            gemsmanager.AddGems(500);

        }
        if (product.definition.id == gem750)
        {
            //reword player gem
            Debug.Log("get the 750 gems");
            gemsmanager.AddGems(750);

        }
        if (product.definition.id == gem1k)
        {
            //reword player gem
            Debug.Log("get the 1k gems");
            gemsmanager.AddGems(1000);

        }
        if (product.definition.id == gem2k)
        {
            //reword player gem
            Debug.Log("get the 2k gems");
            gemsmanager.AddGems(2000);

        }
    }
   
    public void OnPurchaseFailed(Product product,PurchaseFailureReason failureReason)
    {
        Debug.Log(product.definition.id + "Failear becouse " + failureReason);
    }


}
