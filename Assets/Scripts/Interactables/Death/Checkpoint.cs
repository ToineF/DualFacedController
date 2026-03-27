using System;
using Cattac.Character;


namespace Cattac.Interactables.Death
{
    public class Checkpoint : BoxTriggerUnityEventPlayer
    {
        private void Start()
        {
            OnTriggerEnter.AddListener(SetCheckpoint);
        }

        private void OnDestroy()
        {
            OnTriggerEnter.RemoveListener(SetCheckpoint);
        }

        private void SetCheckpoint(CharacterHead characterHead)
        {
            CheckpointsManager.CurrentCheckpoint = this;
        }


        /*#region MulticolorGizmo

        [ExecuteInEditMode]

        // private Color GetMulticolor(Color color)
        // {
        //     // Normalize RGB values
        //     var red = color.r / 255f;
        //     var green = color.g / 255f;
        //     var blue = color.b / 255f;
        //
        //     // Find the minimum and maximum values of R, G and B.
        //     float hue = 0;
        //     if (red > green && red > blue)
        //     {
        //         hue = green-blue/(red-Mathf.Min(green, blue));
        //     }
        //     else if (green > blue)
        //     {
        //         hue = 2.0f+ (blue - red) / (green - Mathf.Min(red, blue));
        //     }
        //     else
        //     {
        //         hue = 4.0f + (red - green) / (blue - Mathf.Min(red, green));
        //     }
        //
        //     hue++;
        //     hue %= 360;
        //
        //     color.r
        //
        // }

        private float GetMin3(float a, float b, float c)
        {
            return Mathf.Min(Mathf.Min(a, b), c);
        }

        private Color HueToRGB(float hue)
        {
            var currentRed = 5 + hue * 6 % 6;
            var currentGreen = 3 + hue * 6 % 6;
            var currentBlue = 1 + hue * 6 % 6;

            var red = 1 - Mathf.Max(GetMin3(currentRed, 4 - currentRed, 1), 0);
            var green = 1 - Mathf.Max(GetMin3(currentGreen, 4 - currentGreen, 1), 0);
            var blue = 1 - Mathf.Max(GetMin3(currentBlue, 4 - currentBlue, 1), 0);

            return new Color(red, green, blue);
        }

        [SerializeField] float speed;
        [SerializeField] Color[] myColors;
        private int colorIndex;
        private float time = 0f;

        private Color UpdateDD(Color color)
        {
            _color = Color.Lerp(_color, myColors[colorIndex], speed * Time.deltaTime);

            time = Mathf.Lerp(time, 1f, speed * Time.deltaTime);
            if (time > 0.9f)
            {
                time = 0;
                colorIndex = (colorIndex + 1) % myColors.Length;
            }

            return color;
        }

        private void Update()
        {
            UpdateDD(_color);
        }

        #endregion*/
    }
}