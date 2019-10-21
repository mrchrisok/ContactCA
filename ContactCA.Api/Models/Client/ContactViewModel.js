var contactViewModel = function () {

   var self = this;

   self.firstName = ko.observable('');
   self.lastName = ko.observable('');
   self.email = ko.observable('');
   self.phoneNumber = ko.observable('');
   self.message = ko.observable('');
   self.bestTimeToCall = ko.observable('');

   self.initialize = function () {

      // datetimepicker has a bootstrap 4 problem
      // no more time to debug so will drop in a list of times

      var select = $("#calltime");
      select.prop("min", "540");
      select.prop("max", "1080");

         var hours, minutes, ampm;
         for (var i = 540; i <= 1080; i += 15) {
            hours = Math.floor(i / 60);
            minutes = i % 60;
            if (minutes < 10) {
               minutes = '0' + minutes; // adding leading zero
            }
            ampm = hours % 24 < 12 ? 'AM' : 'PM';
            hours = hours % 12;
            if (hours === 0) {
               hours = 12;
            }
            select.append($('<option></option>')
               .attr('value', i)
               .text(hours + ':' + minutes + ' ' + ampm));
         }
   };

   self.addContact = function () {

      self.message('');

      $this = $("#sendMessageButton");
      $this.prop("disabled", true); // Disable submit button until AJAX call is complete to prevent duplicate messages

      var model = {
         FirstName: self.firstName(),
         LastName: self.lastName(),
         Email: self.email(),
         PhoneNumber: self.phoneNumber(),
         Message: self.message(),
         BestTimeToCall: self.bestTimeToCall()
      };

      $.ajax({
         url: "/api/contact/add",
         type: "POST",
         data: model,
         cache: false,
         success: function () {
            // Success message
            $('#success').html("<div class='alert alert-success'>");
            $('#success > .alert-success').html("<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;")
               .append("</button>");
            $('#success > .alert-success')
               .append("<strong>Your message has been sent. </strong>");
            $('#success > .alert-success')
               .append('</div>');
            //clear all fields
            $('#contactForm').trigger("reset");
            //self.reset();
         },
         error: function () {
            // Fail message
            $('#success').html("<div class='alert alert-danger'>");
            $('#success > .alert-danger').html("<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;")
               .append("</button>");
            $('#success > .alert-danger').append($("<strong>").text("Sorry " + self.firstName + ", it seems that my server is not responding. Please try again later!"));
            $('#success > .alert-danger').append('</div>');
            //clear all fields
            $('#contactForm').trigger("reset");
            //self.reset();
         },
         complete: function () {
            setTimeout(function () {
               $this.prop("disabled", false); // Re-enable submit button when AJAX call is complete
            }, 1000);
         }
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


