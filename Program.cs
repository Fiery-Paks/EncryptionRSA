using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleApp1
{
    internal class Program
    {
        static List<int> simpleNumbers = new List<int>()
        { 2, 3,  5, 7, 11, 13, 17, 19, 23 , 29, 31,
        37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79,
        83, 89, 97, 101, 103, 107, 109, 113, 127, 131,
        137, 139, 149, 151, 157, 163, 167, 173,  179, 181,
        191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241,
        251, 257, 263, 269, 271, 277, 281, 283, 293, 307, 311, 313,
        317, 331, 337, 347, 349, 353, 359, 367, 373,};/* 379, 383, 389,
        397, 401, 409, 419, 421, 431, 433, 439, 443, 449, 457, 461,
        463, 467, 479, 487, 491, 499, 503, 509, 521, 523, 541, 547,
        557, 563, 569, 571, 577, 587, 593, 599};*/

        static List<char> alphabet = new List<char>()
        {
            'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И',
            'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т',
            'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь',
            'Э', 'Ю', 'Я', ' '
        };

        static void Main(string[] args)
        {
            Random random = new Random();

            int rand_p = random.Next(0, simpleNumbers.Count - 1);
            int rand_q = random.Next(0, simpleNumbers.Count - 1);

            while (rand_p == rand_q)
            {
                rand_q = random.Next(0, simpleNumbers.Count - 1);
            }

            double p = simpleNumbers[rand_p];
            double q = simpleNumbers[rand_q];

            double n = p * q;


            double funcEler = (p - 1) * (q - 1);

            List<int> coprimeNumbers = FindCoprimeNumbers((int)funcEler);
            int rand_SN = random.Next(0, coprimeNumbers.Count - 1);

            double d = coprimeNumbers[rand_SN];

            int k = 0;
            double e = FindE(d, funcEler, ref k);

            Console.WriteLine($" Функция Эйлера = (p - 1) * (q - 1)");
            Console.WriteLine($" e = (k * Функция Эйлера + 1) / d \n n = p * q\n");

            Console.WriteLine($" p = {p}\n q = {q}");
            Console.WriteLine($" k = {k}\n d = {d}\n Функция Эйлера = ({p} - 1) * ({q} - 1) = {funcEler}");
            Console.WriteLine($" e = ({k} * {funcEler} + 1) / {d} = {e}\n n = {p} * {q} = {n}\n");

            Console.WriteLine($" Открытый ключ - ({e}, {n})\n Закрытый ключ - ({d}, {n})\n");

            Console.Write($" Введите ФИО: ");
            string fio = Console.ReadLine().ToUpper();
            if (fio.ToCharArray().Except(alphabet).ToArray().Length != 0)
                Console.WriteLine("В строке присутствуют неизвестные символы");
            else
            {
                Console.WriteLine();
                foreach (char _char in fio.ToCharArray())
                {
                    Console.WriteLine($" Буква: '{_char}'");
                    int numChar = alphabet.IndexOf(_char) + 2;
                    DoEncryptionDecryption(numChar, (int)e, (int)d, (int)n);
                }
            }
            Console.ReadLine();
        }
        static void DoEncryptionDecryption(int numChar, int e, int d, int n)
        {
            int ModExpnumChar = ModExp(numChar, e, n);
            Console.WriteLine($" Изначальное значение: {numChar}, Зашифрованное: {ModExpnumChar}");

            int newnumChar = ModExp(ModExpnumChar, d, n);
            Console.WriteLine($" Зашифрованное: {ModExpnumChar}, Расшифрованное: {newnumChar}");

            Console.WriteLine($" Расшифрованная буква - {alphabet[newnumChar - 2]}\n");
        }

        static int ModExp(int baseNum, int exp, int mod)
        {
            int result = 1;
            baseNum = baseNum % mod;

            while (exp > 0)
            {
                if (exp % 2 == 1)
                    result = (result * baseNum) % mod;

                baseNum = (baseNum * baseNum) % mod;
                exp = exp / 2;
            }

            return result;
        }

        private static double FindE(double d, double funcEler, ref int k)
        {
            double e = (k * funcEler + 1) / d;
            for (; !(e < funcEler) || !((k * funcEler + 1) % d == 0) || !((k * funcEler + 1) / d != d); k++)
                e = (k * funcEler + 1) / d;

            return (k * funcEler + 1) / d;
        }

        private static int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
        private static List<int> FindCoprimeNumbers(int n)
        {
            List<int> coprimeNumbers = new List<int>();
            for (int i = 1; i < n; ++i)
                if (GCD(n, i) == 1)
                    coprimeNumbers.Add(i);

            return coprimeNumbers;
        }
    }
}
