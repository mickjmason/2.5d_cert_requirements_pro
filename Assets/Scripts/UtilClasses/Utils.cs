using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.UtilClasses
{
    public static class Utils
    {
        public static void LogNulls(object checkSource)
        {
            if (checkSource == null)
            {
                var name = checkSource.GetType().Name;
                Debug.LogError(name + " is null");

            }
        }
    }
}
