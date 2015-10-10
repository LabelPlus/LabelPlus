/**
 * 
 * Copyright 2015, Noodlefighter
 * Released under GPL License.
 *
 * License: http://noodlefighter.com/label_plus/license
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LabelPlus
{
    public class GroupDefineItemCollection
    { 
        GroupDefineItem[] defaultItems; 
        List<GroupDefineItem> items;

        public string GetDefaultName(int n) 
        {
            if (n - 1 > defaultItems.Length)
            {
                //不存在用户分组
                throw new Exception();
            }
            else
            {
                return defaultItems[n - 1].Name;
            }
        }

        public string GetViewName(int n){
            if (n - 1 >= items.Count)
            {
                //不存在用户分组
                return "G" + n.ToString();
            }
            else {
                return items[n - 1].Name;
            }
        }
        public string[] GetViewNames()
        {
            List<string> tmp = new List<string>();
            for (int i = 1; i <= 9; i++)
                tmp.Add(GetViewName(i));

            return tmp.ToArray();
        }
        public string GetFullViewName(int n)
        {
            if (n > items.Count) 
            {
                //不存在用户分组
                return "G" + n.ToString();
            }
            else
            {
                return "G"+ n.ToString() + items[n - 1].Name;
            }
        }
        public string[] GetFullViewNames()
        {
            List<string> tmp = new List<string>();
            for (int i = 1; i <= 9; i++)
                tmp.Add(GetFullViewName(i));

            return tmp.ToArray();
        }

        public Color GetColor(int n) {
            return defaultItems[n - 1].Color;
        }

        public Color[] GetColors() {
            Color[] tmp = new Color[defaultItems.Length];
            for (int i = 0; i < defaultItems.Length; i++)
            {
                tmp[i] = defaultItems[i].Color;
            }

            return tmp;
        }

        public int UserGroupCount { get { return items.Count; } }
        public int DefaultGroupCount { get { return defaultItems.Length; } }

        public bool AddUserGroup(string name) { 
            try
            {
                items.Add(new GroupDefineItem(name));
                return true;
            }
            catch { return false; }
        }

        public bool UseDefaultUserGroup()
        {
            try
            {
                items.Clear();

                foreach (var item in defaultItems) {
                    if (item.Name != "")
                    {
                        items.Add(new GroupDefineItem(item.Name));
                    }
                    else
                    {
                        break;
                    }
                }
                return true;
            }
            catch { return false; }
        }
        
        public bool LoadUserGroup(List<string> strs)
        {
            try
            {
                items.Clear();

                foreach (var str in strs) {
                    if (str != "")
                    {
                        items.Add(new GroupDefineItem(str));
                    }
                    else
                    {
                        break;
                    }
                }
                return true;
            }
            catch { return false; }
        }

        public string[] GetUserGroupNameArray() 
        {
            List<string> tmp = new List<string>();
            foreach (var item in items) {
                tmp.Add(item.Name);
            }
            return tmp.ToArray();
        }

        public string[] GetDefaultGroupNameArray()
        {
            List<string> tmp = new List<string>();
            foreach (var item in defaultItems)
            {
                if (item.Name != "")
                    tmp.Add(item.Name);
                else
                    break;
            }
            return tmp.ToArray();
        }

        public void ClearUserGroup() {
            items = new List<GroupDefineItem>();
        }

        public GroupDefineItemCollection(GroupDefineItem[] defaultDefine)
        {
            items = new List<GroupDefineItem>();
            this.defaultItems = defaultDefine;
        }
    }
}
