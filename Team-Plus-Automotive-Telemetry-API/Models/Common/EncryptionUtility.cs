using System.Security.Cryptography;
using System.Text;

namespace Team_Plus_Automotive_Telemetry_API.Models.Common
{
    public class EncryptionUtility
    {
        public static string GenerateEncryptedNumber(EncryptionUtilityRequest request)
        {
            string combinedInput = request.VIN + request.DeviceId;

            // Generate a SHA256 hash
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedInput));

                // Convert byte array to hex string
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                {
                    sb.Append(b.ToString("X2")); // Converts to hexadecimal
                }

                return sb.ToString();
            }
        }
        public static bool ValidateEncryptedNumber(EncryptionUtilityRequest request, string encryptedNumberToValidate)
        {
            // Regenerate the encrypted number from the input
            string generatedEncryptedNumber = EncryptionUtility.GenerateEncryptedNumber(request);

            // Compare the generated number with the one provided
            return generatedEncryptedNumber.Equals(encryptedNumberToValidate, StringComparison.OrdinalIgnoreCase);
        }
    }
}
