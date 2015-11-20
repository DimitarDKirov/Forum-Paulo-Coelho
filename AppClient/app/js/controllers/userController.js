import data from 'data';
import template from 'template';
import 'jquery';
import toastr from 'toastr';

var userController = (function () {

	function userRegister(context) {
		template.get('register')
			.then(function (html) {
				context.$element().html(html);

				$('#content').on('click', '#register-button', function (ev) {

					var username = $('#register-username').val();
					var password = $('#register-password').val();
					var nickname = $('#register-nickname').val();

					var user = {
						email: username,
						password: password,
						confirmPassword: password,
						nickname: nickname
					}

					data.users.register(user)
						.then(function (res) {
							toastr.success('Register success!');
							context.redirect('#/threads');
						}, function (err) {
							toastr.error(err);
						})

					return false;
				})

			}, function (err) {
				toastr.error(err);
			})

	}

	function userLogin(context, container) {

		template.get('login')
			.then(function (html) {
				context.$element().html(html);

				$('#content').on('click', '#login', function (ev) {

					var username = $('#login-username').val();
					var password = $('#login-password').val();

					var user = {
						grant_type: "password",
						username: username,
						password: password,
					}

					data.users.login(user)
						.then(function (res) {
							toastr.success('Login success!');
							context.redirect('#/');
						}, function (err) {
							toastr.error(err);
						})

					return false;
				})
			}, function (err) {
				toastr.error(err);
			})
	}

	function userLogout() {
		data.users.logout()
			.then(function () {
				toastr.success('Logout success!');
			})
	}


	return {
		register: userRegister,
		login: userLogin,
		logout: userLogout
	}
} ())

export default userController;