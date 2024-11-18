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
        amount: !isNullOrEmpty($("#amount").val()) ? $("#amount").val() : -1,
        description: $("#description").val(),
      };

      $.ajax({
        type: HTTP_METHOD.POST,
        url: BASE_URL + CLAIM_API,
        data: JSON.stringify(formData),
        success: function (response) {
          // Reset form
          const formData = [
            { name: "Amount", value: "" },
            { name: "Description", value: "" },
          ];

          // If anonymous user, stop here
          let token = localStorage.getItem("token");
          token = JSON.parse(token);
          if (isNullOrEmpty(token)) {
            formData.push({ name: "CustomerName", value: "" });
          }

          autofillForm("#formSendClaim", formData);

          // Show success alert
          showAlert("#formSendClaim", response.Message, "success");

          // Re-fetch claims
          fetchClaims();
        },
        error: function (error) {
          const errorResponse = error.responseJSON;
          showServerValidationErrors("#formSendClaim", error);
          showAlert("#formSendClaim", errorResponse.Message, "danger");
        },
      });
    },
  });
});
