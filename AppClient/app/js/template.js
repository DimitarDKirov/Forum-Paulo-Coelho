import Handlebars from 'handlebars';
import 'jquery';

var template = (function () {

	function getTemplate(name) {
		var promise = new Promise(function (resolve, reject) {
			$.ajax({
				url: 'app/templates/' + name + '.handlebars',
				method: 'GET',
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
	return {
		get: getTemplate,
	}
} ())

export default template;