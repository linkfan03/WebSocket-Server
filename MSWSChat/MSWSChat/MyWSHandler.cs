using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.WebSockets;

namespace MSWSChat
{
    public class MyWSHandler : WebSocketHandler
    {
        private static WebSocketCollection clients = new WebSocketCollection();
        public override void OnOpen()
        {
            
            base.Send("Welcome from " + this.WebSocketContext.UserHostAddress);
            clients.Add(this);
        }

        public override void OnMessage(string message)
        {
            //If we send some predefined string such as "DISCONNECT" and remove that client from the list of client
            //we can keep the bicyclist, student and roadworker as strictly a sender
            string msgBack = string.Format(
                "You have sent {0} at {1} with {2}", message, DateTime.Now.ToLongTimeString(), Guid.NewGuid().ToString());
            //base.Send(msgBack);
            clients.Broadcast(message);
        }

        public override void OnClose()
        {
            clients.Remove(this);
            base.OnClose();
        }

        public override void OnError()
        {
            clients.Remove(this);
            base.OnError();
        }
    }
}