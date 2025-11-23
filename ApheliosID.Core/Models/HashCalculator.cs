using System;
using System.Text;
using System.Security.Cryptography;

public class HashCalculator
{
    private string rawData{get;}

    public HashCalculator(string rawdata)
    {
        rawData = rawdata;
    }

    public string CalculateSHA256()
    {
        using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                return Convert.ToHexString(bytes).ToLower();
            }
    }
}