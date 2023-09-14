
public class GameplayData
{
    public string GetData(DataItem item)
    {
        return item switch
        {
            _ => throw new System.NotImplementedException()
        };
    }

    public void IncreaseItem(DataItem item)
    {
        throw new System.NotImplementedException();
    }

    public class DataItem : DataEnum
    {
        public DataItem() : base(0, "")
        {
        }

        public DataItem(int id, string name) : base(id, name)
        {
        }

        private DataItem(int id, string name, string description) : base(id, name)
        {
            Description = description;
        }
    }

}