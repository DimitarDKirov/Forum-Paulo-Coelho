import 'jquery';
import 'jqueryUI';
import 'bootstrap';
import 'timepicker';
import Sammy from 'sammy';
import threadsController from 'threadsController';
import postsController from 'postsController';
import userController from 'userController';
import data from 'data';
import toastr from 'toastr';

export function init() {

	var app = Sammy('#content', function () {
		var $container = $('#content');

		this.get('#/home', function (context) {
			console.log("home");
		})
		
		this.get('#/register', function(context){
			userController.register(context, $container);
		})
		
		this.get('#/login', function(context){
			userController.login(context, $container);
		})
		
		this.get('#/logout', function(context){
			userController.logout(context, $container);
		})

		this.get('#/threads', function(context){
			threadsController.all($container);
		} );
		
		this.get('#/posts', postsController.all);
		
		this.get('#/posts-create/:id', function(context){
			 postsController.create(context, $container);
		});
		
		this.get('#/posts/:id', function(context){
			 postsController.createPost(context, $container);
		});
	})

	$(function () {
		app.run('#/');
	})
}