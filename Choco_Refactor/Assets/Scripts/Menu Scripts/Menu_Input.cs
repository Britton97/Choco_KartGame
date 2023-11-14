using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class Menu_Input : MonoBehaviour
{
    private Kart_Input _input;
    [SerializeField] private RectTransform _selector, leftButton, rightButton;
    [SerializeField] private Image lockSprite;
    [SerializeField] private TextMeshProUGUI _priceText, myMoneyText;
    [SerializeField] private int myMoney = 300;
    [SerializeField] private Vector3 leftPos, rightPos;
    [SerializeField] private bool leftActive = true;
    //[SerializeField] List<int> prices = new List<int>();
    [SerializeField] List<Store_Items> items = new List<Store_Items>();
    private int currentSelection;
    [SerializeField] private bool aButtonDownLastFrame = false;
    [SerializeField] private bool aButtonDownCurrentFrame = false;
    [SerializeField] Sprite _lock, unlocked;
    [SerializeField] List<RectTransform> buttonPos = new List<RectTransform>();
    private int buttonSelection = 0;
    [SerializeField] private float interval = 1f;

    #region OnEnable/OnDisable Functions
    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }
    #endregion

    private void Awake()
    {
        _input = new Kart_Input();
        items.Add(new Store_Items(0, 100, false));
        items.Add(new Store_Items(1, 200, false));
        items.Add(new Store_Items(2, 300, true));
        items.Add(new Store_Items(3, 400, false));
        _selector.localPosition = leftButton.localPosition;
        currentSelection = 0;
        _priceText.text = items[currentSelection].price.ToString();
        lockSprite.sprite = items[currentSelection].isBought ? unlocked : _lock;
        myMoneyText.text = $"Money: {myMoney.ToString()}";
        StartCoroutine(SendMessageCoroutine());
    }

    private void FixedUpdate()
    {
        float button = _input.Kart_Controls.ActionButton.ReadValue<float>();
        if(button == 1)
        {
            aButtonDownCurrentFrame = true;
        }
        else
        {
            aButtonDownCurrentFrame = false;
        }
        //MoveSelector();
        Abutton();
    }

    private void LateUpdate()
    {
        aButtonDownLastFrame = aButtonDownCurrentFrame;
    }

    private void MoveSelector()
    {
        Vector2 move = _input.Kart_Controls.Move.ReadValue<Vector2>();
        if (move.x > .9)
        {
            _selector.localPosition = rightButton.localPosition;
            leftActive = false;
        }
        else if (move.x < -.9)
        {
            _selector.localPosition = leftButton.localPosition;
            leftActive = true;
        }
    }

    IEnumerator SendMessageCoroutine()
    {
        while (true)
        {
            // Your message-sending logic goes here
            //Debug.Log("Sending message...");
            Vector2 move = _input.Kart_Controls.Move.ReadValue<Vector2>();
            if (move.x > .9)
            {
                if(buttonSelection == buttonPos.Count - 1)
                {
                    buttonSelection = 0;
                }
                else
                {
                    buttonSelection++;
                }
            }
            else if(move.x < -.9)
            {
                if (buttonSelection == 0)
                {
                    buttonSelection = buttonPos.Count - 1;
                }
                else
                {
                    buttonSelection--;
                }
            }
            _selector.localPosition = buttonPos[buttonSelection].localPosition;
            // Wait for the specified interval before sending the next message
            yield return new WaitForSeconds(interval);
        }
    }

    private void Abutton()
    {
        float button = _input.Kart_Controls.ActionButton.ReadValue<float>();
        if (button == 1 && !aButtonDownLastFrame)
        {
            if (buttonSelection == 0)
            {
                if (currentSelection == 0)
                {
                    currentSelection = items.Count - 1;
                }
                else
                {
                    currentSelection--;
                }
            }
            else if(buttonSelection == 2)
            {
                if (currentSelection == items.Count - 1)
                {
                    currentSelection = 0;
                }
                else
                {
                    currentSelection++;
                }

            }
            else if(buttonSelection == 1) //on the lock
            {
                if (items[currentSelection].isBought)
                {
                    Debug.Log("Item is bought");
                }
                else
                {
                    if (items[currentSelection].price <= myMoney)
                    {
                        Debug.Log("Item is bought");
                        items[currentSelection].isBought = true;
                        lockSprite.sprite = unlocked;
                        myMoney -= items[currentSelection].price;
                        myMoneyText.text = $"Money: {myMoney.ToString()}";
                    }
                    else
                    {
                        Debug.Log("Item is not bought");
                    }
                }
            }
            _priceText.text = items[currentSelection].price.ToString();
            lockSprite.sprite = items[currentSelection].isBought ? unlocked : _lock;
        }
    }
}

public class Store_Items
{
    public int itemNumber;
    public int price;
    public bool isBought;
    public Store_Items(int _itemNumber, int _price, bool _isBought)
    {
        itemNumber = _itemNumber;
        price = _price;
        isBought = _isBought;
    }
}
