using SnowLeopard.Lynx.Extension;
using System;

namespace SnowLeopard.Lynx.ConsoleTest
{
    class Program
    {
        public const string INPUT_STR = "9C55DBE1-5FB6-4EE9-B956-574999DB023A";
        static void Main(string[] args)
        {
            // $2b$10$kI6fEOnO36yzXkvWgp9a8O9KRb4ha2Ej6qar.6iREna73OJlgCYi.

            var userSubmittedPassword = "userSubmittedPassword";
            // hash and save a password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userSubmittedPassword);

            // check a password
            bool validPassword = BCrypt.Net.BCrypt.Verify(userSubmittedPassword, hashedPassword);


            Console.WriteLine($"{nameof(HashExtension.Md5)}：\t\t{INPUT_STR.Md5()}");
            Console.WriteLine($"{nameof(HashExtension.Sha1)}：\t\t{INPUT_STR.Sha1()}");
            Console.WriteLine($"{nameof(HashExtension.Sha256)}：\t{INPUT_STR.Sha256()}");
            Console.WriteLine($"{nameof(HashExtension.Sha384)}：\t{INPUT_STR.Sha384()}");
            Console.WriteLine($"{nameof(HashExtension.Sha512)}：\t{INPUT_STR.Sha512()}");
            Console.WriteLine($"{nameof(HashExtension.Hmac)}Md5：\t{INPUT_STR.Hmac(AlgorithmNameEnum.HMACMD5)}");
            Console.WriteLine($"{nameof(HashExtension.Hmac)}Sha1：\t{INPUT_STR.Hmac(AlgorithmNameEnum.HMACSHA1)}");
            Console.WriteLine($"{nameof(HashExtension.Hmac)}Sha256：\t{INPUT_STR.Hmac(AlgorithmNameEnum.HMACSHA256)}");
            Console.WriteLine($"{nameof(HashExtension.Hmac)}Sha384：\t{INPUT_STR.Hmac(AlgorithmNameEnum.HMACSHA384)}");
            Console.WriteLine($"{nameof(HashExtension.Hmac)}Sha512：\t{INPUT_STR.Hmac(AlgorithmNameEnum.HMACSHA512)}");

            Console.ReadLine();
        }
    }
}
