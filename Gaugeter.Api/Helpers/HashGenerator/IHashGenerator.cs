namespace Gaugeter.Api.Helpers.HashGenerator
{
    public interface IHashGenerator
    {
        string ComputeSha256Hash(string rawData);

        string ComputeSha1Hash(string rawData);
    }
}
