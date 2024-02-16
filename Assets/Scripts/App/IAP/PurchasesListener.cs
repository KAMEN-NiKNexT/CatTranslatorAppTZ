using System;
using System.Collections.Generic;
using UnityEngine;
using RevenueCat;
using Kamen.DataSave;
using TMPro;

public class PurchasesListener : Purchases.UpdatedCustomerInfoListener
{
    public static PurchasesListener Instance;

    #region Variables

    [Header("Objects")]
    [SerializeField] private Purchases _purchases;
    public Purchases.PromotionalOffer lad;
    [SerializeField] private TextMeshProUGUI _text;

    #endregion

    public override void CustomerInfoReceived(Purchases.CustomerInfo customerInfo)
    {
        // display new CustomerInfo
    }

    #region Unity Methods

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    #endregion

    private void Start()
    {
        var purchases = GetComponent<Purchases>();
        purchases.SetDebugLogsEnabled(true);
        purchases.GetOfferings((offerings, error) =>
        {
            if (error != null)
            {
                // show error
            }
            else
            {
                // show offering
            }
        });
    }

    public void PurchaseSubscription(string productIdentifier, Action callback)
    {
        Debug.Log("Оформление подписки");
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        //_text.text = "Оформление подписки";
        _purchases.PurchaseProduct(productIdentifier, (productIdentifier, customerInfo, userCancelled, error) => 
        {
            if (error != null)
            {
            //_text.text = "Ошибка при покупке подписки: " + error.ToString();
                Debug.Log("Ошибка при покупке подписки: " + error.ToString());
            }
            else
            {
            //_text.text = "Подписка успешно оформлена: " + productIdentifier;
                Debug.Log("Подписка успешно оформлена: " + productIdentifier);
                DataSaveManager.Instance.MyData.IsSubscribed = true;
                DataSaveManager.Instance.SaveData();
                callback?.Invoke();
            }
        });
#else
        Debug.Log("Подписка успешно оформлена: " + productIdentifier);
                DataSaveManager.Instance.MyData.IsSubscribed = true;
                DataSaveManager.Instance.SaveData();
                callback?.Invoke();
#endif
    }
    Purchases.CustomerInfoFunc currentCustomerInfo;
    private void W(Purchases.CustomerInfoFunc a)
    {
        currentCustomerInfo = a;
    }
    public void PurchaseSubscriptionWithDiscount(string productIdentifier, Action callback)
    {
        Debug.Log("Оформление подписки");
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        //_text.text = "Оформление подписки";
        Purchases.StoreProduct prod = null;
        Purchases.PromotionalOffer offer = null;
        Purchases.Discount discount = null;

        _purchases.GetProducts(new string[] { productIdentifier }, (products, error) =>
        {
            if (error != null)
            {
                Debug.Log("Ошибка при получение продукта: " + error.ToString());
                return;
            }
            else
            {
                Debug.Log("Продукт получен");
                prod = products[0];

                if (prod.Discounts.Length <= 0)
                {
                    Debug.Log("Скуидок не существует");
                    return;
                }
                else
                {
                    discount = prod.Discounts[0];
                }
            }
        });
        _purchases.GetPromotionalOffer(prod, discount, (promotionalOffer, error) =>
        {
            if (error != null)
            {
                Debug.Log("Ошибка при получение оффера");
                return;
            }
            else
            {
                offer = promotionalOffer;
            }
        });
        _purchases.PurchaseDiscountedProduct(productIdentifier, offer, (productIdentifier, customerInfo, userCancelled, error) => 
        {
            if (error != null)
            {
            //_text.text = "Ошибка при покупке подписки: " + error.ToString();
                Debug.Log("Ошибка при покупке подписки со скидкой: " + error.ToString());
            }
            else
            {
            //_text.text = "Подписка успешно оформлена: " + productIdentifier;
                Debug.Log("Подписка со скидкой успешно оформлена: " + productIdentifier);
                DataSaveManager.Instance.MyData.IsSubscribed = true;
                DataSaveManager.Instance.SaveData();
                callback?.Invoke();
            }
        });
#else
        Debug.Log("Подписка успешно оформлена: " + productIdentifier);
        DataSaveManager.Instance.MyData.IsSubscribed = true;
        DataSaveManager.Instance.SaveData();
        callback?.Invoke();
#endif
    }


    public void BeginPurchase(Purchases.Package package)
    {
        var purchases = GetComponent<Purchases>();
        purchases.PurchasePackage(package, (productIdentifier, customerInfo, userCancelled, error) =>
        {
            if (!userCancelled)
            {
                if (error != null)
                {
                    // show error
                }
                else
                {
                    // show updated Customer Info
                }
            }
            else
            {
                // user cancelled, don't show an error
            }
        });
    }

    void RestoreClicked()
    {
        var purchases = GetComponent<Purchases>();
        purchases.RestorePurchases((customerInfo, error) =>
        {
            if (error != null)
            {
                // show error
            }
            else
            {
                // show updated Customer Info
            }
        });
    }
}