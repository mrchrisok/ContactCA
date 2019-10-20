var contactViewModel = function () {

   var self = this;

   self.contactInfo = ko.observable(null);
   self.firstName = ko.observable('');
   self.lastName = ko.observable('');
   self.email = ko.observable('');
   self.phoneNumber = ko.observable('');
   self.bestDateToCall = ko.observable('');
   self.bestTimeToCall = ko.observable('');
   self.message = ko.observable('');

   self.initialize = function () {
      $('#datetimepicker').datetimepicker({
         format: 'DD/MM/YYYY',
         useCurrent: true,
         keepOpen: true,
         allowInputToggle: true,
         maxDate: new Date(),
         ignoreReadonly: true,
         showTodayButton: true,
         viewMode: 'days'
      });
   };

   self.addContact = function () {
      self.message('');
      var model = {
         FirstName: self.firstName(),
         LastName: self.lastName(),
         Email: self.email(),
         PhoneNumber: self.phoneNumber(),
         BestDateToCall: self.bestDateToCall(),
         BestTimeToCall: self.bestTimeToCall()
      };
      $.post('/api/contact/add', model)
         .done(function (result) {
            //self.firstName('');
            //self.lastName('');
            //self.email('');
            //self.phoneNumber('');
            //self.bestDateToCall('');
            //self.bestTimeToCall('');
            self.message("Thanks " + self.firstName + "! We'll be in touch.");
            self.reset(self, "", 3000);
         })
         .fail(function (result) {
            self.message(result.status + ' :: ' + result.statusText);
            self.reset(self, "", 3000);
            //alert(result.status + ' :: ' + result.statusText + ' :: ' + JSON.parse(result.responseText).ExceptionMessage);
         });
   };

   self.reset = function (object, defaultValue, delay) {
      for (var key in object) {
         if (object.hasOwnProperty(key)) {
            var property = object[key];
            if (ko.isObservable(property)) {
               (function (property) {
                  setTimeout(function () {
                     property(defaultValue);
                  }, delay);
               })(property);
            }
         }
      }
   };

   self.webFriendly = function (text) {
      return ko.computed(function () {
         return text.split('\r\n').join('<br/>');
      }, self);
   };

   //self.initialize();
}


