import data from 'data';
import template from 'template';
import htmlRenderer from 'htmlRenderer';
import toastr from 'toastr';

var postsController = (function () {

	function getAll(context){
		var postsResponse;
		data.posts.all()
		.then(function(res){
			console.log(res);
		},function(err){
			console.log(err);
			toastr.error(err);
		})
	}
	
	function createPost(context, container){
		var threadId = context.params.id;
	    template.get("create-post")
		.then(function(createPostTemplate){
			 return htmlRenderer.renderTemplate(createPostTemplate, { thread: threadId });
		})
		.then(function(html){
			container.html(html);
		})
	}
	
	function create(context, container){
		var threadId = context.params.id;
		
		data.posts.create(threadId).
		then(function(data){
			
		});
	}
	
	return {
		all: getAll,
		create: createPost,
		createPost : create
	}
} ());

export default postsController;