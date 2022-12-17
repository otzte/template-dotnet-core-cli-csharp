using System;
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;




namespace dotnetcore
{
    class Program
    {
        static void Main(string[] args)
        {
            string secretName = "MySecret";
            string keyVaultName = "azrael";
            var kvUri = "https://azrael.vault.azure.net";
            SecretClientOptions options = new SecretClientOptions(){
                Retry = {
                    Delay = TimeSpan.FromSeconds(2),
                    MaxDelay = TimeSpan.FromSeconds(16),
                    MaxRetries = 5,
                    Mode = RetryMode.Exponential                    
                }
            };
            var client = new SecretClient(new Uri(kvUri),new DefaultAzureCredential(),options);
            KeyVaultSecret secret = client.GetSecret(secretName);
            Console.WriteLine("GETSECRET: "+ secret.Value);
            Console.Write("Enter Secret >");
            string secretValue = Console.ReadLine();
            client.SetSecret(secretName, secretValue);
            Console.Write("SetSecret");
            Console.Write(" Key: ", secretName);
            Console.Write(" Value: ", secretValue);
            Console.WriteLine(" GETSECRRET ", secret.Value);

            client.StartDeleteSecret(secretName);
            Console.WriteLine("StartDeleteSecrete" + keyVaultName);

            Console.WriteLine(" GETSECRRET ", secret.Value);
        }
    }
}
