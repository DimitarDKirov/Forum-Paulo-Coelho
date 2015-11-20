import 'jquery';

var data = (function () {

	var baseAddress = 'http://forumpaulocoelho.azurewebsites.net/api';
	var sessionKey = localStorage.getItem('token');
    var username = localStorage.getItem('username');

    function clearUserData() {
        localStorage.removeItem('username');
        localStorage.removeItem('token');
        username = '';
        sessionKey = '';
    };

    function saveUserData(userData) {
        localStorage.setItem('username', userData.username);
        localStorage.setItem('token', userData.token);
        username = userData.username;
        sessionKey = userData.token;
    };

	function register(user) {
		var promise = new Promise(function (resolve, reject) {
			$.ajax({
				url: baseAddress + '/account/register',
				data: JSON.stringify(user),
				method: 'POST',
				contentType: 'application/json',
				success: function (data) {
					resolve(data);
				},
				error: function (err) {
					reject(err);
				}
			})
		});

		return promise;
	}

	function login(user) {
		var promise = new Promise(function (resolve, reject) {
			$.ajax({
				//url: 'http://forumpaulocoelho.azurewebsites.net/Token?grant_type=password&username=' + user.username + '&password=' + user.password,
				url: "http://forumpaulocoelho.azurewebsites.net/token",
				data: user,
				method: 'POST',
				//contentType: 'x-www-form-urlencoded',
				success: function (data) {
					//setCookie(data);
					var response = "Bearer " + data.access_token;
					var userData = {
						username: data.userName,
						token: response
					}
					saveUserData(userData);
					resolve(data);
				},
				error: function (err) {
					reject(err);
				}
			})
		});

		return promise;
	}

	function logout() {
		var promise = new Promise(function (resolve, reject) {

			var header = {
				'x-auth-key': sessionKey
			}

			$.ajax({
				url: 'api/users',
				//headers: header,
				method: 'PUT',
				success: function (data) {
					//clearCookie(currentUser());
					clearUserData();
					resolve(data);
				},
				error: function (err) {
					reject(err);
				}
			})
		})

		return promise;
	}

	function getThreads() {
		var promise = new Promise(function (resolve, reject) {
			$.ajax({
				url: baseAddress + '/threads',
				method: 'GET',
				contentType: 'application/json',
				success: function (data) {
					resolve(data);
				},
				error: function (err) {
					reject(err);
				}
			})
		});

		return promise;
	}

	function getPosts() {
		var promise = new Promise(function (resolve, reject) {
			$.ajax({
				url: baseAddress + '/posts/1',
				method: 'GET',
				contentType: 'application/json',
				success: function (data) {
					resolve(data);
				},
				error: function (err) {
					reject(err);
				}
			})
		});

		return promise;
	}

	function createPost(id) {
		var promise = new Promise(function (resolve, reject) {
			$.ajax({
				url: baseAddress + '/posts?threadId=' + id,
				method: 'POST',
				contentType: 'application/json',
				success: function (data) {
					console.log(data);
					resolve(data);
				},
				error: function (err) {
					reject(err);
				}
			})
		});

		return promise;
	}

	return {
		threads: {
			all: getThreads
		},
		posts: {
			all: getPosts,
			create: createPost
		},
		users: {
			login: login,
			register: register,
			logout: logout
		}
	}
} ());

export default data;