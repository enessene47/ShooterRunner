using UnityEngine;

namespace Mechanics
{
    public class SwipeMecLast : CustomManager
    {
        #region Singleton
        public static SwipeMecLast instance = null;
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        #endregion

        [Header("Swipe Variables")]
        public float clampMaxVal;
        public float lerpMult = 1;
        public float mouseDamp = 600;
        public Transform obj;

        [Header("Others")]
        private float startPosX;
        private float deltaMousePos;
        
        private float rate;

        bool isTouchScreen;


        [HideInInspector] public Vector3 desiredPos = Vector3.zero;

        public void BaseStart()
        {
            rate = Screen.width / 250.0f;

            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
                Input.multiTouchEnabled = false;

            else
                isTouchScreen = false;
        }

        public void Swipe()
        {
            if (isTouchScreen)
                TouchControl();
            else
                MouseControl();
        }

        void MouseControl()
        {
            if (Input.GetMouseButtonDown(0))
                ResetValues();
            else if (Input.GetMouseButton(0))
                ControlOnHold();
            else if(Input.GetMouseButtonUp(0))
                transform.MyDORotate(Vector3.zero);
        }

        void TouchControl()
        {
            switch (Input.touches[0].phase)
            {
                case TouchPhase.Began:
                    ResetValues();
                    break;

                case TouchPhase.Moved:
                    ControlOnHold();
                    break;
                case TouchPhase.Ended:
                    transform.MyDORotate(Vector3.zero);
                    break;
            }
        }

        void ControlOnHold()
        {
            var mousePos = Input.mousePosition;

            deltaMousePos = mousePos.x - startPosX;

            if (mousePos.x > startPosX + rate)
                QuaternionLerp(new Vector3(0, 30, 0));
            else if (mousePos.x < startPosX - rate)
                QuaternionLerp(new Vector3(0, -30, 0));
            else if (deltaMousePos == 0)
                QuaternionLerp(Vector3.zero, 1);

            PositionMethod();
        }

        public void ResetValues() => startPosX = Input.mousePosition.x;

        void PositionMethod()
        {
            float xPos = obj.position.x;

            xPos = Mathf.Lerp(xPos, xPos + (mouseDamp * (deltaMousePos / Screen.width)), Time.deltaTime * lerpMult);

            xPos = Mathf.Clamp(xPos, -clampMaxVal, clampMaxVal);

            obj.position = new Vector3(xPos, obj.position.y, obj.position.z);

            ResetValues();
        }
        private void QuaternionLerp(Vector3 vec, float speed = 5) => transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(vec), Time.deltaTime * speed);
    }
}

