using System;
using System.Numerics;

namespace RSA
{
    internal class Program
    {
        static Random random = new Random();

        // Kiểm tra số nguyên tố
        public static bool IsPrimeNumber(int number)
        {
            if (number <= 1)
                return false;

            if (number == 2 || number == 3)
                return true;

            if (number % 2 == 0)
                return false;

            int sqrt = (int)Math.Sqrt(number);

            for (int i = 3; i <= sqrt; i += 2)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }

        // Tạo số nguyên tố ngẫu nhiên
        public static int GeneratePrimeNumber()
        {
            int primeNumber;

            while (true)
            {
                primeNumber = random.Next(2, 100);

                if (IsPrimeNumber(primeNumber))
                    break;
            }

            return primeNumber;
        }

        // Hàm tính ước chung lớn nhất (GCD)
        public static BigInteger GCD(BigInteger a, BigInteger b)
        {
            while (b != 0)
            {
                BigInteger remainder = a % b;
                a = b;
                b = remainder;
            }

            return a;
        }

        // Hàm chọn khóa công khai
        public static BigInteger ChoosePublicKey(BigInteger phi)
        {
            BigInteger publicKey;

            do
            {
                publicKey = new BigInteger(random.Next(2, (int)phi - 1));
            }
            while (GCD(publicKey, phi) != 1);

            return publicKey;
        }

        // Hàm tìm nghịch đảo modulo
        public static BigInteger ModInverse(BigInteger a, BigInteger m)
        {
            BigInteger gcd, x, y;
            ExtendedEuclidean(a, m, out gcd, out x, out y);

            if (gcd != 1)
                throw new Exception("Không có nghịch đảo modulo.");

            while (x < 0)
                x += m;

            return x;
        }

        // Thuật toán Euclid mở rộng
        public static void ExtendedEuclidean(BigInteger a, BigInteger b, out BigInteger gcd, out BigInteger x, out BigInteger y)
        {
            // Khởi tạo các biến
            BigInteger m0 = b;
            BigInteger x0 = 0, x1 = 1;
            BigInteger y0 = 1, y1 = 0;

            while (b != 0)
            {
                BigInteger q = a / b;
                BigInteger r = a % b;

                BigInteger tempX = x0 - q * x1;
                BigInteger tempY = y0 - q * y1;

                a = b;
                b = r;

                x0 = x1;
                x1 = tempX;

                y0 = y1;
                y1 = tempY;
            }

            gcd = a;
            x = x0;
            y = y0;
        }

        static void Main(string[] args)
        {
            BigInteger p = GeneratePrimeNumber();
            BigInteger q = GeneratePrimeNumber();
            BigInteger n = BigInteger.Multiply(p, q);
            BigInteger phi = BigInteger.Multiply(p - 1, q - 1);
            BigInteger publicKey = ChoosePublicKey(phi);
        }
    }
}

