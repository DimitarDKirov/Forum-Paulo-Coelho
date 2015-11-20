# Team Paulo-Coelho

----------
### Source control repository URI:

[Forum-Paulo-Coelho](https://github.com/DimitarDKirov/Forum-Paulo-Coelho) 


### Models Class Diagram

<img class="slide-image" src="ForumSystemClassDiagram.png" style="width:100%; top:55%; left:10%" />


### API Controllers:

## ThreadsController

----------
**Methods:**

 * **GetAll()** - GET
 * **GetById(int id)** - GET
 * **Post(ThreadRequestModel requestThread)** - POST [Authorize]
 * **GetByCategory(int categoryId)** - GET

**Endpoints:**

* **api/threads** - GET method to return list of threads in the system in format
*
     [{
        "Id": 1,
        "Title": "Lections",
        "Content": "telerik content",
        "DateCreated": "2015-11-19T09:19:38.753",
        "Creator": "admin"
      },
      {
        "Id": 2,
        "Title": "Web Services Exam",
        "Content": "academy questions",
        "DateCreated": "2015-11-20T07:00:20.237",
        "Creator": "admin"
      },
      {
        "Id": 3,
        "Title": "DSA Exam",
        "Content": "academy questions",
        "DateCreated": "2015-11-20T07:05:52.393",
        "Creator": "admin"
      }]

* **api/threads/{id}** - GET method to find information about the thread with the given id. If is found returns OK with content

 {
      "Id": 2,
      "Title": "Web Services Exam",
      "Content": "academy questions",
      "DateCreated": "2015-11-20T07:00:20.237",
      "Creator": "admin"
  }
 If not found returns Bad Request. 

* **api/threads?categoryId={id}** - GET method to return list of all threads in given category.
* **api/threads** - POST method to add thread. Request body is in the form

    `{
        "title":"Web Services Exam",
        "content":"academy questions"
    }`
Both fields are required. Returns OK if addition was successful and the content of added thread

{
  "Id": 3,
  "Title": "Web Services Exam",
  "Content": "academy questions",
  "DateCreated": "2015-11-20T07:05:52.3939387+00:00",
  "Creator": "admin@abv.bg"
}

Otherwise returns BadRequest.
 
----------
## PostsController

----------
**Methods:**

 * **Get(int id)** - GET
 * **GetByThread(int threadId)** - GET
 * **Add(int threadId, PostsRequestModel post)** - POST [Authorize]
 * **GetByUser()** - GET
 * **Update(int id, PostsRequestModel post)** - PUT [Authorize]
 
**Endpoints:**

* **api/posts** - GET
* **api/posts/1** - GET
* **api/posts/1 **- PUT
* **api/posts?threadId=1** - GET
* **api/posts?threadId=1** - POST
 
----------
## CommentsController

----------
**Methods:**

 * **Create(int id, CommentDataModel model)** - POST [Authorize]
  
**Endpoints:**

* **api/comments** - POST

----------
## CategoriesController

----------
**Methods:**

 * **Get()** - GET
 * **Add(string name)** - POST [Authorize]
 * **Update(int id, string name)** - PUT [Authorize]
 
**Endpoints:**

* **api/categories** - GET method, returns JSON object with all categories
*
     [  {
        "Threads": [],
        "Id": 1,
        "Name": "WebServices"
      },
      {
        "Threads": [......],
        "Id": 2,
        "Name": "C"
      } ]

* **/api/categories/<id>?name=<new name>** - PUT method, updates the name of the category with the given id. Returns OK in success or Bad Request category with the given id does not exist.
* **/api/categories?name=<category name>** - POST method, adds category. Name is 50 characters long max. 

----------

## NotificationsController

----------
**Methods:**

 * **Get()** - GET
 
**Endpoints:**

* **api/notifications** - GET

----------

## User Accounts Management

-----------
User accounts are managed by the integrated ASP.NET MVC authentication engine

**Registration:**
 * **/api/account/register** - request content should be JSON object like

    {
        "email":"user@gmail.com",
        "password":"...<pass>...", // at least 6 characters long
        "ConfirmPassword":"...<pass>...",
        "nickname":"user"
    }
Returns OK if account creation is successful.
**Logging in**
 * **/token** - requires following pairs to be passed in the request body as x-www-wwwform-urlencoded:

	* username - user@gmail.com
	* password - ....<pass>...
	* grant_type - password
On success it returns OK with JSON object whicn contains token.

      { 
	 "access_token": ....<token>...
         "token_type": "bearer",
         "expires_in": 1209599,
         "userName": "user@gmail.com",
         ".issued": "Fri, 20 Nov 2015 06:01:46 GMT",
         ".expires": "Fri, 04 Dec 2015 06:01:46 GMT"
       }

This token should then be passed as Authorization header in each request where authorization is required like pair:

    Authorization : Bearer <token>

## Message Queue

* **iron.io**

 Notifications post message functionality is implementet in Post controller and get message functionality in Notifications controller.

### Unit Tests:

* [Api Tests](https://github.com/DimitarDKirov/Forum-Paulo-Coelho/tree/master/Forum-Paulo-Coelho/Tests/ForumSystem.Api.Tests) 

* [Services Tests](https://github.com/DimitarDKirov/Forum-Paulo-Coelho/tree/master/Forum-Paulo-Coelho/Tests/ForumSystem.Services.Test) 





