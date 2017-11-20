using System;

namespace SecretSantaAssigner
{
    public static class SystemVariables
    {
        public static string Domain
        {
            get
            {
                return Environment.GetEnvironmentVariable("EMAIL_SENDER_DOMAIN");
            }
        }

        public static string ServiceAddress
        {
            get
            {
                return Environment.GetEnvironmentVariable("EMAIL_SENDER_SERVICE_ADDRESS");
            }
        }

        public static string Host
        {
            get
            {
                return Environment.GetEnvironmentVariable("EMAIL_SENDER_HOST");
            }
        }
    }
}
