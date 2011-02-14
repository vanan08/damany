using System;
using System.IO;
using System.IO.Pipes;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using RBSPAdapter_COM;

class Program
{
    static string NAME = "Demo";
    static ManualResetEvent evt = new ManualResetEvent(false);

    static void Server()
    {

        while (true)
        {
            var binaryFormatter = new BinaryFormatter();
            using (NamedPipeServerStream server = new NamedPipeServerStream(NAME, PipeDirection.InOut))
            {
                evt.Set();
                WriteGreen("Waiting for connection ...");

                server.WaitForConnection();
                WriteGreen("Connection established.");

                using (StreamReader sr = new StreamReader(server))
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(server))
                        {
                            int i = 0;
                            while (true)
                            {
                                var s = sr.ReadLine();

                                if (s == null)
                                {
                                    break;
                                }

                                Console.WriteLine("------received query----------\r\n");
                                //if (_q == null)
                                RSBPAdapterCOMObjClass _q;
                                _q = new RSBPAdapterCOMObjClass();
                                _q.queryCondition = string.Format("sfzh='{0}'", s);
                                _q.queryType = "QueryQGRK";
                                var result = _q.queryCondition;
                                Console.WriteLine(result + "\r\n" + "---------------");

                                _q.queryCondition = string.Format("sfzh='{0}'", s);
                                _q.queryType = "QueryZTK";
                                result = _q.queryCondition;
                                Console.WriteLine(result + "\r\n" + "---------------");

                                System.Runtime.InteropServices.Marshal.ReleaseComObject(_q);

                                WriteGreen("SERVER: Received {0}, sent {1}.", i, s);
                                binaryFormatter.Serialize(server, result);
                                //sw.WriteLine(result.Substring(0, 10));
                                //sw.Flush();
                                server.Flush();
                                Thread.Sleep(1000);
                            }
                        }
                    }
                    catch (System.IO.IOException)
                    {
                        break;
                    }
                   
                }

            }
        }
    }

    static void Main(string[] args)
    {
        new Thread(Server).Start();

        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
        return;

        evt.WaitOne();

        using (NamedPipeClientStream client = new NamedPipeClientStream(".", NAME, PipeDirection.InOut))
        {
            WriteRed("Connecting to server ...");
            client.Connect();
            WriteRed("Connected.");

            using (StreamWriter sw = new StreamWriter(client))
            {
                using (StreamReader sr = new StreamReader(client))
                {
                    Random rand = new Random();

                    while (true)
                    {
                        int i = rand.Next(1000);
                        sw.WriteLine(i);
                        sw.Flush();
                        string s = sr.ReadLine();
                        WriteRed("CLIENT:  Sent {0}, received {1}.", i, s);
                    }
                }
            }
        }
    }

    static void WriteRed(string msg, params object[] p)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(msg, p);
        Console.ResetColor();
    }

    static void WriteGreen(string msg, params object[] p)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(msg, p);
        Console.ResetColor();
    }
}