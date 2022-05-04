using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HTTP_Server
{
    public class Response
    {
        private Byte[] _data = null;
        private String _status;
        private String _mime;

        private Response(String status, String mime, Byte[] data)
        {
            _status = status;
            _data = data;
            _mime = mime;
        }

        public static Response From(Request request)
        {
            if (request == null)
                return MakeNullRequest();
            if(request.Type == "GET")
            {

            }
            else
            {

            }
        }

        private static Response MakeNullRequest()
        {
            String file = Environment.CurrentDirectory + HTTPServer.MSG_DIR + "400.html";
            FileInfo fi = new FileInfo(file);
            FileStream fs = fi.OpenRead();
            BinaryReader reader = new BinaryReader(fs);
            Byte[] d = new Byte[fs.Length];
            reader.Read(d, 0, d.Length);


            return new Response("400 Bad Request", "html/text", d);
        }

        private static Response MakePageNotFound()
        {
            String file = Environment.CurrentDirectory + HTTPServer.MSG_DIR + "404.html";
            FileInfo fi = new FileInfo(file);
            FileStream fs = fi.OpenRead();
            BinaryReader reader = new BinaryReader(fs);
            Byte[] d = new Byte[fs.Length];
            reader.Read(d, 0, d.Length);


            return new Response("404 Page Not Found", "html/text", d);
        }

        public void Post(NetworkStream stream)
        {
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(String.Format("{0} {1}\r\nServer: {2}\r\nContent-Type: {3}\r\nAccept-Ranges: bytes\r\nContent=Length: {4}\r\n",
                HTTPServer.VERSION, _status, HTTPServer.NAME, _mime, _data.Length));

            stream.Write(_data, 0, _data.Length);
        }
    }
}
