using UnityEngine;

using UnitySampleAssets.CrossPlatformInput;

namespace UnitySampleAssets._2D
{
    public class Platformer2DUserControl : MonoBehaviour
    {
        #region Fields

        public bool jump;

        private PlatformerCharacter2D character;

        #endregion

        #region Methods

        private void Awake()
        {
            this.character = this.GetComponent<PlatformerCharacter2D>();
        }

        private void FixedUpdate()
        {
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            this.character.Move(h, crouch, this.jump);
            this.jump = false;
        }

        private void Update()
        {
            if (!this.jump)
            {
                this.jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }

        #endregion
    }
}