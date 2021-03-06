﻿using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace Mystique.Core
{
    public class CollectibleAssemblyLoadContext : AssemblyLoadContext
    {
        private Assembly _entryPoint = null;
        private bool _isEnabled = false;
        private readonly string _pluginName = string.Empty;
        private Dictionary<string, Stream> _resourceItems = null;

        public CollectibleAssemblyLoadContext(string pluginName) : base(isCollectible: true)
        {
            _pluginName = pluginName;
            _resourceItems = new Dictionary<string, Stream>();
        }

        public string PluginName => _pluginName;

        public bool IsEnabled => _isEnabled;

        public void Enable()
        {
            _isEnabled = true;
        }

        public void AddResource(string path, Stream stream)
        {
            if (_resourceItems.ContainsKey(path))
            {
                _resourceItems[path] = stream;
            }
            else
            {
                _resourceItems.Add(path, stream);
            }
        }

        public void RemoveResource(string path)
        {
            if (_resourceItems.ContainsKey(path))
            {
                _resourceItems.Remove(path);
            }
        }

        public Stream LoadResource(string virtualPath)
        {
            var className = $"{PluginName}.{virtualPath.Replace("~/", string.Empty).Replace("/", ".")}";

            if (_resourceItems.ContainsKey(className))
            {
                var stream = _resourceItems[className];

                stream.Position = 0;
                return stream;
            }
            else
            {
                return null;
            }
        }

        public void ClearResource()
        {
            _resourceItems.Clear();
        }

        public void Disable()
        {
            _isEnabled = false;
        }

        public void SetEntryPoint(Assembly entryPoint)
        {
            _entryPoint = entryPoint;
        }

        public Assembly GetEntryPointAssembly()
        {
            return _entryPoint;
        }

        protected override Assembly Load(AssemblyName name)
        {
            return null;
        }
    }
}
