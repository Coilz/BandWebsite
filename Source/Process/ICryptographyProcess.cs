namespace Ewk.BandWebsite.Process
{
    public interface ICryptographyProcess
    {
        string Encrypt(string value);
        string Decrypt(string encryptedValue);

        string GenerateSecureRandomNumber(int lenth);
    }
}