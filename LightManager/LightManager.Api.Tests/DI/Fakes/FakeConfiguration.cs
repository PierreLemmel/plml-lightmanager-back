using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LightManager.Api.Tests.DI.Fakes
{
    internal class FakeConfiguration : IConfiguration
    {
        public string this[string key]
        {
            get => $"Value-{key}";
            set => throw new NotImplementedException();
        }

        public IEnumerable<IConfigurationSection> GetChildren() => throw new NotImplementedException();
        public IChangeToken GetReloadToken() => throw new NotImplementedException();
        public IConfigurationSection GetSection(string key) => throw new NotImplementedException();
    }
}