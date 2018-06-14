using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

public class CustomConfigurationSource : IConfigurationSource {
    public IConfigurationProvider Build (IConfigurationBuilder builder) {
        return new CustomConfigurationProvider ();
    }
}

public class CustomConfigurationProvider : ConfigurationProvider {
    public override void Load () {
        Data = new Dictionary<string, string> { { "Custom:Site:Name", "John Wu's Blog" },
            { "Custom:Site:Domain", "blog.johnwu.cc" }
        };
    }
}