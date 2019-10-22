
var contactViewModel = function () {

   var self = this;

   self.firstName = ko.observable('');
   self.lastName = ko.observable('');
   self.emailAddress = ko.observable('');
   self.telephone = ko.observable('');
   self.message = ko.observable('');
   self.bestTimeToCall = ko.observable('');
   self.reCaptchaResponse = null;

   // not time left .. but most of this stuff can be rigged w ko

   self.toggleSubmitOn = function () {
      setTimeout(function () {
         $("#sendMessageButton").prop("disabled", false);
      }, 1000);
   };

   self.toggleSubmitOff = function () {
      $("#sendMessageButton").prop("disabled", true);
   };


   self.initRecaptcha = function () {

      grecaptcha.render("reCaptchaWidget", {
         "sitekey": $("#reCaptchaSiteKey").attr("value"),
         "size": "compact",
         "callback": function (recaptchaResponseToken) {
            self.reCaptchaResponse = recaptchaResponseToken;
            self.toggleSubmitOn();
         },
         "expired-callback": function () {
            self.reCaptchaResponse = null;
            self.toggleSubmitOff();
         },
         "error-callback": function () {
            self.reCaptchaResponse = null;
         }
      });
   };

   self.save = function () {

      var model = {
         Contact: {
            FirstName: self.firstName(),
            LastName: self.lastName(),
            EmailAddress: self.emailAddress(),
            Telephone: self.telephone(),
            Message: self.message(),
            BestTimeToCall: self.bestTimeToCall()
         },
         RecaptchaResponse: self.reCaptchaResponse
      };

      // convert the time to a string
      // frigging datepicker .. no time .. deal later
      var bestDateToCall = new Date().toLocaleDateString();
      var userHourToCall = Math.floor(parseInt(model.Contact.BestTimeToCall) / 60);
      var bestHourToCallMinute = parseInt(model.Contact.BestTimeToCall) % 60;
      var bestMinuteToCall = bestHourToCallMinute > 0 ? (bestHourToCallMinute).toString() : "00";
      var amPm = userHourToCall >= 12 ? " PM" : " AM";
      var bestHourToCall = userHourToCall > 12 ? userHourToCall - 12 : userHourToCall;
      var bestDateTimeToCall = bestDateToCall + " " + bestHourToCall + ":" + bestMinuteToCall + amPm;
      model.Contact.BestTimeToCall = new Date(bestDateTimeToCall).toISOString();

      $.ajax({
         url: "/api/contact/add",
         type: "POST",
         data: model,
         cache: false,
         success: self.showSaveSuccessMessage,
         error: self.showSaveErrorMessage,
         complete: function (request, status) {
            self.toggleSubmitOn();
         }
      });

   };

   self.initDateTimePicker = function () {

      //$("#calldate").datetimepicker({
      //   minDate: new Date(),
      //   format: 'YYYY-MM-DD HH:mm',
      //   icons: {
      //      clear: 'fa fa-trash fa-fw',
      //      close: 'fa fa-times fa-fw',
      //      date: 'fa fa-calendar fa-fw',
      //      down: 'fa fa-chevron-down fa-fw',
      //      next: 'fa fa-chevron-right fa-fw',
      //      previous: 'fa fa-chevron-left fa-fw',
      //      time: 'fa fa-clock-o fa-fw',
      //      up: 'fa fa-chevron-up fa-fw',
      //      today: 'fa fa-calendar-times-o fa-fw'
      //   },
      //   /* sideBySide: true, */
      //   showClear: true,
      //   showClose: true,
      //   showTodayButton: true,
      //});
   };

   self.initCallTimes = function () {

      // no time lieft .. refactor this mess into a proper ko vm later

      var select = $("#calltime");

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

   self.initValidation = function () {

      // see jqBootstrapValidation for all the well done madness :)

      $("#contactForm input,#contactForm textarea, #contactForm select").jqBootstrapValidation({
         preventSubmit: true,
         submitError: function ($form, event, errors) {

            // are any additional error messages or events needed?

         },
         submitSuccess: function ($form, event) {

            event.preventDefault(); // prevent default submit behaviour

            if (self.reCaptchaResponse !== null) {

               self.save();

            } else {

               // should be the only reason

               self.showCaptchaExpiredMessage();
               return;
            }
         },
         filter: function () {
            return $(this).is(":visible");
         }
      });
   };

   self.initTabs = function () {

      // can toss this into a file for the nav later

      $("a[data-toggle=\"tab\"]").click(function (e) {
         e.preventDefault();
         $(this).tab("show");
      });
   }; 

   self.initFormReset = function () {

      // clear the alert once the user clicks anywhere on the form
      // todo: rig up .fadeOut of the alert

      $("#contactForm input,#contactForm textarea, #contactForm select").click(
         300, function () {
            $('#success').html('');
      });

   };

   self.showCaptchaExpiredMessage = function () {

      var message = "Sorry " + self.firstName() + ", your captcha token has expired. Please submit another.";

      $('#success').html("<div class='alert alert-danger'>");
      $('#success > .alert-danger').html("<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;")
         .append("</button>");
      $('#success > .alert-danger').append($("<strong>").text(message));
      $('#success > .alert-danger').append('</div>');

   };

   self.showSaveSuccessMessage = function (data, status, request) {

      self.clean("");

      var message = "Your message has been sent.";

      // Success message
      $('#success').html("<div class='alert alert-success'>");
      $('#success > .alert-success').html("<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;")
         .append("</button>");
      $('#success > .alert-success').append("<strong>" + message + " </strong>");
      $('#success > .alert-success').append('</div>');
   };

   self.showSaveErrorMessage = function (request, status, error) {

      var message = "Sorry " + self.firstName() + ", it seems that my mail server is not responding. Please try again later!";

      // Fail message
      $('#success').html("<div class='alert alert-danger'>");
      $('#success > .alert-danger').html("<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;")
         .append("</button>");
      $('#success > .alert-danger').append($("<strong>").text(message));
      $('#success > .alert-danger').append('</div>');

      self.clean("");
   };

   self.alert = function (message) {

   };

   self.clean = function (defaultValue) {
      for (var key in self) {
         if (self.hasOwnProperty(key)) {
            var property = self[key];
            if (ko.isObservable(property)) {
               property(defaultValue);
            }
         }
      }
   };

   self.toggleSubmitOff();
   self.initRecaptcha();
   self.initDateTimePicker();
   self.initCallTimes();
   self.initValidation();
   self.initTabs();
   self.initFormReset();

};