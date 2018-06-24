namespace SMS.Common
{
    public enum ProductList
    {
        SMS = 1
    }

    public enum Status
    {
        Inactive = 1,
        Active = 2
    }

    public enum CompanyType
    {
        Financial = 1,
        Medical = 2,
        Commercial = 3,
        Media = 4
    }

    public enum AdviserType
    {
        Financial = 1,
        Medical = 2,
        Commercial = 3,
        Media = 4
    }

    public enum BrokerType
    {
        Financial = 1,
        Medical = 2,
        Commercial = 3,
        Media = 4
    }

    public enum PlayerType
    {
        Financial = 1,
        Medical = 2,
        Commercial = 3,
        Media = 4
    }

    public enum CommandMood
    {
        Add = 1,
        Edit = 2,
        View = 3,
        Remove = 4
    }

    public enum UserType
    {
        Company=1,
        Broker=2,
        Player=3,
        Adviser=4,
        Admin = 5,
    }
}
