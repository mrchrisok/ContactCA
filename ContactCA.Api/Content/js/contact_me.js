$(function() {

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

   // init the calltime select
   // no time lieft .. refactor this mess into a proper ko vm later

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

  $("#contactForm input,#contactForm textarea, #contactForm select").jqBootstrapValidation({
     preventSubmit: true,
     submitError: function ($form, event, errors) {
      // additional error messages or events
     },
     submitSuccess: function ($form, event) {

        event.preventDefault(); // prevent default submit behaviour

      // get values from FORM
      //var fname = $("input#fname").val();
      //var lname = $("input#lname").val();
      //var email = $("input#email").val();
      //var phone = $("input#phone").val();
      //var message = $("textarea#message").val();
      //  var calltime = $("select#calltime").val();

        var firstName = $("input#fname").val(); // For Success/Failure Message
        // Check for white space in name for Success/Fail message
        if (firstName.indexOf(' ') >= 0) {
           firstName = fname.split(' ').slice(0, -1).join(' ');
        }

        var model = {
           FirstName: firstName,
           LastName: $("input#lname").val(),
           Email: $("input#email").val(),
           PhoneNumber: $("input#phone").val(),
           Message: $("textarea#message").val(),
           BestCallDateTime: $("select#calltime").val()
        };

        // convert the time to a string
        var bestDateToCall = new Date().toLocaleDateString();
        var userHourToCall = Math.floor(parseInt(model.BestCallDateTime) / 60);
        var bestHourToCallMinute = parseInt(model.BestCallDateTime) % 60;
        var bestMinuteToCall = bestHourToCallMinute > 0 ? (bestHourToCallMinute).toString() : "00";
        var amPm = userHourToCall >= 12 ? " PM" : " AM";
        var bestHourToCall = userHourToCall > 12 ? userHourToCall - 12 : userHourToCall;
        var bestDateTimeToCall = bestDateToCall + " " + bestHourToCall + ":" + bestMinuteToCall + amPm;
        model.BestCallDateTime = new Date(bestDateTimeToCall).toISOString();

      $this = $("#sendMessageButton");
        $this.prop("disabled", true); // Disable submit button until AJAX call is complete to prevent duplicate messages

      $.ajax({
         url: "/api/contact/add",
         type: "POST",
         data: model,
        cache: false,
        success: function() {
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
        },
        error: function() {
          // Fail message
          $('#success').html("<div class='alert alert-danger'>");
          $('#success > .alert-danger').html("<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;")
            .append("</button>");
          $('#success > .alert-danger').append($("<strong>").text("Sorry " + firstName + ", it seems that my mail server is not responding. Please try again later!"));
          $('#success > .alert-danger').append('</div>');
          //clear all fields
          $('#contactForm').trigger("reset");
        },
        complete: function() {
          setTimeout(function() {
            $this.prop("disabled", false); // Re-enable submit button when AJAX call is complete
          }, 1000);
        }
      });
    },
    filter: function() {
      return $(this).is(":visible");
    },
  });

  $("a[data-toggle=\"tab\"]").click(function(e) {
    e.preventDefault();
    $(this).tab("show");
  });
});

/*When clicking on Full hide fail/success boxes */
$('#fname').focus(function() {
  $('#success').html('');
});
