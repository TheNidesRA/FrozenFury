using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public PlayerEconomySO PlayerEconomySo;

    [SerializeField]
    public enum CoinType
    {
        Coin,
        Ring
    }

    [SerializeField]
    public enum ItemToBuy
    {
        ConeMan,
        Torrine,
        ConemanSkin1,
        ConemanSkin2,
        TorrineSkin1,
        TorrineSkin2,
        ICMSkin1,
        ICMSkin2
    }


    private float _coins;
    private float _rings;

    public TextMeshProUGUI TextoMonedas;
    public TextMeshProUGUI TextoAnillos;

    private bool _ConeMan;
    private bool _Torrine;

    private bool _Skin_1_ICM;

    public bool ConeMan
    {
        get => _ConeMan;
        set
        {
            _ConeMan = value;

            PlayerEconomySo.ConeMan = value;
            ButtonBuyConeman.interactable = !value;
            ButtonChooseConeman.interactable = value;
        }
    }

    public bool Torrine
    {
        get => _Torrine;
        set
        {
            _Torrine = value;

            PlayerEconomySo.Torrine = value;
            ButtonBuyTorrine.interactable = !value;
            ButtonChooseTorrine.interactable = value;
        }
    }

    public bool Skin1Icm
    {
        get => _Skin_1_ICM;
        set
        {
            _Skin_1_ICM = value;

            PlayerEconomySo.ICM_skin_1 = value;
            ButtonBuy_Skin_1_ICM.interactable = !value;
            ButtonChooseICMSkin1.interactable = value;
        }
    }

    public bool Skin2Icm
    {
        get => _Skin_2_ICM;
        set
        {
            _Skin_2_ICM = value;

            PlayerEconomySo.ICM_skin_2 = value;
            ButtonBuy_Skin_2_ICM.interactable = !value;
            ButtonChooseICMSkin2.interactable = value;
        }
    }

    public bool Skin1ConeMan
    {
        get => _Skin_1_ConeMan;
        set
        {
            _Skin_1_ConeMan = value;

            PlayerEconomySo.ConeMan_skin_1 = value;
            ButtonBuy_Skin_1_Coneman.interactable = !value;
            ButtonChooseConemanSkin1.interactable = value;
        }
    }

    public bool Skin2ConeMan
    {
        get => _Skin_2_ConeMan;
        set
        {
            _Skin_2_ConeMan = value;

            PlayerEconomySo.ConeMan_skin_2 = value;
            ButtonBuy_Skin_2_Coneman.interactable = !value;
            ButtonChooseConemanSkin2.interactable = value;
        }
    }

    public bool Skin1Torrine
    {
        get => _Skin_1_Torrine;
        set
        {
            _Skin_1_Torrine = value;

            PlayerEconomySo.Torrine_skin_1 = value;
            ButtonBuy_Skin_1_Torrine.interactable = !value;
            ButtonChooseTorrineSkin1.interactable = value;
        }
    }

    public bool Skin2Torrine
    {
        get => _Skin_2_Torrine;
        set
        {
            _Skin_2_Torrine = value;

            PlayerEconomySo.Torrine_skin_2 = value;
            ButtonBuy_Skin_2_Torrine.interactable = !value;
            ButtonChooseTorrineSkin2.interactable = value;
        }
    }

    private bool _Skin_2_ICM;
    private bool _Skin_1_ConeMan;
    private bool _Skin_2_ConeMan;
    private bool _Skin_1_Torrine;
    private bool _Skin_2_Torrine;


    public Button ButtonBuyConeman;
    public Button ButtonBuyTorrine;

    public Button ButtonBuy_Skin_1_ICM;
    public Button ButtonBuy_Skin_2_ICM;
    public Button ButtonBuy_Skin_1_Coneman;
    public Button ButtonBuy_Skin_2_Coneman;
    public Button ButtonBuy_Skin_1_Torrine;
    public Button ButtonBuy_Skin_2_Torrine;


    public Button ButtonChooseConeman;
    public Button ButtonChooseTorrine;

    public Button ButtonChooseConemanSkin1;
    public Button ButtonChooseConemanSkin2;
    public Button ButtonChooseICMSkin1;
    public Button ButtonChooseICMSkin2;
    public Button ButtonChooseTorrineSkin1;
    public Button ButtonChooseTorrineSkin2;


    public GameObject ConfirmDialoge;
    public Button yes;

    public Transform modelPosition;
    public GameObject model;


    public float Coins
    {
        get => _coins;
        set
        {
            _coins = value;
            PlayerEconomySo.coins = value;
            if (value > 99999)
            {
                TextoMonedas.text = "+99.999";
            }
            else
            {
                TextoMonedas.text = String.Format("{0:n0}", value);
            }
        }
    }

    public float Rings
    {
        get => _rings;
        set
        {
            _rings = value;
            PlayerEconomySo.rings = value;

            if (value > 99999)
            {
                TextoAnillos.text = "+99.999";
            }
            else
            {
                TextoAnillos.text = String.Format("{0:n0}", value);
            }
        }
    }

    private void Awake()
    {
        Coins = PlayerEconomySo.coins;
        Rings = PlayerEconomySo.rings;
        Torrine = PlayerEconomySo.Torrine;
        ConeMan = PlayerEconomySo.ConeMan;
        Skin1Torrine = PlayerEconomySo.Torrine_skin_1;
        Skin2Torrine = PlayerEconomySo.Torrine_skin_2;
        Skin1ConeMan = PlayerEconomySo.ConeMan_skin_1;
        Skin2ConeMan = PlayerEconomySo.ConeMan_skin_2;
        Skin1Icm = PlayerEconomySo.ICM_skin_1;
        Skin2Icm = PlayerEconomySo.ICM_skin_2;
    }


    public void AddCoins(float coins)
    {
        Coins += coins;
    }

    public void AddRings(float rings)
    {
        Rings += rings;
    }

    public void BuyTorrine(float cost)
    {
        CoinType moneda = CoinType.Ring;
        if (CheckMoney(cost, moneda))
        {
            ConfirmDialoge.SetActive(true);
            yes.onClick.RemoveAllListeners();
            yes.onClick.AddListener(() =>
            {
                Buy(cost, moneda);
                Torrine = true;
                ConfirmDialoge.SetActive(false);
            });
        }
    }

    public void BuyConeman(float cost)
    {
        CoinType moneda = CoinType.Ring;
        if (CheckMoney(cost, moneda))
        {
            ConfirmDialoge.SetActive(true);
            yes.onClick.RemoveAllListeners();
            yes.onClick.AddListener(() =>
            {
                Buy(cost, moneda);
                ConeMan = true;
                ConfirmDialoge.SetActive(false);
            });
        }
    }

    public void BuyConemanSkin1(float cost)
    {
        CoinType moneda = CoinType.Coin;
        if (CheckMoney(cost, moneda))
        {
            ConfirmDialoge.SetActive(true);
            yes.onClick.RemoveAllListeners();
            yes.onClick.AddListener(() =>
            {
                Buy(cost, moneda);
                Skin1ConeMan = true;
                ConfirmDialoge.SetActive(false);
            });
        }
    }

    public void BuyTorrineSkin1(float cost)
    {
        CoinType moneda = CoinType.Coin;
        if (CheckMoney(cost, moneda))
        {
            ConfirmDialoge.SetActive(true);
            yes.onClick.RemoveAllListeners();
            yes.onClick.AddListener(() =>
            {
                Buy(cost, moneda);
                Skin1Torrine = true;
                ConfirmDialoge.SetActive(false);
            });
        }
    }

    public void BuyICMSkin1(float cost)
    {
        CoinType moneda = CoinType.Coin;
        if (CheckMoney(cost, moneda))
        {
            ConfirmDialoge.SetActive(true);
            yes.onClick.RemoveAllListeners();
            yes.onClick.AddListener(() =>
            {
                Buy(cost, moneda);
                Skin1Icm = true;
                ConfirmDialoge.SetActive(false);
            });
        }
    }


    public void BuyConemanSkin2(float cost)
    {
        CoinType moneda = CoinType.Ring;
        if (CheckMoney(cost, moneda))
        {
            ConfirmDialoge.SetActive(true);
            yes.onClick.RemoveAllListeners();
            yes.onClick.AddListener(() =>
            {
                Buy(cost, moneda);
                Skin2ConeMan = true;
                ConfirmDialoge.SetActive(false);
            });
        }
    }

    public void BuyTorrineSkin2(float cost)
    {
        CoinType moneda = CoinType.Ring;
        if (CheckMoney(cost, moneda))
        {
            ConfirmDialoge.SetActive(true);
            yes.onClick.RemoveAllListeners();
            yes.onClick.AddListener(() =>
            {
                Buy(cost, moneda);
                Skin2Torrine = true;
                ConfirmDialoge.SetActive(false);
            });
        }
    }

    public void BuyICMSkin2(float cost)
    {
        CoinType moneda = CoinType.Ring;
        if (CheckMoney(cost, moneda))
        {
            ConfirmDialoge.SetActive(true);
            yes.onClick.RemoveAllListeners();
            yes.onClick.AddListener(() =>
            {
                Buy(cost, moneda);
                Skin2Icm = true;
                ConfirmDialoge.SetActive(false);
            });
        }
    }


    public void tryToBuy(float cost, CoinType moneda, ItemToBuy item)
    {
        if (CheckMoney(cost, moneda))
        {
            switch (item)
            {
                case ItemToBuy.Torrine:
                    Torrine = true;
                    break;
                case ItemToBuy.ConeMan:
                    ConeMan = true;
                    break;
                case ItemToBuy.ConemanSkin1:
                    Skin1ConeMan = true;
                    break;
                case ItemToBuy.ConemanSkin2:
                    Skin2ConeMan = true;
                    break;
                case ItemToBuy.TorrineSkin1:
                    Skin1Torrine = true;
                    break;
                case ItemToBuy.TorrineSkin2:
                    Skin2Torrine = true;
                    break;
                case ItemToBuy.ICMSkin1:
                    Skin1Icm = true;
                    break;
                case ItemToBuy.ICMSkin2:
                    Skin2Icm = true;
                    break;
            }
        }
    }

    private void Buy(float cost, CoinType moneda)
    {
        switch (moneda)
        {
            case CoinType.Coin:
                Coins -= cost;
                break;

            case CoinType.Ring:
                Rings -= cost;
                break;

            default:
                break;
        }
    }

    private bool CheckMoney(float cost, CoinType moneda)
    {
        switch (moneda)
        {
            case CoinType.Coin:
                if (Coins < cost)
                {
                    Fallo();
                    return false;
                }

                return true;
            case CoinType.Ring:
                if (Rings < cost)
                {
                    Fallo();
                    return false;
                }

                AudioManager.Instance?.PlayRandomBuy();
                return true;
            default:
                return false;
        }
    }

    public void AddPriceImage(Sprite img)
    {
        yes.image.sprite = img;
    }

    public void AddModel(GameObject pj)
    {
        if (model != null)
        {
            Destroy(model);
        }


        model = Instantiate(pj, modelPosition);

        Vector3 pos = new Vector3(0, -280, -300);
        Vector3 rotation = new Vector3(0, 180, 0);

        model.transform.localScale = new Vector3(60, 60, 60);
        model.transform.localPosition = pos;
        model.transform.localRotation = Quaternion.Euler(rotation);
    }

    public void AddModelShop(GameObject pj)
    {
        if (model != null)
        {
            Destroy(model);
        }


        model = Instantiate(pj, modelPosition);

        Vector3 pos = new Vector3(0, -280, -300);
        Vector3 rotation = new Vector3(0, 180, 0);

        model.transform.localScale = new Vector3(60, 60, 60) * 3.5f;
        model.transform.localPosition = pos;
        model.transform.localRotation = Quaternion.Euler(rotation);
    }


    private void Fallo()
    {
        Debug.Log("Sos pobre no lo puedes comprar jajajaja");
    }
}