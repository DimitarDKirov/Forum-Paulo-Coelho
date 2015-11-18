# Team Paulo-Coelho


----------
### Team members:

* **Dimitar Kirov - dimkirov**
* **Todor Dimitrov - TodorDimitrov**
* **Rosen Todorov - RosenTodorov**


### Source control repository URI:

[Forum-Paulo-Coelho](https://github.com/DimitarDKirov/Forum-Paulo-Coelho) 

### Project purpose:

Internet forum Web Api, where people can open new topic.Each topic , users can make posts. Also, depending on the access level of the user it has a different level of access.Only authorized users can add new topics , posts or comments to them.
All users can see the entire contents of the forum.
Service support all restful operation like **PUT**, **GET**, **POST** and **DELETE**.

### Models Class Diagram


![]("ForumSystemClassDiagram.png")


### API Controllers:

## ThreadsController

----------
**Methods:**

 * **GetAll()** - GET
 * **GetById(int id)** - GET
 * **Post(ThreadRequestModel requestThread)** - POST [Authorize]
 * **GetByCategory(int categoryId)** - GET
 
----------
## PostsController

----------
**Methods:**

 * **Get(int id)** - GET
 * **GetByThread(int threadId)** - GET
 * **Add(int threadId, PostsRequestModel post)** - POST [Authorize]
 * **GetByUser()** - GET
 * **Update(int id, PostsRequestModel post)** - PUT [Authorize]
 
----------
## CommentsController

----------
**Methods:**

 * **Create(int id, CommentDataModel model)** - POST [Authorize]
 
----------
## CategoriesController

----------
**Methods:**

 * **Get()** - GET
 * **Add(string name)** - POST [Authorize]
 * **Update(int id, string name)** - PUT [Authorize]
 
----------

### Tests:

* [Api Tests](https://github.com/DimitarDKirov/Forum-Paulo-Coelho/tree/master/Forum-Paulo-Coelho/Tests/ForumSystem.Api.Tests) 

* [Services Tests](https://github.com/DimitarDKirov/Forum-Paulo-Coelho/tree/master/Forum-Paulo-Coelho/Tests/ForumSystem.Services.Test) 




