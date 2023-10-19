using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace Sakkbabuk
{
    internal class Program
    {
        #region Natúr
        static List<(int, int)> Szomszédai(string[] map, int i, int j)
        {
            List<(int, int)> rizzalt = new List<(int, int)>();

            int N = map.Length; //sorok szama
            int M = map[0].Length;  // oszlopok szama

            if (i > 0 && map[i - 1][j] != 'X')
            {
                rizzalt.Add((i - 1, j)); //N
            }
            if (j > 0 && map[i][j - 1] != 'X')
            {
                rizzalt.Add((i, j - 1)); //W
            }
            if (i < N - 1 && map[i + 1][j] != 'X')
            {
                rizzalt.Add((i + 1, j)); //S
            }
            if (j < M - 1 && map[i][j + 1] != 'X')
            {
                rizzalt.Add((i, j + 1)); //E
            }

            return rizzalt;
        }

        static List<(int, int)> Legrovidebb_ut(string[] map, (int, int) start, (int, int) end)
        {
            int N = map.Length;
            int M = map[0].Length;

            int fehér = 0;
            int kék = 1;
            int piros = 2;

            int[,] szin = new int[N, M];

            Queue<(int, int)> tennivalok = new Queue<(int, int)>();

            tennivalok.Enqueue(start);
            szin[start.Item1, start.Item2] = kék;

            (int, int)[,] honnan = new (int, int)[N, M];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    honnan[i, j] = (-2, -2);
                }
            }
            honnan[start.Item1, start.Item2] = (-1, -1);

            while (tennivalok.Count != 0)
            {
                (int, int) tennivalo = tennivalok.Dequeue();

                // FELDOLGOZÁS

                if (tennivalo == end)
                {
                    return Return_feltoltese(honnan, end);
                }
                szin[tennivalo.Item1, tennivalo.Item2] = piros;

                // SZOMSZÉDAIVAL FOGLALKOZUNK/TÖLTJÜK A TENNIVALÓKAT

                foreach ((int, int) szomszed in Szomszédai(map, tennivalo.Item1, tennivalo.Item2))
                {
                    if (szin[szomszed.Item1, szomszed.Item2] == fehér)
                    {
                        tennivalok.Enqueue(szomszed);
                        szin[szomszed.Item1, szomszed.Item2] = kék;
                        honnan[szomszed.Item1, szomszed.Item2] = tennivalo;
                    }
                }
            }

            return null;
        }
        #endregion

        #region Paraszt
        static List<(int, int)> Szomszédai_paraszt(string[] map, int i, int j)
        {
            List<(int, int)> rizzalt = new List<(int, int)>();

            if (i > 0 && map[i - 1][j] != 'X')
            {
                rizzalt.Add((i - 1, j)); //N
            }

            return rizzalt;
        }

        static List<(int, int)> Legrovidebb_ut_paraszt(string[] map, (int, int) start, (int, int) end)
        {
            int N = map.Length;
            int M = map[0].Length;

            int fehér = 0;
            int kék = 1;
            int piros = 2;

            int[,] szin = new int[N, M];

            Queue<(int, int)> tennivalok = new Queue<(int, int)>();

            tennivalok.Enqueue(start);
            szin[start.Item1, start.Item2] = kék;

            (int, int)[,] honnan = new (int, int)[N, M];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    honnan[i, j] = (-2, -2);
                }
            }
            honnan[start.Item1, start.Item2] = (-1, -1);

            while (tennivalok.Count != 0)
            {
                (int, int) tennivalo = tennivalok.Dequeue();

                // FELDOLGOZÁS

                if (tennivalo == end)
                {
                    return Return_feltoltese(honnan, end);
                }
                szin[tennivalo.Item1, tennivalo.Item2] = piros;

                // SZOMSZÉDAIVAL FOGLALKOZUNK/TÖLTJÜK A TENNIVALÓKAT

                foreach ((int, int) szomszed in Szomszédai_paraszt(map, tennivalo.Item1, tennivalo.Item2))
                {
                    if (szin[szomszed.Item1, szomszed.Item2] == fehér)
                    {
                        tennivalok.Enqueue(szomszed);
                        szin[szomszed.Item1, szomszed.Item2] = kék;
                        honnan[szomszed.Item1, szomszed.Item2] = tennivalo;
                    }
                }
            }

            return null;
        }
        #endregion

        #region Bástya
        static List<(int, int)> Szomszédai_bastya(string[] map, int i, int j)
        {
            List<(int, int)> rizzalt = new List<(int, int)>();

            int N = map.Length;
            int M = map[0].Length;

            int tav = 1;

            while (i - tav >= 0 && map[i - tav][j] != 'X')
            {
                rizzalt.Add((i - tav, j)); //N

                tav++;
            }
            tav = 1;

            while (j - tav >= 0 && map[i][j - tav] != 'X')
            {
                rizzalt.Add((i, j - tav)); //W

                tav++;
            }
            tav = 1;

            while (i + tav <= N - 1 && map[i + tav][j] != 'X')
            {
                rizzalt.Add((i + tav, j)); //S

                tav++;
            }
            tav = 1;

            while (j + tav <= M - 1 && map[i][j + tav] != 'X')
            {
                rizzalt.Add((i, j + tav)); //E

                tav++;
            }

            return rizzalt;
        }

        static List<(int, int)> Legrovidebb_ut_bastya(string[] map, (int, int) start, (int, int) end)
        {
            int N = map.Length;
            int M = map[0].Length;

            int fehér = 0;
            int kék = 1;
            int piros = 2;

            int[,] szin = new int[N, M];

            Queue<(int, int)> tennivalok = new Queue<(int, int)>();

            tennivalok.Enqueue(start);
            szin[start.Item1, start.Item2] = kék;

            (int, int)[,] honnan = new (int, int)[N, M];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    honnan[i, j] = (-2, -2);
                }
            }
            honnan[start.Item1, start.Item2] = (-1, -1);

            while (tennivalok.Count != 0)
            {
                (int, int) tennivalo = tennivalok.Dequeue();

                // FELDOLGOZÁS

                if (tennivalo == end)
                {
                    return Return_feltoltese(honnan, end);
                }
                szin[tennivalo.Item1, tennivalo.Item2] = piros;

                // SZOMSZÉDAIVAL FOGLALKOZUNK/TÖLTJÜK A TENNIVALÓKAT

                foreach ((int, int) szomszed in Szomszédai_bastya(map, tennivalo.Item1, tennivalo.Item2))
                {
                    if (szin[szomszed.Item1, szomszed.Item2] == fehér)
                    {
                        tennivalok.Enqueue(szomszed);
                        szin[szomszed.Item1, szomszed.Item2] = kék;
                        honnan[szomszed.Item1, szomszed.Item2] = tennivalo;
                    }
                }
            }

            return null;
        }
        #endregion

        #region Ló
        static List<(int, int)> Szomszédai_lo(string[] map, int i, int j)
        {
            List<(int, int)> rizzalt = new List<(int, int)>();

            int N = map.Length;
            int M = map[0].Length;

            if (i - 2 >= 0 && j - 1 >= 0 && map[i - 2][j - 1] != 'X')
            {
                rizzalt.Add((i - 2, j - 1));
            }           //i-2; j-1
            if (i - 2 >= 0 && j + 1 <= M - 1 && map[i - 2][j + 1] != 'X')
            {
                rizzalt.Add((i - 2, j + 1));
            }       //i-2; j+1
            if (i - 1 >= 0 && j + 2 <= M - 1 && map[i - 1][j + 2] != 'X')
            {
                rizzalt.Add((i - 1, j + 2));
            }       //i-1; j+2
            if (i + 1 <= N - 1 && j + 2 <= M - 1 && map[i + 1][j + 2] != 'X')
            {
                rizzalt.Add((i + 1, j + 2));
            }   //i+1; j+2
            if (i + 2 <= N - 1 && j + 1 <= M - 1 && map[i + 2][j + 1] != 'X')
            {
                rizzalt.Add((i + 2, j + 1));
            }   //i+2; j+1
            if (i + 2 <= N - 1 && j - 1 >= 0 && map[i + 2][j - 1] != 'X')
            {
                rizzalt.Add((i + 2, j - 1));
            }       //i+2; j-1
            if (i + 1 <= N - 1 && j - 2 >= 0 && map[i + 1][j - 2] != 'X')
            {
                rizzalt.Add((i + 1, j - 2));
            }       //i+1; j-2
            if (i - 1 >= 0 && j - 2 >= 0 && map[i - 1][j - 2] != 'X')
            {
                rizzalt.Add((i - 1, j - 2));
            }           //i-1; j-2

            return rizzalt;
        }

        static List<(int, int)> Legrovidebb_ut_lo(string[] map, (int, int) start, (int, int) end)
        {
            int N = map.Length;
            int M = map[0].Length;

            int fehér = 0;
            int kék = 1;
            int piros = 2;

            int[,] szin = new int[N, M];

            Queue<(int, int)> tennivalok = new Queue<(int, int)>();

            tennivalok.Enqueue(start);
            szin[start.Item1, start.Item2] = kék;

            (int, int)[,] honnan = new (int, int)[N, M];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    honnan[i, j] = (-2, -2);
                }
            }
            honnan[start.Item1, start.Item2] = (-1, -1);

            while (tennivalok.Count != 0)
            {
                (int, int) tennivalo = tennivalok.Dequeue();

                // FELDOLGOZÁS

                if (tennivalo == end)
                {
                    return Return_feltoltese(honnan, end);
                }
                szin[tennivalo.Item1, tennivalo.Item2] = piros;

                // SZOMSZÉDAIVAL FOGLALKOZUNK/TÖLTJÜK A TENNIVALÓKAT

                foreach ((int, int) szomszed in Szomszédai_lo(map, tennivalo.Item1, tennivalo.Item2))
                {
                    if (szin[szomszed.Item1, szomszed.Item2] == fehér)
                    {
                        tennivalok.Enqueue(szomszed);
                        szin[szomszed.Item1, szomszed.Item2] = kék;
                        honnan[szomszed.Item1, szomszed.Item2] = tennivalo;
                    }
                }
            }

            return null;
        }
        #endregion

        #region Futó
        static List<(int, int)> Szomszédai_futo(string[] map, int i, int j)
        {
            List<(int, int)> rizzalt = new List<(int, int)>();

            int N = map.Length;
            int M = map[0].Length;

            int tav = 1;

            while (i - tav >= 0 && j - tav >= 0 && map[i - tav][j - tav] != 'X')
            {
                rizzalt.Add((i - tav, j - tav));

                tav++;
            }   //i-1; j-1
            tav = 1;

            while (i - tav >= 0 && j + tav <= M - 1 && map[i - tav][j + tav] != 'X')
            {
                rizzalt.Add((i - tav, j + tav));

                tav++;
            }   //i-1; j+1
            tav = 1;

            while (i + tav <= N - 1 && j + tav <= M - 1 && map[i + tav][j + tav] != 'X')
            {
                rizzalt.Add((i + tav, j + tav));

                tav++;
            }   //i+1; j+1
            tav = 1;

            while (i + tav <= N - 1 && j - tav >= 0 && map[i + tav][j - tav] != 'X')
            {
                rizzalt.Add((i + tav, j - tav));

                tav++;
            }   //i+1; j-1

            return rizzalt;
        }

        static List<(int, int)> Legrovidebb_ut_futo(string[] map, (int, int) start, (int, int) end)
        {
            int N = map.Length;
            int M = map[0].Length;

            int fehér = 0;
            int kék = 1;
            int piros = 2;

            int[,] szin = new int[N, M];

            Queue<(int, int)> tennivalok = new Queue<(int, int)>();

            tennivalok.Enqueue(start);
            szin[start.Item1, start.Item2] = kék;

            (int, int)[,] honnan = new (int, int)[N, M];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    honnan[i, j] = (-2, -2);
                }
            }
            honnan[start.Item1, start.Item2] = (-1, -1);

            while (tennivalok.Count != 0)
            {
                (int, int) tennivalo = tennivalok.Dequeue();

                // FELDOLGOZÁS

                if (tennivalo == end)
                {
                    return Return_feltoltese(honnan, end);
                }
                szin[tennivalo.Item1, tennivalo.Item2] = piros;

                // SZOMSZÉDAIVAL FOGLALKOZUNK/TÖLTJÜK A TENNIVALÓKAT

                foreach ((int, int) szomszed in Szomszédai_futo(map, tennivalo.Item1, tennivalo.Item2))
                {
                    if (szin[szomszed.Item1, szomszed.Item2] == fehér)
                    {
                        tennivalok.Enqueue(szomszed);
                        szin[szomszed.Item1, szomszed.Item2] = kék;
                        honnan[szomszed.Item1, szomszed.Item2] = tennivalo;
                    }
                }
            }

            return null;
        }
        #endregion

        #region Király
        static List<(int, int)> Szomszédai_kiraly(string[] map, int i, int j)
        {
            List<(int, int)> rizzalt = new List<(int, int)>();

            int N = map.Length; //sorok szama
            int M = map[0].Length;  // oszlopok szama

            if (i - 1 >= 0 && map[i - 1][j] != 'X')
            {
                rizzalt.Add((i - 1, j)); //N
            }
            if (j - 1 >= 0 && map[i][j - 1] != 'X')
            {
                rizzalt.Add((i, j - 1)); //W
            }
            if (i + 1 < N - 1 && map[i + 1][j] != 'X') 
            {
                rizzalt.Add((i + 1, j)); //S
            }
            if (j + 1 < M - 1 && map[i][j + 1] != 'X') 
            {
                rizzalt.Add((i, j + 1)); //E
            }
            if (i - 1 >= 0 && j - 1 >= 0 && map[i - 1][j - 1] != 'X')
            {
                rizzalt.Add((i - 1, j - 1)); //NW
            }
            if (i - 1 >= 0 && j + 1 <= M - 1 && map[i - 1][j + 1] != 'X')
            {
                rizzalt.Add((i - 1, j + 1)); //NE
            }
            if (i + 1 <= N - 1 && j + 1 <= M - 1 && map[i + 1][j + 1] != 'X')
            {
                rizzalt.Add((i + 1, j + 1)); //SE
            }
            if (i + 1 <= N - 1 && j - 1 >= 0 && map[i + 1][j - 1] != 'X')
            {
                rizzalt.Add((i + 1, j - 1)); //SW
            }

            return rizzalt;
        }

        static List<(int, int)> Legrovidebb_ut_kiraly(string[] map, (int, int) start, (int, int) end)
        {
            int N = map.Length;
            int M = map[0].Length;

            int fehér = 0;
            int kék = 1;
            int piros = 2;

            int[,] szin = new int[N, M];

            Queue<(int, int)> tennivalok = new Queue<(int, int)>();

            tennivalok.Enqueue(start);
            szin[start.Item1, start.Item2] = kék;

            (int, int)[,] honnan = new (int, int)[N, M];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    honnan[i, j] = (-2, -2);
                }
            }
            honnan[start.Item1, start.Item2] = (-1, -1);

            while (tennivalok.Count != 0)
            {
                (int, int) tennivalo = tennivalok.Dequeue();

                // FELDOLGOZÁS

                if (tennivalo == end)
                {
                    return Return_feltoltese(honnan, end);
                }
                szin[tennivalo.Item1, tennivalo.Item2] = piros;

                // SZOMSZÉDAIVAL FOGLALKOZUNK/TÖLTJÜK A TENNIVALÓKAT

                foreach ((int, int) szomszed in Szomszédai_kiraly(map, tennivalo.Item1, tennivalo.Item2))
                {
                    if (szin[szomszed.Item1, szomszed.Item2] == fehér)
                    {
                        tennivalok.Enqueue(szomszed);
                        szin[szomszed.Item1, szomszed.Item2] = kék;
                        honnan[szomszed.Item1, szomszed.Item2] = tennivalo;
                    }
                }
            }

            return null;
        }
        #endregion

        #region Kiráynő
        static List<(int, int)> Szomszédai_kiralyno(string[] map, int i, int j)
        {
            List<(int, int)> rizzalt = new List<(int, int)>();

            int N = map.Length;
            int M = map[0].Length;

            int tav = 1;

            while (i - tav >= 0 && map[i - tav][j] != 'X')
            {
                rizzalt.Add((i - tav, j)); //N

                tav++;
            }
            tav = 1;

            while (j - tav >= 0 && map[i][j - tav] != 'X')
            {
                rizzalt.Add((i, j - tav)); //W

                tav++;
            }
            tav = 1;

            while (i + tav <= N - 1 && map[i + tav][j] != 'X')
            {
                rizzalt.Add((i + tav, j)); //S

                tav++;
            }
            tav = 1;

            while (j + tav <= M - 1 && map[i][j + tav] != 'X')
            {
                rizzalt.Add((i, j + tav)); //E

                tav++;
            }
            tav = 1;

            while (i - tav >= 0 && j - tav >= 0 && map[i - tav][j - tav] != 'X')
            {
                rizzalt.Add((i - tav, j - tav));

                tav++;
            }   //i-1; j-1
            tav = 1;

            while (i - tav >= 0 && j + tav <= M - 1 && map[i - tav][j + tav] != 'X')
            {
                rizzalt.Add((i - tav, j + tav));

                tav++;
            }   //i-1; j+1
            tav = 1;

            while (i + tav <= N - 1 && j + tav <= M - 1 && map[i + tav][j + tav] != 'X')
            {
                rizzalt.Add((i + tav, j + tav));

                tav++;
            }   //i+1; j+1
            tav = 1;

            while (i + tav <= N - 1 && j - tav >= 0 && map[i + tav][j - tav] != 'X')
            {
                rizzalt.Add((i + tav, j - tav));

                tav++;
            }   //i+1; j-1

            return rizzalt;
        }


        static List<(int, int)> Legrovidebb_ut_kiralyno(string[] map, (int, int) start, (int, int) end)
        {
            int N = map.Length;
            int M = map[0].Length;

            int fehér = 0;
            int kék = 1;
            int piros = 2;

            int[,] szin = new int[N, M];

            Queue<(int, int)> tennivalok = new Queue<(int, int)>();

            tennivalok.Enqueue(start);
            szin[start.Item1, start.Item2] = kék;

            (int, int)[,] honnan = new (int, int)[N, M];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    honnan[i, j] = (-2, -2);
                }
            }
            honnan[start.Item1, start.Item2] = (-1, -1);

            while (tennivalok.Count != 0)
            {
                (int, int) tennivalo = tennivalok.Dequeue();

                // FELDOLGOZÁS

                if (tennivalo == end)
                {
                    return Return_feltoltese(honnan, end);
                }
                szin[tennivalo.Item1, tennivalo.Item2] = piros;

                // SZOMSZÉDAIVAL FOGLALKOZUNK/TÖLTJÜK A TENNIVALÓKAT

                foreach ((int, int) szomszed in Szomszédai_kiralyno(map, tennivalo.Item1, tennivalo.Item2))
                {
                    if (szin[szomszed.Item1, szomszed.Item2] == fehér)
                    {
                        tennivalok.Enqueue(szomszed);
                        szin[szomszed.Item1, szomszed.Item2] = kék;
                        honnan[szomszed.Item1, szomszed.Item2] = tennivalo;
                    }
                }
            }

            return null;
        }
        #endregion

        static List<(int, int)> Return_feltoltese((int, int)[,] honnan, (int, int) end)
        {
            List<(int, int)> rizzalt = new List<(int, int)>();
            (int, int) node = end;

            while (honnan[node.Item1, node.Item2] != (-1, -1))
            {
                rizzalt.Add(node);
                node = honnan[node.Item1, node.Item2];
            }

            rizzalt.Reverse();
            return rizzalt;
        }

        static void Kiir(string[] map, List<(int, int)> ut)
        {
            int N = map.Length;
            int M = map[0].Length;

            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    if (ut.Contains((i, j)))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("o");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.Write(map[i][j]);
                    }
                }
                Console.WriteLine();
            }
        }

        static void Paraszt(string[] map, (int, int) S, (int, int) E)
        {
            List<(int, int)> ut = Legrovidebb_ut_paraszt(map, S, E);

            Console.WriteLine(string.Join(" -> ", ut));

            Kiir(map, ut);
        }

        static void Bastya(string[] map, (int, int) S, (int, int) E)
        {
            List<(int, int)> ut = Legrovidebb_ut_bastya(map, S, E);

            Console.WriteLine(string.Join(" -> ", ut));

            Kiir(map, ut);
        }

        static void Lo(string[] map, (int, int) S, (int, int) E)
        {
            List<(int, int)> ut = Legrovidebb_ut_lo(map, S, E);

            Console.WriteLine(string.Join(" -> ", ut));

            Kiir(map, ut);
        }

        static void Futo(string[] map, (int, int) S, (int, int) E)
        {
            List<(int, int)> ut = Legrovidebb_ut_futo(map, S, E);

            Console.WriteLine(string.Join(" -> ", ut));

            Kiir(map, ut);
        }

        static void Kiraly(string[] map, (int, int) S, (int, int) E)
        {
            List<(int, int)> ut = Legrovidebb_ut_kiraly(map, S, E);

            Console.WriteLine(string.Join(" -> ", ut));

            Kiir(map, ut);
        }

        static void Kiralyno(string[] map, (int, int) S, (int, int) E)
        {
            List<(int, int)> ut = Legrovidebb_ut_kiralyno(map, S, E);

            Console.WriteLine(string.Join(" -> ", ut));

            Kiir(map, ut);
        }

        static void Main(string[] args)
        {
            string[] map = File.ReadAllLines("input.txt");

            // Console.WriteLine(string.Join(",", Szomszédai(map, 7, 5)));

            /** /
            (int, int) S = (7, 5);
            (int, int) E = (11, 16);
            List<(int, int)> ut = Legrovidebb_ut(map, S, E);

            Console.WriteLine(string.Join(" -> ", ut));

            Kiir(map, ut);
            /**/


            Kiralyno(map, (7, 5), (11, 16));
        }

        /*
        ..........X........
        ..XXXXXXX.X..X.....
        ..X.....X.X..X.....
        ..X.....X.X..X.....
        ........X....X.X...
        ........X....X.X...
        ...X....X....X.X...
        ...X.S..X....X.X...
        .X.X....X....X.XXXX
        .X.XXXXXXXXXXX.X...
        .X......X......X...
        .X......X...X..XE..
        .XXXXXXXX...X..XXX.
        ............X......
        ............X......
        ..........XXXXXXXXX
        ........X..........
        ........X..........
        */
    }
}
