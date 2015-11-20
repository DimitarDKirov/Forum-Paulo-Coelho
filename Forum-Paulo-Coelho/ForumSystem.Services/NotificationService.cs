using ForumSystem.Data;
using ForumSystem.Models;
using ForumSystem.Services.Contracts;
using IronMQ;
using IronMQ.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.Services
{
    public class NotificationService : INotificationService
    {
        private readonly string MessageQueueProjectId = ConfigurationManager.AppSettings["MessageQueueProjectId"];// "5649969d4aa03100090000b2";
        private readonly string MessageQueueToken = ConfigurationManager.AppSettings["MessageQueueToken"];// "j46Yol8vc3puwszWc9O3";
        private readonly string MessageQueueName = ConfigurationManager.AppSettings["QueueName"];
        private readonly IRepository<User> usersRepository;

        public NotificationService(IRepository<User> users)
        {
            this.usersRepository = users;
        }

        public IQueryable<string> All(string username)
        {
            Client client = new Client(MessageQueueProjectId, MessageQueueToken);
            Queue queue = client.Queue(MessageQueueName);

            var currentUser = this.usersRepository
                .All()
                .FirstOrDefault(u => u.UserName == username);

            List<string> messages = new List<string>();
            Message msg = queue.Get();

            while (msg != null)
            {
                string messageContent = msg.Body;
                if (!messageContent.StartsWith(currentUser.Nickname))
                {
                    messages.Add(messageContent);
                    queue.DeleteMessage(msg);
                }
                
                msg = queue.Get();
            }

            return messages.AsQueryable();
        }
    }
}
