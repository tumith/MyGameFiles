using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use
        public static int keyCount;
        private GameObject dooor;
        private int count;
        public Text countText;
        public Text winText;
        public Text deathText;


        private void Start()
        {
            //segir til um hvað er í hverju eins og hvað stendur í count textanum og win textanum 
            dooor = GameObject.Find("Door");
            count = 0;
            countText.text = "Count: " + count.ToString();
            winText.text = "";
        }

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }
        // colider kóði til að skinja fjársjóð og lykil
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "key")
            {
                CarUserControl.keyCount += 2;
                other.gameObject.SetActive (true);
                dooor.SetActive(false);
            }
            else if (other.gameObject.tag == "PickUp")
            {
                other.gameObject.SetActive(false);
                count = count + 1;
                SetCountText();
            }
        }
        
        void SetCountText()
        {
            // ef þú ert búin að safna öllum fjársjóðinum þá skrifast "victory" en SetCountText er bara print 
            countText.text = "Count: " + count.ToString();
            if (count >= 14)
            {
                winText.text = "Victory";
            }
        }

        private void FixedUpdate()
        {
            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }
    }
}
