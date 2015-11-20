import Handlebars from 'handlebars';

var htmlRenderer = (function(){

    var htmlRenderer = {};
    htmlRenderer.renderTemplate = function(htmlTemplate, data){

        var promise = new Promise(function(resolve, reject){
            var source = htmlTemplate;
            var template = Handlebars.compile(source);

            var html = template(data);
            resolve(html);
        })
        
        return promise;
    }

    return htmlRenderer;
}())

export default htmlRenderer;
