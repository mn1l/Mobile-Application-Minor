using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

namespace Home
{
    public class ButtonInfo : MonoBehaviour
    {

        public int ItemID;
        public TMP_Text PriceTxt;
        public HID.Button Equip;
        public GameObject StoreScript;

        void Update()
        {
            
        }
    }
}