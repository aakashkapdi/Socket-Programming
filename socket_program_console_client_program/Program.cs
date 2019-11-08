using System.Net;
using System.Net.Sockets;
using System;
using System.Text;

namespace socket_program_console_client_program
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipaddr = null;
            int IntPortInput = 0;
            
            try
            {
                int flag_ip, flag_port;
                Console.WriteLine("Welcome");
                do {
                    flag_ip = 0;
                    Console.WriteLine("Enter Valid IP address and Enter");
                    string strIpaddr = Console.ReadLine();
                    if (!IPAddress.TryParse(strIpaddr, out ipaddr))
                    {
                        Console.WriteLine("Invalid Ip Address");
                        flag_ip = 1;
                    }
                } while (flag_ip != 0);
                do
                {
                    flag_port = 0;
                    Console.WriteLine("Enter Valid port number and Enter");
                    string strPortInput = Console.ReadLine();
                    if (!int.TryParse(strPortInput.Trim(), out IntPortInput))
                    {
                        Console.WriteLine("Invalid Ip Address");
                        flag_port = 1;
                    }
                } while (flag_port != 0);

                Console.WriteLine(ipaddr.ToString() +IntPortInput.ToString());
                client.Connect(ipaddr, IntPortInput);
                Console.WriteLine("Connected to server");
                string input = string.Empty;
                while (true)
                {
                    input = Console.ReadLine();
                    if (input.Equals("<Exit>"))
                        break;
                    byte[] buffSend=Encoding.ASCII.GetBytes(input);

                    client.Send(buffSend);
                    byte[] buffReceived = new byte[128];
                    int nrec = client.Receive(buffReceived);
                    string s_rec=Encoding.ASCII.GetString(buffReceived, 0, nrec);
                    Console.WriteLine("Data received= " + s_rec);  
                }
                Console.ReadKey();
                

            }

            catch(Exception ee)
            {
                Console.WriteLine(ee.ToString());
                return;
            }

            finally
            {
                if (client != null)
                {
                    if(client.Connected==true)
                       client.Shutdown(SocketShutdown.Both);
                    client.Close();
                    client.Dispose();
                }
            }

            Console.WriteLine("Press a key to exit");
            Console.ReadKey();
        }
    }
}
