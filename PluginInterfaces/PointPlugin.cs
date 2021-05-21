using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Points.Plugins
{
    public static class PointPlugin
    {
        public static IDictionary<string, IPointPlugin> LoadPlugin(string pathfile)
        {
            IDictionary<string, IPointPlugin> result = new Dictionary<string, IPointPlugin>();
            if (File.Exists(pathfile))
            {
                var asm = Assembly.LoadFile(pathfile);
                Type pluginClass;
                IPointPlugin plugin;
                IDictionary<string, IPointPlugin> typelist = new Dictionary<string, IPointPlugin>();
                List<string> sortlist = new List<string>();
                foreach (Type type in asm.GetTypes())
                {
                    if (!type.IsAbstract && type.IsClass &&
                        type.GetInterface("IPointPlugin") != null)
                    {
                        pluginClass = type.GetInterface("IPointPlugin");
                        plugin = Activator.CreateInstance(type) as IPointPlugin;
                        if (plugin != null)
                        {
                            string key = plugin.PluginCategory + plugin.PluginShortType;
                            if (!typelist.ContainsKey(key))
                            {
                                typelist.Add(key, plugin);
                                sortlist.Add(key);
                            }
                        }
                    }
                }
                sortlist.Sort();
                foreach (string key in sortlist)
                {
                    result.Add(key, typelist[key]);
                }
            }
            return result;
        }

        public static IDictionary<string, IPointPlugin> LoadPlugins(string path)
        {
            IDictionary<string, IPointPlugin> result = new Dictionary<string, IPointPlugin>();
            List<string> filelist = new List<string>(Directory.GetFiles(path));
            filelist.Sort();
            foreach (string f in filelist)
            {
                FileInfo fi = new FileInfo(f);
                if (fi.Extension.Equals(".dll"))
                {
                    string[] items = fi.Name.Split(new char[] { '.' });
                    if (items.Length > 1 && items[0].Equals("Points") &&
                        !items[1].Equals("Plugins"))
                    {
                        IDictionary<string, IPointPlugin> list = 
                            LoadPlugin(path + "\\" + fi.Name);
                        foreach (KeyValuePair<string, IPointPlugin> kvp in list)
                        {
                            result.Add(kvp);
                        }
                    }
                }
            }
            return result;
        }

    }
}
