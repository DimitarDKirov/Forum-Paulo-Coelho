namespace ForumSystem.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using ForumSystem.Services.Contracts;

    public class NotificationsController : ApiController
    {
        private readonly INotificationService notifications;

        public NotificationsController(INotificationService notifications)
        {
            this.notifications = notifications;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            string username = this.User.Identity.Name;

            var userNotifications = this.notifications
                .All(username)
                .ToList();

            return Ok(userNotifications);
        }
    }
}
