using GoMemory.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GoMemory.Helpers
{
    public class ColorsCollection
    {
        public static ComplexColor[] ColorsArray()
        {
            ComplexColor[] allColours = new ComplexColor[9];
        allColours[0] = new ComplexColor() { TextColor = Color.Blue, SpeltColor = "Blue" };
        allColours[1] = new ComplexColor() { TextColor = Color.Brown, SpeltColor = "Brown" };
        allColours[2] = new ComplexColor() { TextColor = Color.Red, SpeltColor = "Red" };
        allColours[3] = new ComplexColor() { TextColor = Color.Gray, SpeltColor = "Gray" };
        allColours[4] = new ComplexColor() { TextColor = Color.Green, SpeltColor = "Green" };
        allColours[5] = new ComplexColor() { TextColor = Color.Orange, SpeltColor = "Orange" };
        allColours[6] = new ComplexColor() { TextColor = Color.Pink, SpeltColor = "Pink" };
        allColours[7] = new ComplexColor() { TextColor = Color.Purple, SpeltColor = "Purple" };
        allColours[8] = new ComplexColor() { TextColor = Color.Teal, SpeltColor = "Teal" };

            return allColours;
        }
}
}
