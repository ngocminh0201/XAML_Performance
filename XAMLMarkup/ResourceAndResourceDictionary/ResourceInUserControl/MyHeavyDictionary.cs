using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test3.XAMLMarkup.ResourceAndResourceDictionary.ResourceInUserControl
{
    public class MyHeavyDictionary : ResourceDictionary
    {
        public static int InstanceCounter = 0;

        public MyHeavyDictionary()
        {
            InstanceCounter++;
            System.Diagnostics.Debug.WriteLine($"MyHeavyDictionary #{InstanceCounter} created");
        }
    }

}
