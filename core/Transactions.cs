using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace ApheliosID.Core;

class Transaction
{
    public readonly string From;
    public readonly string To;
    public readonly string Data;
    public readonly DateTime Timestamp;
    public readonly string Signature;
    public readonly string Hash;
    public readonly TransactionType Type;

    public Transaction(string from , TransactionType type, string data)
    {
            if (string.IsNullOrWhiteSpace(from))
                throw new ArgumentException("El campo 'From' no puede estar vacío", nameof(from));
            
            if (string.IsNullOrWhiteSpace(data))
                throw new ArgumentException("El campo 'Data' no puede estar vacío", nameof(data));

            From = from;
            Type = type;
            Data = data;
            Timestamp = DateTime.UtcNow;
            Hash = CalculateHash();
    }

    public Transaction(string from, TransactionType type, string data, 
                          DateTime timestamp, string signature, string hash)
    {
            From = from;
            Type = type;
            Data = data;
            Timestamp = timestamp;
            Signature = signature;
            Hash = hash;
    }

    public string CalculateHash()
    {
            string rawData = $"{From}{Type}{Data}{Timestamp:O}";
            
            byte[] bytes = Encoding.UTF8.GetBytes(rawData);
            
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
    }

    public bool ValidHash()
    {
        return Hash == CalculateHash();
    }

    public bool VerifySignature(string publickey)
    {
	    if(string.IsNullOrWhiteSpace(Signature))
		    throw new InvalidOperationException("La transacción no tiene una firma para verificar.");
	    try
	    {
		    byte[] signatureBytes = Convert.FromBase64String(Signature);
		    byte[] dataBytes = Enconding.UTF8.GetBytes($"{From}{Type]{Data}{Timestamp:0}");
		    using (RSA rsa = RSA.Create())
		    {
			    rsa.FromXmlString(publickey);
			    return rsa.VerifyData(dataBytes, 
					    signatureBytes, 
					    HashAlgorithmName.SHA256, 
					    RSASignaturePadding.Pkcs1);
		    }
	    }
	    catch (System.Exception)
	    {
	        Console.WriteLine("Error al verificar la firma de la transacción.");
		return false;
	    }
    }

    public void Sign(string privateKeyPem)
    {
	try
	{
	    byte[] dataBytes = Encoding.UTF8.GetBytes($"{From}{Type}{Data}{Timestamp:O}");
	    using (RSA rsa = RSA.Create())
	    {
		    rsa.FromXmlString(privateKeyPem);
		    byte[] signatureBytes = rsa.SignData(dataBytes, 
				    HashAlgorithmName.SHA256, 
				    RSASignaturePadding.Pkcs1);
		    Signature = Convert.ToBase64String(signatureBytes);
	    }
	}
	catch (System.Exception)
	{
		throw new InvalidOperationException("Error al firmar la transacción.");
	}
    }

    public bool IsValid(string publicKeyPem)
    {
	    if(!HasValidHash())
	    {
		    Console.WriteLine("Hash X.");
		    return false;
	    }
	    if(!VerifySignature(publicKeyPem))
	    {
		    Console.WriteLine("Firma X");
		    return false;
	    }
	    return true;
    }
}

public enum TransactionType
{
	CREATE_IDENTITY,
	UPDATE_IDENTITY,
	DELETE_IDENTITY,
	ISSUE_CREDENTIAL,
	REVOKE_CREDENTIAL,
	DEACTIVATE_IDENTITY
}
