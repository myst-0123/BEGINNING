using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace NovelScene
{
    public class ImageManager : MonoBehaviour
    {
        private Image backgroundImageComponent;
        private Image[] characterImageComponents = new Image[3];

        // Start is called before the first frame update
        void Start()
        {
            GameObject backgroundImage = GameObject.Find("BackgroundImage");
            GameObject character1 = GameObject.Find("Character1");
            GameObject character2 = GameObject.Find("Character2");
            GameObject character3 = GameObject.Find("Character3");

            backgroundImageComponent = backgroundImage.GetComponent<Image>();

            characterImageComponents[0] = character1.GetComponent<Image>();
            characterImageComponents[1] = character2.GetComponent<Image>();
            characterImageComponents[2] = character3.GetComponent<Image>();
        }

        public void SetBackgroundImage(string image_name)
        {
            Texture2D texture = Resources.Load("images/backgrounds/" + image_name) as Texture2D;

            backgroundImageComponent.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }

        public void SetBackgroundA(float num)
        {
            backgroundImageComponent.color = new Color(1.0f, 1.0f, 1.0f, num);
        }

        public void SetCharacterImage(int index, string image_name)
        {
            Texture2D texture = Resources.Load("images/characters/" + image_name) as Texture2D;

            characterImageComponents[index - 1].sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }
    }
}
