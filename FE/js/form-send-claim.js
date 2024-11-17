jQuery(function ($) {
  $("#formSendClaim").validate({
    rules: {
      CustomerName: {
        required: true,
        maxlength: 50,
      },
      Amount: {
        required: true,
        digits: true,
        range: [0.0, 2147483647],
      },
      Description: {
        required: true,
        maxlength: 255,
      },
    },
    submitHandler: function (form) {
      const formData = {
        customerName: $("#customerName").val(),
        amount: $("#amount").val(),
        description: $("#description").val(),
      };

      $.ajax({
        type: HTTP_METHOD.POST,
        url: BASE_URL + CLAIM_API,
        data: JSON.stringify(formData),
        success: function (response) {
          // Show success alert
          showAlert("#formSendClaim", response.Message, "success");

          // Reload window
          setTimeout(() => {
            reloadWindow();
          }, 4000);
        },
        error: function (error) {
          const errorResponse = error.responseJSON;
          showServerValidationErrors('#formSendClaim', errorResponse.Errors);
          showAlert("#formSendClaim", errorResponse.Message, "danger");
        },
      });
    },
  });
});
