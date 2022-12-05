namespace BaseCorePlugin.Contracts
{
    public interface IFTPService
    {
        string Host { get; set; }
        string User { get; set; }
        string Password { get; set; }
    }
}
