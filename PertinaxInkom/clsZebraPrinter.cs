using PertinaxInkom.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PertinaxInkom
{
    public class clsZebraPrinter
    {
        public static string GenerateZPL(string edition, string nickname, string firstname, string lastname, string barcode)
        {
            return ZebraConfig.Default.zplcode.ToString();
        }

        public static string SendZPLToPrinter(string zplCode, string printer)
        {
            try
            {
                // Create a TCP client
                using (TcpClient client = new TcpClient(printer, 9100))
                {
                    // Translate the passed message into ASCII and store it as a byte array
                    byte[] zplData = Encoding.ASCII.GetBytes(zplCode);

                    // Get a client stream for reading and writing
                    using (NetworkStream stream = client.GetStream())
                    {
                        // Send the message to the connected TcpServer
                        stream.Write(zplData, 0, zplData.Length);
                    }
                }

                return "succes";
            }
            catch (Exception ex)
            {
                return ($"Failed to send ZPL to printer: {ex.Message}");
            }
        }
    }
}
