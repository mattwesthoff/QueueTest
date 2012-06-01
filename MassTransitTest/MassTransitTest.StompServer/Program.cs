using System;

namespace MassTransitTest.StompServer
{
    class Program
    {
        static void Main(string[] args)
        {
            StartWebsocketServer();
        }

        private static void StartWebsocketServer()
        {
            Console.Out.WriteLine("starting the websockets service");

            var wsListener = new Ultralight.Listeners.StompWebsocketListener("ws://localhost:61614/");

            wsListener.OnConnect
               += stompClient =>
               {
                   Console.WriteLine("a new client connected!");
                   stompClient.OnMessage += msg => Console.Out.WriteLine("msg received: {0} {1}", msg.Command, msg.Body);
               };
            
            var server = new Ultralight.StompServer(wsListener);
            server.Start();

            Console.WriteLine("ready... type 'exit' to stop");
            while (Console.ReadLine() != "exit")
            {
                Console.WriteLine("type 'exit' to stop");                
            }
            
        }
    }
}
