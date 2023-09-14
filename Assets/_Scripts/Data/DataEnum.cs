
public abstract class DataEnum : Enumeration
{
    public DataEnum(int id, string name) : base(id, name) { }
    public DataEnum(int id, string name, string description) : base(id, name) { Description = description; }
    public string Description = null;
}

