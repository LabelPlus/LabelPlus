/**
 * 
 * Copyright 2015, Noodlefighter
 * Released under GPL License.
 *
 * License: http://noodlefighter.com/label_plus/license
 */

/*
 *  Describe:           Global Var Manage Class ,ignore case.
 *  Author:             Noodlefitghter 
 *   Edit Data:          2014/08/13
 */

using System.Collections;
 
namespace LabelPlus
{
    static class StringResources
    {
        static Hashtable globalVar;
        static StringResources() {
            globalVar = new Hashtable();
        }
        static public string GetValue(string itemName)
        {
            itemName=itemName.ToLower();

            return (string)globalVar[itemName];
        }
        static public void SetValue(string itemName,string value)
        {
            itemName = itemName.ToLower();

            if (!globalVar.ContainsKey(itemName))
            {
                globalVar.Add(itemName, value);
            }
            else {
                globalVar[itemName] = value;
            }
        }        
    }
}
