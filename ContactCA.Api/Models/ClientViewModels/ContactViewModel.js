var postViewModel = function (blogPostId) {

   var self = this;

   self.firstName = ko.observable(null);
   self.lastName = ko.observable('');
   self.email = ko.observable('');
   self.telephone = ko.observable('');
   self.bestTimeToCall = ko.observable('');
   self.message = ko.observable('');

   self.submit = function () {
      self.message('');
      var model = {
         FirstName: self.firstName(),
         LastName: self.email(),
         Email: self.comment(),
         Telephone: self.blogPost().BlogPostId(),
         BestTimeToCall: self.bestTimeToCall
      }
      $.post('/api/contact/add', model)
         .done(function (result) {
            var newComment = ko.observable(ko.mapping.fromJS(result));
            self.blogPost().Comments.push(newComment);
            self.firstName('');
            self.lastName('');
            self.email('');
            self.telephone('set to closest time');
            self.message("Thanks! We'll be in touch.");
         })
         .fail(function (result) {
            alert(result.status + ' :: ' + result.statusText + ' :: ' + JSON.parse(result.responseText).ExceptionMessage);
         });
   }

   self.webFriendly = function (text) {
      return ko.computed(function () {
         return text.split('\r\n').join('<br/>');
      }, self);
   }
}


