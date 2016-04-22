using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace sunjiahaoz
{
    public enum ColorName
    {
        White,
        Black,
        Red,
        Blue,
        Yellow,
        Green,
        Gray,
        Magenta,

        Yolk,   // 淡黄
        Brown, // 棕色
    }
    public class SimpleColorFactory : SimpleDataFactory<ColorName, Color>
    {
        public SimpleColorFactory()
        {
            RegisterData(ColorName.White, Color.white);
            RegisterData(ColorName.Black, Color.black);
            RegisterData(ColorName.Red, Color.red);
            RegisterData(ColorName.Blue, Color.blue);
            RegisterData(ColorName.Yellow, Color.yellow);
            RegisterData(ColorName.Green, Color.green);
            RegisterData(ColorName.Gray, Color.gray);
            RegisterData(ColorName.Magenta, Color.magenta);
            RegisterData(ColorName.Yolk, new Color(1, 1, 176f / 255f));
            RegisterData(ColorName.Brown, new Color(180f / 255f, 90f / 255f, 0));
        }
    }
}
