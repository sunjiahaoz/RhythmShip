/*
ColorHSL
By: @sunjiahaoz, 2016-5-11

HSL颜色以及HSL与RGB颜色的转换
*/
using UnityEngine;
using System.Collections;

namespace sunjiahaoz
{

    public class ColorHSL
    {
        public ColorHSL(float fHue, float fSaturation, float fLuminance)
        {
            _fHue = fHue;
            _fSaturation = fSaturation;
            _fLuminance = fLuminance;
        }

        public ColorHSL()
        {

        }

        public float _fHue; // 0~360
        public float _fSaturation;  // 0~100
        public float _fLuminance;   // 0~100

        public Color Color()
        {
            return HSLtoRGB(this);
        }


        public static Color HSLtoRGB(ColorHSL chsl)
        {
            float h = chsl._fHue;                  // h must be [0, 360]  
            float s = chsl._fSaturation / 100f; // s must be [0, 1]  
            float l = chsl._fLuminance / 100f;      // l must be [0, 1]  
            float R, G, B;
            if (chsl._fSaturation == 0)
            {
                // achromatic color (gray scale)  
                R = G = B = l * 255.0f;
            }
            else
            {
                float q = (l < 0.5f) ? (l * (1.0f + s)) : (l + s - (l * s));
                float p = (2.0f * l) - q;
                float Hk = h / 360.0f;
                float[] T = new float[3];
                T[0] = Hk + 0.3333333f; // Tr   0.3333333f=1.0/3.0  
                T[1] = Hk;              // Tb  
                T[2] = Hk - 0.3333333f; // Tg  
                for (int i = 0; i < 3; i++)
                {
                    if (T[i] < 0) T[i] += 1.0f;
                    if (T[i] > 1) T[i] -= 1.0f;
                    if ((T[i] * 6) < 1)
                    {
                        T[i] = p + ((q - p) * 6.0f * T[i]);
                    }
                    else if ((T[i] * 2.0f) < 1) //(1.0/6.0)<=T[i] && T[i]<0.5  
                    {
                        T[i] = q;
                    }
                    else if ((T[i] * 3.0f) < 2) // 0.5<=T[i] && T[i]<(2.0/3.0)  
                    {
                        T[i] = p + (q - p) * ((2.0f / 3.0f) - T[i]) * 6.0f;
                    }
                    else T[i] = p;
                }
                R = T[0] * 255.0f;
                G = T[1] * 255.0f;
                B = T[2] * 255.0f;
            }

            Color color = new Color();
            color.a = 1;
            color.r = (((R > 255) ? 255 : ((R < 0) ? 0 : R))) / 255;
            color.g = (((G > 255) ? 255 : ((G < 0) ? 0 : G))) / 255;
            color.b = (((B > 255) ? 255 : ((B < 0) ? 0 : B))) / 255;
            return color;
        }

        public static ColorHSL RGBtoHSL(Color color)
        {
            float h = 0, s = 0, l = 0;
            // normalizes red-green-blue values  
            float r = color.r;
            float g = color.g;
            float b = color.b;
            float maxVal = Mathf.Max(r, g, b);
            float minVal = Mathf.Min(r, g, b);

            // hue  
            if (maxVal == minVal)
            {
                h = 0; // undefined  
            }
            else if (maxVal == r && g >= b)
            {
                h = 60.0f * (g - b) / (maxVal - minVal);
            }
            else if (maxVal == r && g < b)
            {
                h = 60.0f * (g - b) / (maxVal - minVal) + 360.0f;
            }
            else if (maxVal == g)
            {
                h = 60.0f * (b - r) / (maxVal - minVal) + 120.0f;
            }
            else if (maxVal == b)
            {
                h = 60.0f * (r - g) / (maxVal - minVal) + 240.0f;
            }

            // luminance  
            l = (maxVal + minVal) / 2.0f;

            // saturation  
            if (l == 0 || maxVal == minVal)
            {
                s = 0;
            }
            else if (0 < l && l <= 0.5f)
            {
                s = (maxVal - minVal) / (maxVal + minVal);
            }
            else if (l > 0.5f)
            {
                s = (maxVal - minVal) / (2 - (maxVal + minVal)); //(maxVal-minVal > 0)?  
            }

            ColorHSL colorHsl = new ColorHSL();
            colorHsl._fHue = (h > 360) ? 360 : ((h < 0) ? 0 : h);
            colorHsl._fSaturation = ((s > 1) ? 1 : ((s < 0) ? 0 : s)) * 100;
            colorHsl._fLuminance = ((l > 1) ? 1 : ((l < 0) ? 0 : l)) * 100;

            return colorHsl;
        }
    }
}
