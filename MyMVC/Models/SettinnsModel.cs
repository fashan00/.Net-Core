public class Settings {
    public string[] SupportedCultures { get; set; }
    public CustomObject CustomObject { get; set; }
}

public class CustomObject {
    public Property1 Property { get; set; }
}

public class Property1 {
    public int SubProperty1 { get; set; }
    public bool SubProperty2 { get; set; }
    public string SubProperty3 { get; set; }
}