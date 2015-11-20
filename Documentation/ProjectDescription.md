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
```json
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
```
     

* **api/threads/{id}** - GET method to find information about the thread with the given id. If is found returns OK with content

```json
 {
      "Id": 2,
      "Title": "Web Services Exam",
      "Content": "academy questions",
      "DateCreated": "2015-11-20T07:00:20.237",
      "Creator": "admin"
  }
```
  
 If not found returns Bad Request. 

* **api/threads?categoryId={id}** - GET method to return list of all threads in given category.
* **api/threads** - POST method to add thread. Request body is in the form

```json
    {
        "title":"Web Services Exam",
        "content":"academy questions"
    }
```
Both fields are required. Returns OK if addition was successful and the content of added thread

```json
 {
      "Id": 3,
      "Title": "Web Services Exam",
      "Content": "academy questions",
      "DateCreated": "2015-11-20T07:05:52.3939387+00:00",
      "Creator": "admin@abv.bg"
    }
```

Otherwise returns BadRequest.
 
----------
## PostsController

----------
**Methods:**

 * **Get(int id)** - GET 
 * **Add(int threadId, PostsRequestModel post)** - POST [Authorize]
 * **GetByUser()** - GET
 * **GetByThread(int threadId)** - GET
 * **Update(int id, PostsRequestModel post)** - PUT [Authorize]
 * **Delete(int id)** - DELETE [Authorize]
 
**Endpoints:**

* **api/posts** - GET method to return all posts created by the currently logged user
* **api/posts/1** - GET method which returns OK with information about the post with the given id
* **api/posts/1**- PUT method to update the post with the given id. Request should contain the new content. Returns OK if update was successful otherwise BadRequest.
* **api/posts?threadId=1** -   GET method to return all posts for the thread with the given id. If successful returns OK and list of all posts found 

```json
[
  {
    "Id": 1,
    "PostDate": "2015-11-20T07:41:22.37",
    "Content": "Will the exam be easy",
    "NickName": "user",
    "ThreadTitle": "Web Services Exam",
    "Comments": []
  },
  {
    "Id": 2,
    "PostDate": "2015-11-20T07:42:49.517",
    "Content": "Does anybody knows when the exam will be",
    "NickName": "user",
    "ThreadTitle": "Web Services Exam",
    "Comments": [
      {
        "Id": 1,
        "UserName": "user@gmail.com",
        "CommentDate": "2015-11-20T08:05:07.293",
        "Content": "I do not know?",
        "PostId": 2
      },
      {
        "Id": 2,
        "UserName": "user@gmail.com",
        "CommentDate": "2015-11-20T08:05:44.307",
        "Content": "Ask the trainers",
        "PostId": 2
      }
    ]
  }
] 
```

* **api/posts?threadId=1** - POST method to add post to the thread with the given id. Request should contain content with length 10 to 2000 symbols:
* **api/posts/3** - DELETE method to delete the post with the given id. It is allowed only for the user who created the post. Returns OK if delete was successful or BadRequest otherwise.

```json
{
"content":"Will the exam be easy?"
}
```

Returns OK if the operation was successful and Bad request if the thread was not found or content's length was not fulfilled.
 
----------
## CommentsController
----------
**Methods:**

 * **Create(int id, CommentDataModel model)** - POST [Authorize]
  
**Endpoints:**

* **api/comments?postid=2** - POST method to add comment for the post post with the given id. Requires content to be provided

```json
    {
        "content":"Ask the trainers"
    }
```

Returns OK if request was successful or BadRequest otherwise

----------
## CategoriesController

----------
**Methods:**

 * **Get()** - GET
 * **Add(string name)** - POST [Authorize]
 * **Update(int id, string name)** - PUT [Authorize]
 
**Endpoints:**

* **api/categories** - GET method, returns JSON object with all categories

```json
     [  {
        "Threads": [],
        "Id": 1,
        "Name": "WebServices"
      },
      {
        "Threads": [],
        "Id": 2,
        "Name": "C"
      } ]
```

* **/api/categories/<id>?name=<new name>** - PUT method, updates the name of the category with the given id. Returns OK in success or Bad Request category with the given id does not exist.
* **/api/categories?name=<category name>** - POST method, adds category. Name is 50 characters long max. 

----------

## NotificationsController

----------
**Methods:**

 * **Get()** - GET
 
**Endpoints:**

* **api/notifications** - GET method which returns messages about users who added posts to threads, created by the currently logged used. Implemented with Iron MQ.
```json
[
  "user added post on your thread.",
  "user added post on your thread.",
  "user added post on your thread."
]
```
----------

## User Accounts Management

-----------
User accounts are managed by the integrated ASP.NET MVC authentication engine

**Registration:**
 * **/api/account/register** - request content should be JSON object like

```json
    {
        "email":"user@gmail.com",
        "password":"...<pass>...", 
        "ConfirmPassword":"...<pass>...",
        "nickname":"user"
    }
```
Password is at least 6 characters long. Nickname must be 25 chracters long max
Returns OK if account creation is successful.

**Logging in**
 * **/token** - requires following pairs to be passed in the request body as x-www-wwwform-urlencoded:

```json
	 username - user@gmail.com
	 password - ....<pass>...
	 grant_type - password
```
On success it returns OK with JSON object whicn contains token.

```json
      { 
	 "access_token": ....<token>...
         "token_type": "bearer",
         "expires_in": 1209599,
         "userName": "user@gmail.com",
         ".issued": "Fri, 20 Nov 2015 06:01:46 GMT",
         ".expires": "Fri, 04 Dec 2015 06:01:46 GMT"
       }
```
       

This token should then be passed as Authorization header in each request where authorization is required like pair:

    Authorization : Bearer <token>

## Message Queue

* **iron.io**

 Notifications post message functionality is implementet in Post controller and get message functionality in Notifications controller.

### Unit Tests:

* [Api Tests](https://github.com/DimitarDKirov/Forum-Paulo-Coelho/tree/master/Forum-Paulo-Coelho/Tests/ForumSystem.Api.Tests) 

* [Services Tests](https://github.com/DimitarDKirov/Forum-Paulo-Coelho/tree/master/Forum-Paulo-Coelho/Tests/ForumSystem.Services.Test) 





