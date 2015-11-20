import data from 'data';
import template from 'template';
import htmlRenderer from 'htmlRenderer';
import toastr from 'toastr';

var threadsController = (function () {

	function getAll(context) {
		var threadsResponse;
		data.threads.all()
			.then(function (res) {
				threadsResponse = res;
				return template.get("threads");
			}, function (err) {
				toastr.error(err);
			})
			.then(function (threadsTemplate) {
				 return htmlRenderer.renderTemplate(threadsTemplate, { threads: threadsResponse });
			})
			.then(function(html){
				context.html(html);
			})
	}
	return {
		all: getAll
	}
} ());

export default threadsController;